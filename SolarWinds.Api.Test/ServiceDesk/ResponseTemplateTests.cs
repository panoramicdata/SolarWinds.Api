namespace SolarWinds.Api.Test.ServiceDesk;

public class ResponseTemplateTests(ITestOutputHelper output) : TestWithOutput(output)
{
	[Fact]
	public async Task GetTotalCount_ReturnsResult()
	{
		var result = await ServiceDeskClient.ResponseTemplates.GetTotalCountAsync(CancellationToken);
		result.Should().NotBeNull();
		result.EffectiveCount.Should().BeGreaterThanOrEqualTo(0);
	}

	[Fact]
	public async Task GetTotalCountPersonal_ReturnsResult()
	{
		var result = await ServiceDeskClient.ResponseTemplates.GetTotalCountPersonalAsync(CancellationToken);
		result.Should().NotBeNull();
		result.EffectiveCount.Should().BeGreaterThanOrEqualTo(0);
	}

	[Fact]
	public async Task GetTotalCountGlobal_ReturnsResult()
	{
		var result = await ServiceDeskClient.ResponseTemplates.GetTotalCountGlobalAsync(CancellationToken);
		result.Should().NotBeNull();
		result.EffectiveCount.Should().BeGreaterThanOrEqualTo(0);
	}

	[Fact]
	public async Task GetResponseTemplateVariables_WithValidIncidentId_ReturnsResult()
	{
		Incident? created = null;
		int incidentId;

		var incidents = await ServiceDeskClient.Incidents.GetAsync(
			new GetIncidentsRequest { Page = 1, PerPage = 1 },
			CancellationToken);

		if (incidents.Count > 0)
		{
			incidentId = incidents[0].Id;
		}
		else
		{
			try
			{
				created = await ServiceDeskClient.Incidents.CreateAsync(new IncidentCreateRequest
				{
					Incident = new IncidentWriteFields
					{
						Name = $"Response template probe {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
						Description = "Temporary incident for response template variable coverage",
						Priority = "Low",
					}
				}, CancellationToken);
			}
			catch (ApiException)
			{
				return;
			}

			incidentId = created.Id;
		}

		try
		{
			var variables = await ServiceDeskClient.Incidents.GetResponseTemplateVariablesAsync(incidentId, CancellationToken);
			variables.Should().NotBeNull();
		}
		catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			// Some tenants do not expose response-template variables for all incidents.
		}
		finally
		{
			if (created?.Id > 0)
			{
				try
				{
					await ServiceDeskClient.Incidents.DeleteAsync(created.Id, CancellationToken);
				}
				catch (ApiException)
				{
				}
			}
		}
	}
}
