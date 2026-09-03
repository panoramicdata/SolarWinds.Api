namespace SolarWinds.Api.Test.ServiceDesk;

/// <summary>
/// Represents this type.
/// </summary>
public class CoreDomainQueryRequestTests
{
	/// <summary>
	/// Executes Categories_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Categories_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<ICategories>(client);

		await api.GetAsync(new GetCategoriesRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/categories.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Categories_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Categories_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<ICategories>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/categories/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes CatalogItems_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task CatalogItems_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<ICatalogItems>(client);

		await api.GetAsync(new GetCatalogItemsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/catalog_items.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes CatalogItems_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task CatalogItems_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<ICatalogItems>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/catalog_items/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes ConfigurationItems_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task ConfigurationItems_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IConfigurationItems>(client);

		await api.GetAsync(new GetConfigurationItemsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/configuration_items.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes ConfigurationItems_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task ConfigurationItems_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IConfigurationItems>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/configuration_items/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Contracts_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Contracts_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IContracts>(client);

		await api.GetAsync(new GetContractsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/contracts.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Contracts_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Contracts_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IContracts>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/contracts/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Hardwares_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Hardwares_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IHardwares>(client);

		await api.GetAsync(new GetHardwaresRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/hardwares.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Hardwares_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Hardwares_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IHardwares>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/hardwares/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes OtherAssets_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task OtherAssets_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IOtherAssets>(client);

		await api.GetAsync(new GetOtherAssetsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/other_assets.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes OtherAssets_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task OtherAssets_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IOtherAssets>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/other_assets/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Problems_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Problems_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IProblems>(client);

		await api.GetAsync(new GetProblemsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/problems.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Problems_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Problems_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IProblems>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/problems/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Changes_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Changes_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IChanges>(client);

		await api.GetAsync(new GetChangesRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/changes.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Changes_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Changes_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<IChanges>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/changes/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Solutions_GetAll_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Solutions_GetAll_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler();
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<ISolutions>(client);

		await api.GetAllAsync(new GetSolutionsRequest { Layout = ResponseLayout.Long }, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/solutions.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

	/// <summary>
	/// Executes Solutions_GetById_WithLayoutLong_UsesExpectedQueryParameters.
	/// </summary>
	[Fact]
	public async Task Solutions_GetById_WithLayoutLong_UsesExpectedQueryParameters()
	{
		var capture = new CaptureHandler("{}");
		using var client = ServiceDeskTestApi.CreateHttpClient(capture);
		var api = ServiceDeskTestApi.CreateApi<ISolutions>(client);

		await api.GetAsync(123, ResponseLayout.Long, CancellationToken.None);

		capture.LastRequest.Should().NotBeNull();
		capture.LastRequest!.RequestUri!.AbsolutePath.Should().Be("/solutions/123.json");
		var query = ServiceDeskTestApi.ParseQuery(capture.LastRequest.RequestUri);
		query["layout"].Should().Be("long");
	}

}

