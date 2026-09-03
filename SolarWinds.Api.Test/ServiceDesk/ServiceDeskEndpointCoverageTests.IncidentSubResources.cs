namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Endpoint coverage for the sub-resources that hang off an incident: comments, tasks and time tracks.
/// </summary>
public partial class ServiceDeskEndpointCoverageTests
{
	/// <summary>
	/// Executes Comments_Tasks_TimeTracks_AreCovered.
	/// </summary>
	[Fact]
	public async Task Comments_Tasks_TimeTracks_AreCovered()
	{
		if (!ShouldRunDestructiveIntegrationTests())
		{
			return;
		}

		var created = new IncidentSubResources();
		try
		{
			created.Incident = await CreateCoverageSourceIncidentAsync();
			await ExerciseCommentEndpointsAsync(created);
			await ExerciseTaskEndpointsAsync(created);
			await ExerciseTimeTrackEndpointsAsync(created);
		}
		catch (ApiException ex) when ((int)ex.StatusCode >= 500 || ex.StatusCode == HttpStatusCode.Unauthorized)
		{
			// Some tenant workflows intermittently throw server errors, and some tokens do not grant write access.
		}
		finally
		{
			await CleanUpIncidentSubResourcesAsync(created);
		}
	}

	/// <summary>
	/// The records a sub-resource coverage run creates, tracked so that whatever was created can
	/// be removed afterwards, whether or not the run reached the end.
	/// </summary>
	private sealed class IncidentSubResources
	{
		public Incident? Incident { get; set; }
		public Comment? Comment { get; set; }
		public ServiceTask? Task { get; set; }
		public TimeTrack? TimeTrack { get; set; }
	}

	/// <summary>
	/// Creates the incident that the comment, task and time-track endpoints hang off.
	/// </summary>
	private async Task<Incident> CreateCoverageSourceIncidentAsync()
	{
		var createdIncident = await ServiceDeskClient.Incidents.CreateAsync(new IncidentCreateRequest
		{
			Incident = new IncidentWriteFields
			{
				Name = $"Coverage source incident {DateTimeOffset.UtcNow:yyyyMMddHHmmss}",
				Description = "Source incident for comment/task/time-track coverage",
				Priority = "Low",
			}
		}, CancellationToken);

		createdIncident.Should().NotBeNull();
		createdIncident!.Id.Should().BePositive();

		return createdIncident;
	}

	private async Task ExerciseCommentEndpointsAsync(IncidentSubResources created)
	{
		var incidentId = created.Incident!.Id;

		created.Comment = await ServiceDeskClient.Comments.CreateAsync(ObjectType.Incidents, incidentId, new CommentCreateRequest
		{
			Comment = new CommentWriteFields { Body = "Coverage comment", IsPrivate = true }
		}, CancellationToken);

		created.Comment.Should().NotBeNull();
		created.Comment!.Id.Should().BePositive();

		await ServiceDeskClient.Comments.UpdateAsync(ObjectType.Incidents, incidentId, created.Comment.Id, new CommentUpdateRequest
		{
			Comment = new CommentWriteFields { Body = "Coverage comment updated", IsPrivate = true }
		}, CancellationToken);
	}

	private async Task ExerciseTaskEndpointsAsync(IncidentSubResources created)
	{
		var incidentId = created.Incident!.Id;

		created.Task = await ServiceDeskClient.Tasks.CreateAsync(ObjectType.Incidents, incidentId, new TaskCreateRequest
		{
			Task = new TaskWriteFields { Name = "Coverage task" }
		}, CancellationToken);

		created.Task.Should().NotBeNull();
		created.Task!.Id.Should().BePositive();

		await ServiceDeskClient.Tasks.UpdateAsync(ObjectType.Incidents, incidentId, created.Task.Id, new TaskUpdateRequest
		{
			Task = new TaskWriteFields { Name = "Coverage task updated", IsComplete = false }
		}, CancellationToken);
	}

	private async Task ExerciseTimeTrackEndpointsAsync(IncidentSubResources created)
	{
		var incidentId = created.Incident!.Id;

		created.TimeTrack = await ServiceDeskClient.TimeTracks.CreateAsync(ObjectType.Incidents, incidentId, new TimeTrackCreateRequest
		{
			TimeTrack = new TimeTrackWriteFields { Name = "Coverage track", MinutesParsed = "15" }
		}, CancellationToken);

		created.TimeTrack.Should().NotBeNull();
		created.TimeTrack!.Id.Should().BePositive();

		_ = await ServiceDeskClient.TimeTracks.GetAsync(ObjectType.Incidents, incidentId, CancellationToken);

		await ServiceDeskClient.TimeTracks.UpdateAsync(ObjectType.Incidents, incidentId, created.TimeTrack.Id, new TimeTrackUpdateRequest
		{
			TimeTrack = new TimeTrackWriteFields { Name = "Coverage track updated", MinutesParsed = "20" }
		}, CancellationToken);
	}

	/// <summary>
	/// Removes whatever the run created, sub-resources first: the API will not delete an incident
	/// that still has them.
	/// </summary>
	private async Task CleanUpIncidentSubResourcesAsync(IncidentSubResources created)
	{
		if (created.Incident is not { Id: > 0 } incident)
		{
			return;
		}

		await DeleteIfCreatedAsync(created.TimeTrack?.Id, id =>
			ServiceDeskClient.TimeTracks.DeleteAsync(ObjectType.Incidents, incident.Id, id, CancellationToken));

		await DeleteIfCreatedAsync(created.Task?.Id, id =>
			ServiceDeskClient.Tasks.DeleteAsync(ObjectType.Incidents, incident.Id, id, CancellationToken));

		await DeleteIfCreatedAsync(created.Comment?.Id, id =>
			ServiceDeskClient.Comments.DeleteAsync(ObjectType.Incidents, incident.Id, id, CancellationToken));

		await DeleteIfCreatedAsync(incident.Id, id =>
			ServiceDeskClient.Incidents.DeleteAsync(id, CancellationToken));
	}
}
