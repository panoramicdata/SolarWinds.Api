using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolarWinds.Api.Exceptions;
using SolarWinds.Api.Queries;

namespace SolarWinds.Api;

public class SolarWindsClient
{
	private readonly HttpClient _httpClient;

	public SolarWindsClient(
		string hostname,
		string username,
		string password,
		int port,
		bool ignoreSslCertificateErrors)
	{
		if (hostname == null)
		{
			throw new ArgumentNullException(nameof(hostname));
		}

		if (hostname?.Length == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(hostname), hostname, "Must not be empty.");
		}

		if (username == null)
		{
			throw new ArgumentNullException(nameof(username));
		}

		if (username?.Length == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(username), username, "Must not be empty.");
		}

		if (password == null)
		{
			throw new ArgumentNullException(nameof(password));
		}

		if (password?.Length == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(password), password, "Must not be empty.");
		}

		if (port <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(port), port, "Must be a number greater than 0.");
		}

		var handler = new HttpClientHandler
		{
			Credentials = new NetworkCredential(username, password),
		};
		if (ignoreSslCertificateErrors)
		{
			handler.ServerCertificateCustomValidationCallback = RelaxedCertificateCheck;
		}

		_httpClient = new HttpClient(handler)
		{
			BaseAddress = new Uri($"https://{hostname}:{port}/SolarWinds/InformationService/v3/Json/Query")
		};
	}

	private bool RelaxedCertificateCheck(HttpRequestMessage arg1, X509Certificate2 arg2, X509Chain arg3, SslPolicyErrors arg4) => true;

	public Task<QueryResponse<T>> FilterQueryAsync<T>(FilterQuery<T> filterQuery, CancellationToken cancellationToken) where T : Entity
		=> SqlQueryAsyncInternal<T>(filterQuery.GetSqlQuery(), cancellationToken);

	public Task<QueryResponse<T>> SqlQueryAsync<T>(SqlQuery query, CancellationToken cancellationToken) where T : Entity
		=> SqlQueryAsyncInternal<T>(query, cancellationToken);

	public Task<QueryResponse<JObject>> SqlJObjectQueryAsync(SqlQuery query, CancellationToken cancellationToken)
		=> SqlQueryAsyncInternal<JObject>(query, cancellationToken);

	private async Task<QueryResponse<T>> SqlQueryAsyncInternal<T>(SqlQuery query, CancellationToken cancellationToken)
	{
		// Input checking
		if (query == null)
		{
			throw new ArgumentNullException(nameof(query));
		}

		if (query.Sql == null)
		{
			throw new ArgumentException("Query SQL is null.", nameof(query));
		}

		if (query.Sql.Length == 0)
		{
			throw new ArgumentException("Query SQL is empty.", nameof(query));
		}

		// Execute query
		var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
		var httpResponseMessage = await _httpClient.PostAsync("", content, cancellationToken);

		// OK response?
		if (!httpResponseMessage.IsSuccessStatusCode)
		{
			throw new SolarWindsApiHttpException(httpResponseMessage);
		}

		// Yes.  Deserialize.
		var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
		QueryResponse<T> result;
		try
		{
			result = JsonConvert.DeserializeObject<QueryResponse<T>>(
				responseBody
				, new JsonSerializerSettings
				{
#if DEBUG
					MissingMemberHandling = MissingMemberHandling.Error,
					ContractResolver = new RequireObjectPropertiesContractResolver(),
#endif
					TypeNameHandling = TypeNameHandling.Auto
				});
		}
		catch (Exception e)
		{
			throw new SolarWindsApiDeserializationException("Failed to deserialize in QueryAsync", e);
		}

		// Return the result
		return result;
	}
}
