using System.Text.Json;
using System.Text.Json.Nodes;

namespace SolarWinds.Api.OpenApi;

internal static partial class Program
{
	private static readonly JsonSerializerOptions JsonOptions = new()
	{
		WriteIndented = true
	};

	public static int Main(string[] args)
	{
		try
		{
			var outputPath = ResolveOutputPath(ParseOutputPath(args));
			var document = BuildOpenApiDocument();
			Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
			File.WriteAllText(outputPath, document.ToJsonString(JsonOptions));
			Console.WriteLine($"Generated OpenAPI document: {outputPath}");
			return 0;
		}
		catch (Exception ex)
		{
			Console.Error.WriteLine("Failed to generate OpenAPI document.");
			Console.Error.WriteLine(ex);
			return 1;
		}
	}

	private static string ResolveOutputPath(string? outputPath)
	{
		if (!string.IsNullOrWhiteSpace(outputPath))
		{
			return Path.GetFullPath(outputPath);
		}

		var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));
		return Path.Combine(repoRoot, "SolarWinds.ServiceDesk.OpenApi.json");
	}

	private static JsonObject BuildOpenApiDocument()
	{
		var paths = new JsonObject();
		var schemas = new JsonObject();
		var documentTags = new SortedSet<string>(StringComparer.Ordinal);
		var schemaBuilder = new SchemaBuilder(schemas);

		var interfaces = typeof(SolarWindsServiceDeskClient).Assembly.GetTypes()
			.Where(static t =>
				t.IsInterface &&
				t.Namespace is "SolarWinds.Api.ServiceDesk.Interfaces")
			.OrderBy(static t => t.Name, StringComparer.Ordinal)
			.ToArray();

		foreach (var iface in interfaces)
		{
			BuildInterfaceOperations(iface, paths, schemaBuilder, documentTags);
		}

		return new JsonObject
		{
			["openapi"] = "3.1.1",
			["info"] = BuildInfo(),
			["servers"] = BuildServers(),
			["paths"] = paths,
			["tags"] = BuildTags(documentTags),
			["components"] = BuildComponents(schemas),
			["security"] = BuildSecurityRequirement()
		};
	}

	private static JsonObject BuildInfo()
		=> new()
		{
			["title"] = "SolarWinds Service Desk API",
			["version"] = GetApiVersion(),
			["description"] = "Generated from SolarWinds.Api Refit interfaces and models."
		};

	private static JsonArray BuildServers()
		=> new()
		{
			new JsonObject
			{
				["url"] = "https://api.samanage.com",
				["description"] = "SolarWinds Service Desk (US)"
			},
			new JsonObject
			{
				["url"] = "https://apieu.samanage.com",
				["description"] = "SolarWinds Service Desk (EU)"
			},
			new JsonObject
			{
				["url"] = "https://apiau.samanage.com",
				["description"] = "SolarWinds Service Desk (APJ)"
			}
		};

	private static JsonArray BuildTags(IEnumerable<string> documentTags)
	{
		var tags = new JsonArray();
		foreach (var tag in documentTags)
		{
			tags.Add(new JsonObject
			{
				["name"] = tag
			});
		}

		return tags;
	}

	private static JsonObject BuildComponents(JsonObject schemas)
		=> new()
		{
			["schemas"] = schemas,
			["securitySchemes"] = new JsonObject
			{
				["X-Samanage-Authorization"] = new JsonObject
				{
					["type"] = "apiKey",
					["in"] = "header",
					["name"] = "X-Samanage-Authorization",
					["description"] = "Service Desk API token header, e.g. Bearer <token>."
				}
			}
		};

	private static JsonArray BuildSecurityRequirement()
		=> new()
		{
			new JsonObject
			{
				["X-Samanage-Authorization"] = new JsonArray()
			}
		};

	private static string GetApiVersion()
	{
		// The NBGV AssemblyVersion is height-free (major.minor.0.0), so the document
		// version only changes when version.json does. Embedding git height here would
		// make every commit invalidate the checked-in document.
		var assemblyVersion = typeof(SolarWindsServiceDeskClient).Assembly.GetName().Version;
		return assemblyVersion is null ? "0.0" : assemblyVersion.ToString(2);
	}

	private static string? ParseOutputPath(string[] args)
		=> args switch
		{
			[] => null,
			[var outputPath] when !outputPath.StartsWith("--", StringComparison.Ordinal) => outputPath,
			_ => throw new ArgumentException("Usage: SolarWinds.Api.OpenApi [<output-path>]")
		};
}
