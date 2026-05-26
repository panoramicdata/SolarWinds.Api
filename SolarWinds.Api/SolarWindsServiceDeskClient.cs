using System.Text.Json;
using Refit;
using SolarWinds.Api.Http;
using SolarWinds.Api.ServiceDesk.Interfaces;

namespace SolarWinds.Api;

/// <summary>
/// A client for the SolarWinds Service Desk API.
/// </summary>
public class SolarWindsServiceDeskClient
{
	private readonly HttpClient _httpClient;

	private static readonly RefitSettings RefitSettings = new(
		new SystemTextJsonContentSerializer(new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
		}));

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
	/// Initializes a new instance of the <see cref="SolarWindsServiceDeskClient"/> class.
	/// </summary>
	/// <param name="options">The client options.</param>
	public SolarWindsServiceDeskClient(SolarWindsServiceDeskClientOptions options)
	{
		ArgumentNullException.ThrowIfNull(options);

		if (string.IsNullOrWhiteSpace(options.BaseUrl))
		{
			throw new ArgumentException("BaseUrl must be provided.", nameof(options.BaseUrl));
		}

		if (string.IsNullOrWhiteSpace(options.AccessToken))
		{
			throw new ArgumentException("AccessToken must be provided.", nameof(options.AccessToken));
		}

		HttpMessageHandler handler = new HttpClientHandler();
		handler = options.Logger is { } logger
			? new LoggingDelegatingHandler(logger) { InnerHandler = handler }
			: handler;
		handler = new SolarWindsServiceDeskBackingOffHandler(options) { InnerHandler = handler };

		_httpClient = new HttpClient(handler)
		{
			BaseAddress = new Uri(options.BaseUrl),
			Timeout = options.HttpClientTimeout
		};

		_httpClient.DefaultRequestHeaders.Add("X-Samanage-Authorization", "Bearer " + options.AccessToken);
		_httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.samanage.v2.1+json");

		Tickets = RestService.For<ITickets>(_httpClient, RefitSettings);
		Users = RestService.For<IUsers>(_httpClient, RefitSettings);
		Incidents = RestService.For<IIncidents>(_httpClient, RefitSettings);
		OtherAssets = RestService.For<IOtherAssets>(_httpClient, RefitSettings);
		ConfigurationItems = RestService.For<IConfigurationItems>(_httpClient, RefitSettings);
		Problems = RestService.For<IProblems>(_httpClient, RefitSettings);
		Changes = RestService.For<IChanges>(_httpClient, RefitSettings);
		Releases = RestService.For<IReleases>(_httpClient, RefitSettings);
		Solutions = RestService.For<ISolutions>(_httpClient, RefitSettings);
		CatalogItems = RestService.For<ICatalogItems>(_httpClient, RefitSettings);
		ServiceRequests = RestService.For<IServiceRequests>(_httpClient, RefitSettings);
		Sites = RestService.For<ISites>(_httpClient, RefitSettings);
		Departments = RestService.For<IDepartments>(_httpClient, RefitSettings);
		Roles = RestService.For<IRoles>(_httpClient, RefitSettings);
		Groups = RestService.For<IGroups>(_httpClient, RefitSettings);
		Categories = RestService.For<ICategories>(_httpClient, RefitSettings);
		Hardwares = RestService.For<IHardwares>(_httpClient, RefitSettings);
		MobileDevices = RestService.For<IMobileDevices>(_httpClient, RefitSettings);
		Softwares = RestService.For<ISoftwares>(_httpClient, RefitSettings);
		Printers = RestService.For<IPrinters>(_httpClient, RefitSettings);
		Contracts = RestService.For<IContracts>(_httpClient, RefitSettings);
		PurchaseOrders = RestService.For<IPurchaseOrders>(_httpClient, RefitSettings);
		Vendors = RestService.For<IVendors>(_httpClient, RefitSettings);
		Tasks = RestService.For<ITasks>(_httpClient, RefitSettings);
		Comments = RestService.For<IComments>(_httpClient, RefitSettings);
		TimeTracks = RestService.For<ITimeTracks>(_httpClient, RefitSettings);
		Purchases = RestService.For<IPurchases>(_httpClient, RefitSettings);
		Audits = RestService.For<IAudits>(_httpClient, RefitSettings);
		Risks = RestService.For<IRisks>(_httpClient, RefitSettings);
		Attachments = RestService.For<IAttachments>(_httpClient, RefitSettings);
	}
}
