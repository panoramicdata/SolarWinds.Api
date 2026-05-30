using AwesomeAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace SolarWinds.Api.Test.ServiceDesk;

public class WorkflowApproverTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetApprovers_WithValidIds_ReturnsResult()
	{
		// WorkflowApprover IDs are workflow-specific; skip gracefully if none configured
		var configuration = new ConfigurationBuilder()
			.AddUserSecrets<WorkflowApproverTests>()
			.Build();

		var idsRaw = configuration["ServiceDesk:WorkflowApproverIds"];
		if (string.IsNullOrWhiteSpace(idsRaw))
		{
			return;
		}

		var ids = idsRaw
			.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(s => int.TryParse(s, out var v) ? v : (int?)null)
			.Where(v => v.HasValue)
			.Select(v => v!.Value)
			.ToList();

		if (ids.Count == 0)
		{
			return;
		}

		var approvers = await ServiceDeskClient.WorkflowApprovers.GetAsync(ids, CancellationToken);
		approvers.Should().NotBeNull();
	}
}
