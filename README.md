[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# SolarWinds.Api NuGet package

[![Nuget](https://img.shields.io/nuget/v/SolarWinds.Api)](https://www.nuget.org/packages/SolarWinds.Api/)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/TODO)](https://app.codacy.com/gh/panoramicdata/SolarWinds.Api/dashboard)

A .NET API client for SolarWinds Orion and Service Desk.

## OpenAPI UI (GitHub Pages)

This repository publishes an interactive Swagger UI for the generated Service Desk OpenAPI document via GitHub Pages.

- Workflow: `.github/workflows/gh-pages.yml`
- UI source: `docs/openapi/index.html`
- OpenAPI document: `SolarWinds.ServiceDesk.OpenApi.json`

To regenerate the OpenAPI document for a follow-up commit where git height will increment by one, run:

`dotnet run --project ./SolarWinds.Api.OpenApi/SolarWinds.Api.OpenApi.csproj --configuration Release -- ./SolarWinds.ServiceDesk.OpenApi.json --next-commit-version`

`--next-commit-version` requires a clean working tree (`git status --porcelain`) and increments the last numeric segment of the NBGV-derived version before writing the document.

After enabling GitHub Pages with **Build and deployment: GitHub Actions**, the site is published at:

`https://<org-or-user>.github.io/SolarWinds.Api/`

## Installation

Install the NuGet package:

```bash
Install-Package SolarWinds.Api
```

## Configuration

This library expects an `appsettings.json` file in your project's output directory with the following structure for connecting to SolarWinds Orion:

```json
{
  "Hostname": "YOUR_ORION_HOSTNAME",
  "Username": "YOUR_ORION_USERNAME",
  "Password": "YOUR_ORION_PASSWORD",
  "IgnoreSslCertificateErrors": true
}
```

For the SolarWinds Service Desk API, see [API documentation](https://apidoc.samanage.com/#section/General-Concepts) and the configuration below.

## Usage

### Initializing the Client

#### SolarWinds Orion Client

The `SolarWindsClient` automatically loads configuration from `appsettings.json`.

```csharp
using SolarWinds.Api;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

// For logging (optional)
var loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());
var logger = loggerFactory.CreateLogger<SolarWindsClient>();

var client = new SolarWindsClient(logger);
```

#### SolarWinds Service Desk Client

The `SolarWindsServiceDeskClient` requires options to be passed during initialization.

```csharp
using SolarWinds.Api;
using SolarWinds.Api.ServiceDesk;

var serviceDeskOptions = new SolarWindsServiceDeskClientOptions
{
    // Regional base URLs:
    //   US:  https://api.samanage.com
    //   EU:  https://apieu.samanage.com
    //   APJ: https://apiau.samanage.com
    BaseUrl = "https://api.samanage.com",
    AccessToken = "YOUR_SERVICEDESK_ACCESS_TOKEN"
};

var serviceDeskClient = new SolarWindsServiceDeskClient(serviceDeskOptions);
```

### Making API Calls

#### SolarWinds Orion API Examples

##### Get Nodes

```csharp
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

// Get all nodes
var allNodes = await client.GetAllAsync<Node>();

// Get nodes with a filter
var filteredNodes = await client.FilterQueryAsync(new FilterQuery<Node>
{
    Constraints = new List<Constraint>
    {
        new Eq(nameof(Node.Status), 1) // Filter by status (e.g., Up)
    },
    OrderBy = nameof(Node.Caption),
    Take = 10
});

// Get nodes using SQL query
var sqlNodes = await client.SqlQueryAsync<Node>(new SqlQuery
{
    Sql = "SELECT TOP 5 Caption, IPAddress FROM Orion.Nodes WHERE Status = @status",
    Parameters = new Dictionary<string, object> { { "status", 1 } }
});
```

##### Get Custom Properties

```csharp
using SolarWinds.Api.Orion;
using SolarWinds.Api.Queries;

// Get all custom properties
var customProperties = await client.GetAllAsync<CustomProperty>();

// Get custom property values
var customPropertyValues = await client.GetAllAsync<CustomPropertyValue>();
```

#### SolarWinds Service Desk API Examples

##### Get Incidents

```csharp
using SolarWinds.Api.ServiceDesk.Models;

// Get incidents using a query request
var allIncidents = await serviceDeskClient.Incidents.GetAsync(new GetIncidentsRequest
{
    Layout = ResponseLayout.Short,
    Page = 1,
    PerPage = 50
}, CancellationToken.None);

// Get a specific incident by ID (layout is required)
var incident = await serviceDeskClient.Incidents.GetAsync(123, ResponseLayout.Long, CancellationToken.None);
```

##### Get Users

```csharp
using SolarWinds.Api.ServiceDesk.Models;

// Get all users
var allUsers = await serviceDeskClient.Users.GetAllAsync();

// Get a specific user by ID (layout is required)
var user = await serviceDeskClient.Users.GetAsync(456, ResponseLayout.Short, CancellationToken.None);
```

## Breaking Changes (Current Migration Wave)

This release contains wide-reaching breaking changes across the Service Desk client to align with official API behavior and payload shapes.

### What changed

- GET-by-ID methods now require layout explicitly:
    - `GetAsync(int id, ResponseLayout layout, CancellationToken cancellationToken)`
- Object-scoped endpoints no longer take free-form strings for `objectType`:
    - now use `ObjectType objectType` enum values (serialized via `EnumMember` string mappings).
- Many write operations now require wrapper request payloads instead of raw entity bodies:
    - examples: `IncidentCreateRequest`, `IncidentUpdateRequest`, `CategoryCreateRequest`, `ContractUpdateRequest`, and similar domain-specific request models.
- List/query reads in migrated domains now use typed query request models:
    - examples: `GetIncidentsRequest`, `GetChangesRequest`, `GetContractsRequest`.
- Legacy convenience signatures are being removed as domains are migrated:
    - examples: `GetAllAsync(...)`, `GetPageAsync(...)`.

### Why this changed

- To match Service Desk OpenAPI paths and payload roots.
- To make request intent explicit and strongly typed.
- To reduce ambiguous signatures and reduce runtime shape mismatches.

## Service Desk Migration Guide

Use this as a checklist when upgrading existing code.

### 1) Update GET-by-ID calls to include layout

Before:

```csharp
var category = await serviceDeskClient.Categories.GetAsync(categoryId, cancellationToken);
```

After:

```csharp
var category = await serviceDeskClient.Categories.GetAsync(categoryId, ResponseLayout.Short, cancellationToken);
```

### 2) Replace string objectType with ObjectType enum

Before:

```csharp
var comment = await serviceDeskClient.Comments.CreateAsync("incidents", incidentId, request, cancellationToken);
```

After:

```csharp
var comment = await serviceDeskClient.Comments.CreateAsync(ObjectType.Incidents, incidentId, request, cancellationToken);
```

### 3) Replace raw write bodies with wrapper requests

Before:

```csharp
var updated = await serviceDeskClient.Incidents.UpdateAsync(incidentId, incident, cancellationToken);
```

After:

```csharp
var updated = await serviceDeskClient.Incidents.UpdateAsync(
        incidentId,
        new IncidentUpdateRequest
        {
                Incident = new IncidentWriteFields
                {
                        Name = "Updated title"
                }
        },
        cancellationToken);
```

### 4) Use typed query requests for list endpoints

Before:

```csharp
var changes = await serviceDeskClient.Changes.GetAllAsync(cancellationToken);
```

After:

```csharp
var changes = await serviceDeskClient.Changes.GetAsync(new GetChangesRequest
{
        Layout = ResponseLayout.Long
}, cancellationToken);
```

### 5) Confirm date and enum serialization assumptions

- Date query values use `MMM d yyyy` formatting.
- Enums are serialized using `EnumMember` values where present.

### 6) Upgrade strategy for consumers

- Compile and fix all Service Desk call sites by signature.
- Start with read-path calls (`GetAsync`) then write-path wrappers.
- Run integration tests against your tenant, especially workflows touching category/group/department payloads.

### Querying

The library provides flexible querying capabilities for the Orion API using `FilterQuery` and `SqlQuery`.

#### `FilterQuery<T>`

Allows filtering, ordering, and pagination using strongly-typed properties.

```csharp
using SolarWinds.Api.Queries;
using SolarWinds.Api.Orion;
using System.Collections.Generic;

var query = new FilterQuery<Node>
{
    Constraints = new List<Constraint>
    {
        new Gt(nameof(Node.CpuLoad), 50), // CPU Load greater than 50
        new Le(nameof(Node.MemoryUsed), 80) // Memory Used less than or equal to 80
    },
    OrderBy = nameof(Node.Caption),
    Skip = 0,
    Take = 20
};

var result = await client.FilterQueryAsync(query);
```

Supported constraints: `Eq`, `Ne`, `Gt`, `Ge`, `Lt`, `Le`.

#### `SqlQuery`

For more complex queries, you can use raw SWQL (SolarWinds Query Language).

```csharp
using SolarWinds.Api.Queries;
using System.Collections.Generic;

var sqlQuery = new SqlQuery
{
    Sql = "SELECT N.Caption, N.IPAddress, CP.Value FROM Orion.Nodes N JOIN Orion.NodesCustomProperties CP ON N.NodeID = CP.NodeID WHERE CP.CustomPropertyID = @cpId",
    Parameters = new Dictionary<string, object> { { "cpId", 123 } }
};

var result = await client.SqlQueryAsync<dynamic>(sqlQuery); // You can use dynamic or a custom class
```

### Error Handling

API calls may throw `SolarWindsApiException` for API-specific errors or `SolarWindsApiHttpException` for HTTP-related errors.

```csharp
using SolarWinds.Api.Exceptions;
using System;
using System.Net.Http;

try
{
    // Your API call here
    await client.GetAllAsync<Node>();
}
catch (SolarWindsApiHttpException ex)
{
    Console.WriteLine($"HTTP Error: {ex.StatusCode} - {ex.Message}");
    Console.WriteLine($"Response Body: {ex.Content}");
}
catch (SolarWindsApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"Network Error: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
}
```

## SolarWinds Service Desk API Reference

- [API Documentation](https://apidoc.samanage.com/#section/General-Concepts)
- Authentication header: `X-Samanage-Authorization: Bearer <token>`
- Accept header: `application/vnd.samanage.v2.1+json`
- Regional base URLs:
  | Region | Base URL |
  |--------|----------|
  | US     | `https://api.samanage.com` |
  | EU     | `https://apieu.samanage.com` |
  | APJ    | `https://apiau.samanage.com` |

### Test Credentials

The test project uses [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) to store credentials. To run the ServiceDesk integration tests:

```bash
cd SolarWinds.Api.Test
dotnet user-secrets set "ServiceDesk:BaseUrl" "https://api.samanage.com"
dotnet user-secrets set "ServiceDesk:AccessToken" "<your-token>"
```

## Contributing

Contributions are welcome! Please feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.