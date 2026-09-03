using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Refit;
using SolarWinds.Api.Http;
using SolarWinds.Api.ServiceDesk.Helpers;
using SolarWinds.Api.ServiceDesk.Interfaces;

namespace SolarWinds.Api;

/// <summary>
/// A client for the SolarWinds Service Desk API.
/// </summary>
public class SolarWindsServiceDeskClient
{
	private static readonly RefitSettings RefitSettings = new(
		new SystemTextJsonContentSerializer(new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
		}))
	{
		UrlParameterFormatter = new ServiceDeskUrlParameterFormatter()
	};

	/// <summary>
	/// Gets the Service Desk Tickets API.
	/// </summary>
	public ITickets Tickets { get; private set; }

	/// <summary>
	/// Gets the Service Desk Users API.
	/// </summary>
	public IUsers Users { get; private set; }

	/// <summary>
	/// Gets the Service Desk Incidents API.
	/// </summary>
	public IIncidents Incidents { get; private set; }

	/// <summary>
	/// Gets the Service Desk Other Assets API.
	/// </summary>
	public IOtherAssets OtherAssets { get; private set; }

	/// <summary>
	/// Gets the Service Desk Configuration Items API.
	/// </summary>
	public IConfigurationItems ConfigurationItems { get; private set; }

	/// <summary>
	/// Gets the Service Desk Problems API.
	/// </summary>
	public IProblems Problems { get; private set; }

	/// <summary>
	/// Gets the Service Desk Changes API.
	/// </summary>
	public IChanges Changes { get; private set; }

	/// <summary>
	/// Gets the Service Desk Change Catalogs API.
	/// </summary>
	public IChangeCatalogs ChangeCatalogs { get; private set; }

	/// <summary>
	/// Gets the Service Desk Change Requests API.
	/// </summary>
	public IChangeRequests ChangeRequests { get; private set; }

	/// <summary>
	/// Gets the Service Desk Releases API.
	/// </summary>
	public IReleases Releases { get; private set; }

	/// <summary>
	/// Gets the Service Desk Solutions API.
	/// </summary>
	public ISolutions Solutions { get; private set; }

	/// <summary>
	/// Gets the Service Desk Catalog Items API.
	/// </summary>
	public ICatalogItems CatalogItems { get; private set; }

	/// <summary>
	/// Gets the Service Desk Service Requests API.
	/// </summary>
	public IServiceRequests ServiceRequests { get; private set; }

	/// <summary>
	/// Gets the Service Desk Sites API.
	/// </summary>
	public ISites Sites { get; private set; }

	/// <summary>
	/// Gets the Service Desk Departments API.
	/// </summary>
	public IDepartments Departments { get; private set; }

	/// <summary>
	/// Gets the Service Desk Roles API.
	/// </summary>
	public IRoles Roles { get; private set; }

	/// <summary>
	/// Gets the Service Desk Groups API.
	/// </summary>
	public IGroups Groups { get; private set; }

	/// <summary>
	/// Gets the Service Desk Memberships API.
	/// </summary>
	public IMemberships Memberships { get; private set; }

	/// <summary>
	/// Gets the Service Desk Categories API.
	/// </summary>
	public ICategories Categories { get; private set; }

	/// <summary>
	/// Gets the Service Desk Hardware API.
	/// </summary>
	public IHardwares Hardwares { get; private set; }

	/// <summary>
	/// Gets the Service Desk Mobile Devices API.
	/// </summary>
	public IMobileDevices MobileDevices { get; private set; }

	/// <summary>
	/// Gets the Service Desk Software API.
	/// </summary>
	public ISoftwares Softwares { get; private set; }

	/// <summary>
	/// Gets the Service Desk Printers API.
	/// </summary>
	public IPrinters Printers { get; private set; }

	/// <summary>
	/// Gets the Service Desk Contracts API.
	/// </summary>
	public IContracts Contracts { get; private set; }

	/// <summary>
	/// Gets the Service Desk Purchase Orders API.
	/// </summary>
	public IPurchaseOrders PurchaseOrders { get; private set; }

	/// <summary>
	/// Gets the Service Desk Vendors API.
	/// </summary>
	public IVendors Vendors { get; private set; }

	/// <summary>
	/// Gets the Service Desk Tasks API.
	/// </summary>
	public ITasks Tasks { get; private set; }

	/// <summary>
	/// Gets the Service Desk Comments API.
	/// </summary>
	public IComments Comments { get; private set; }

	/// <summary>
	/// Gets the Service Desk Time Tracks API.
	/// </summary>
	public ITimeTracks TimeTracks { get; private set; }

	/// <summary>
	/// Gets the Service Desk Purchases API.
	/// </summary>
	public IPurchases Purchases { get; private set; }

	/// <summary>
	/// Gets the Service Desk Audits API.
	/// </summary>
	public IAudits Audits { get; private set; }

	/// <summary>
	/// Gets the Service Desk Risks API.
	/// </summary>
	public IRisks Risks { get; private set; }

	/// <summary>
	/// Gets the Service Desk Attachments API.
	/// </summary>
	public IAttachments Attachments { get; private set; }

	/// <summary>
	/// Gets the Service Desk Notifications API.
	/// </summary>
	public INotifications Notifications { get; private set; }

	/// <summary>
	/// Gets the Service Desk Incident Types API.
	/// </summary>
	public IIncidentTypes IncidentTypes { get; private set; }

	/// <summary>
	/// Gets the Service Desk ITSM setup states API.
	/// </summary>
	public ISetupItsmStates SetupItsmStates { get; private set; }

	/// <summary>
	/// Gets the Service Desk UI custom views API.
	/// </summary>
	public IUiCustomViews UiCustomViews { get; private set; }

	/// <summary>
	/// Gets the Service Desk UI infrastructure API.
	/// </summary>
	public IUiInfrastructure UiInfrastructure { get; private set; }

	/// <summary>
	/// Gets the Service Desk dashboards API.
	/// </summary>
	public IDashboards Dashboards { get; private set; }

	/// <summary>
	/// Gets the Service Desk widgets API.
	/// </summary>
	public IWidgets Widgets { get; private set; }

	/// <summary>
	/// Gets the Service Desk UI jsonhtml list API.
	/// </summary>
	public IUiJsonHtmlLists UiJsonHtmlLists { get; private set; }

	/// <summary>
	/// Gets the Service Desk Custom Forms API.
	/// </summary>
	public ICustomForms CustomForms { get; private set; }

	/// <summary>
	/// Gets the Service Desk Response Templates API.
	/// </summary>
	public IResponseTemplates ResponseTemplates { get; private set; }

	/// <summary>
	/// Gets the Service Desk Workflow Approvers API.
	/// </summary>
	public IWorkflowApprovers WorkflowApprovers { get; private set; }

	/// <summary>
	/// Gets the Service Desk Custom Fields API.
	/// </summary>
	public ICustomFields CustomFields { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="SolarWindsServiceDeskClient"/> class.
	/// </summary>
	/// <param name="options">The client options.</param>
	public SolarWindsServiceDeskClient(SolarWindsServiceDeskClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);

		if (string.IsNullOrWhiteSpace(options.BaseUrl))
		{
			throw new ArgumentException("BaseUrl must be provided.", nameof(options));
		}

		if (string.IsNullOrWhiteSpace(options.AccessToken))
		{
			throw new ArgumentException("AccessToken must be provided.", nameof(options));
		}

		var httpClient = CreateHttpClient(options);

		InitializeItsmApis(httpClient);
		InitializeAssetApis(httpClient);
		InitializeProcurementApis(httpClient);
		InitializeOrganizationApis(httpClient);
		InitializeActivityApis(httpClient);
		InitializeUiApis(httpClient);
	}

	private static HttpClient CreateHttpClient(SolarWindsServiceDeskClientOptions options)
	{
		HttpMessageHandler handler = new HttpClientHandler();
		handler = options.Logger is { } logger
			? new LoggingDelegatingHandler(logger) { InnerHandler = handler }
			: handler;
		handler = new SolarWindsServiceDeskBackingOffHandler(options) { InnerHandler = handler };

		var httpClient = new HttpClient(handler)
		{
			BaseAddress = new Uri(options.BaseUrl),
			Timeout = options.HttpClientTimeout
		};

		httpClient.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + options.AccessToken);
		httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.samanage.v2.1+json");

		return httpClient;
	}

	/// <summary>
	/// Builds the APIs for incident, problem, change, release and request records.
	/// </summary>
	[MemberNotNull(
		nameof(Tickets),
		nameof(Incidents),
		nameof(Problems),
		nameof(Changes),
		nameof(ChangeCatalogs),
		nameof(ChangeRequests),
		nameof(Releases),
		nameof(Solutions),
		nameof(Risks),
		nameof(IncidentTypes),
		nameof(SetupItsmStates),
		nameof(CatalogItems),
		nameof(ServiceRequests))]
	private void InitializeItsmApis(HttpClient httpClient)
	{
		Tickets = RestService.For<ITickets>(httpClient, RefitSettings);
		Incidents = RestService.For<IIncidents>(httpClient, RefitSettings);
		Problems = RestService.For<IProblems>(httpClient, RefitSettings);
		Changes = RestService.For<IChanges>(httpClient, RefitSettings);
		ChangeCatalogs = RestService.For<IChangeCatalogs>(httpClient, RefitSettings);
		ChangeRequests = RestService.For<IChangeRequests>(httpClient, RefitSettings);
		Releases = RestService.For<IReleases>(httpClient, RefitSettings);
		Solutions = RestService.For<ISolutions>(httpClient, RefitSettings);
		Risks = RestService.For<IRisks>(httpClient, RefitSettings);
		IncidentTypes = RestService.For<IIncidentTypes>(httpClient, RefitSettings);
		SetupItsmStates = RestService.For<ISetupItsmStates>(httpClient, RefitSettings);
		CatalogItems = RestService.For<ICatalogItems>(httpClient, RefitSettings);
		ServiceRequests = RestService.For<IServiceRequests>(httpClient, RefitSettings);
	}

	/// <summary>
	/// Builds the APIs for hardware and configuration inventory.
	/// </summary>
	[MemberNotNull(
		nameof(OtherAssets),
		nameof(ConfigurationItems),
		nameof(Hardwares),
		nameof(MobileDevices),
		nameof(Softwares),
		nameof(Printers))]
	private void InitializeAssetApis(HttpClient httpClient)
	{
		OtherAssets = RestService.For<IOtherAssets>(httpClient, RefitSettings);
		ConfigurationItems = RestService.For<IConfigurationItems>(httpClient, RefitSettings);
		Hardwares = RestService.For<IHardwares>(httpClient, RefitSettings);
		MobileDevices = RestService.For<IMobileDevices>(httpClient, RefitSettings);
		Softwares = RestService.For<ISoftwares>(httpClient, RefitSettings);
		Printers = RestService.For<IPrinters>(httpClient, RefitSettings);
	}

	/// <summary>
	/// Builds the APIs for contracts, purchasing and suppliers.
	/// </summary>
	[MemberNotNull(
		nameof(Contracts),
		nameof(PurchaseOrders),
		nameof(Vendors),
		nameof(Purchases))]
	private void InitializeProcurementApis(HttpClient httpClient)
	{
		Contracts = RestService.For<IContracts>(httpClient, RefitSettings);
		PurchaseOrders = RestService.For<IPurchaseOrders>(httpClient, RefitSettings);
		Vendors = RestService.For<IVendors>(httpClient, RefitSettings);
		Purchases = RestService.For<IPurchases>(httpClient, RefitSettings);
	}

	/// <summary>
	/// Builds the APIs for people, places and the groupings that classify records.
	/// </summary>
	[MemberNotNull(
		nameof(Users),
		nameof(Sites),
		nameof(Departments),
		nameof(Roles),
		nameof(Groups),
		nameof(Memberships),
		nameof(Categories))]
	private void InitializeOrganizationApis(HttpClient httpClient)
	{
		Users = RestService.For<IUsers>(httpClient, RefitSettings);
		Sites = RestService.For<ISites>(httpClient, RefitSettings);
		Departments = RestService.For<IDepartments>(httpClient, RefitSettings);
		Roles = RestService.For<IRoles>(httpClient, RefitSettings);
		Groups = RestService.For<IGroups>(httpClient, RefitSettings);
		Memberships = RestService.For<IMemberships>(httpClient, RefitSettings);
		Categories = RestService.For<ICategories>(httpClient, RefitSettings);
	}

	/// <summary>
	/// Builds the APIs for the work, correspondence and history attached to a record.
	/// </summary>
	[MemberNotNull(
		nameof(Tasks),
		nameof(Comments),
		nameof(TimeTracks),
		nameof(Audits),
		nameof(Attachments),
		nameof(Notifications),
		nameof(WorkflowApprovers))]
	private void InitializeActivityApis(HttpClient httpClient)
	{
		Tasks = RestService.For<ITasks>(httpClient, RefitSettings);
		Comments = RestService.For<IComments>(httpClient, RefitSettings);
		TimeTracks = RestService.For<ITimeTracks>(httpClient, RefitSettings);
		Audits = RestService.For<IAudits>(httpClient, RefitSettings);
		Attachments = RestService.For<IAttachments>(httpClient, RefitSettings);
		Notifications = RestService.For<INotifications>(httpClient, RefitSettings);
		WorkflowApprovers = RestService.For<IWorkflowApprovers>(httpClient, RefitSettings);
	}

	/// <summary>
	/// Builds the APIs for the portal's views, dashboards and form definitions.
	/// </summary>
	[MemberNotNull(
		nameof(UiCustomViews),
		nameof(UiInfrastructure),
		nameof(Dashboards),
		nameof(Widgets),
		nameof(UiJsonHtmlLists),
		nameof(CustomForms),
		nameof(ResponseTemplates),
		nameof(CustomFields))]
	private void InitializeUiApis(HttpClient httpClient)
	{
		UiCustomViews = RestService.For<IUiCustomViews>(httpClient, RefitSettings);
		UiInfrastructure = RestService.For<IUiInfrastructure>(httpClient, RefitSettings);
		Dashboards = RestService.For<IDashboards>(httpClient, RefitSettings);
		Widgets = RestService.For<IWidgets>(httpClient, RefitSettings);
		UiJsonHtmlLists = RestService.For<IUiJsonHtmlLists>(httpClient, RefitSettings);
		CustomForms = RestService.For<ICustomForms>(httpClient, RefitSettings);
		ResponseTemplates = RestService.For<IResponseTemplates>(httpClient, RefitSettings);
		CustomFields = RestService.For<ICustomFields>(httpClient, RefitSettings);
	}
}
