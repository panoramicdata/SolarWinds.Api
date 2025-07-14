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
		ArgumentNullException.ThrowIfNull(hostname);
		ArgumentException.ThrowIfNullOrEmpty(hostname);

		ArgumentNullException.ThrowIfNull(username);
		ArgumentException.ThrowIfNullOrEmpty(username);

		ArgumentNullException.ThrowIfNull(password);
		ArgumentException.ThrowIfNullOrEmpty(password);

		ArgumentOutOfRangeException.ThrowIfNegativeOrZero(port);

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

	private bool RelaxedCertificateCheck(HttpRequestMessage arg1, X509Certificate2? arg2, X509Chain? arg3, SslPolicyErrors arg4) => true;

	public Task<QueryResponse<T>> FilterQueryAsync<T>(FilterQuery<T> filterQuery, CancellationToken cancellationToken) where T : Entity
		=> SqlQueryAsyncInternal<T>(filterQuery.GetSqlQuery(), cancellationToken);

	public Task<QueryResponse<T>> SqlQueryAsync<T>(SqlQuery query, CancellationToken cancellationToken) where T : Entity
		=> SqlQueryAsyncInternal<T>(query, cancellationToken);

	public Task<QueryResponse<JObject>> SqlJObjectQueryAsync(SqlQuery query, CancellationToken cancellationToken)
		=> SqlQueryAsyncInternal<JObject>(query, cancellationToken);

	private async Task<QueryResponse<T>> SqlQueryAsyncInternal<T>(SqlQuery query, CancellationToken cancellationToken)
	{
		// Input checking
		ArgumentNullException.ThrowIfNull(query);
		ArgumentNullException.ThrowIfNull(query.Sql);
		ArgumentException.ThrowIfNullOrEmpty(query.Sql);

		// Execute query
		var content = new StringContent(JsonConvert.SerializeObject(query), Encoding.UTF8, "application/json");
		var httpResponseMessage = await _httpClient.PostAsync("", content, cancellationToken);

		// OK response?
		if (!httpResponseMessage.IsSuccessStatusCode)
		{
			throw new SolarWindsApiHttpException(httpResponseMessage);
		}

		// Yes.  Deserialize.
		var responseBody = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
		QueryResponse<T>? result;
		try
		{
			result = JsonConvert.DeserializeObject<QueryResponse<T>>(
				responseBody,
				new JsonSerializerSettings
				{
#if DEBUG
					MissingMemberHandling = MissingMemberHandling.Error,
#endif
					TypeNameHandling = TypeNameHandling.Auto
				});
		}
		catch (Exception e)
		{
			throw new SolarWindsApiDeserializationException("Failed to deserialize in QueryAsync", e);
		}

		// Return the result
		return result ?? throw new SolarWindsApiDeserializationException("Deserialization returned null result");
	}
}
