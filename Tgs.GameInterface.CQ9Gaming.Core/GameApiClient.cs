using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;
using Microsoft.Extensions.Logging;
using Tgs.GameInterface.CQ9Gaming.Core.Extensions;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient(
	ILogger<GameApiClient> logger,
	HttpClient httpClient)
	: IGameApiClient
{
	private const string DefaultPassword = "tgspassw0rd";

	private async ValueTask<JsonNode> GetWithParameterAsync(
		Uri apiUrl,
		string apiToken,
		IDictionary<string, string> parameters,
		string logPrefix,
		CancellationToken cancellationToken = default)
	{
		logPrefix = $"{nameof(GameApiClient).ToLower()}.{logPrefix}";

		if (parameters.Any())
		{
			var builder = new UriBuilder(apiUrl);
			var query = HttpUtility.ParseQueryString(builder.Query);
			foreach (var parameter in parameters)
				query[parameter.Key] = parameter.Value;
			builder.Query = query.ToString();
			apiUrl = builder.Uri;
		}

		httpClient.DefaultRequestHeaders.Clear();
		httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(apiToken);

		var sw = Stopwatch.StartNew();

		var response = await httpClient.GetAsync(
			requestUri: apiUrl,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		sw.Stop();

		var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

		var request = response.RequestMessage;

		var apiLogEvent = new
		{
			Path = request?.RequestUri?.AbsoluteUri.NoLongerThan(512) ?? string.Empty,
			Method = request?.Method.ToString().NoLongerThan(8) ?? string.Empty,
			Prefix = logPrefix,
			ServerName = Environment.MachineName.NoLongerThan(128),
			RequestHeaders = request?.Headers + request?.Content?.Headers?.ToString(),
			RequestContent = string.Empty,
			ResponseHeaders = response.Headers.ToString(),
			ResponseContent = responseContent.NoLongerThan(4096),
			ResponseCode = (short)response.StatusCode,
			ExecutionTimeMs = (int)sw.ElapsedMilliseconds,
			RecordedOn = DateTimeOffset.UtcNow
		};
		logger.LogInformation("{apiLogEvent}", JsonSerializer.Serialize(apiLogEvent));

		response.EnsureSuccessStatusCode();

		return JsonNode.Parse(responseContent)!;
	}

	private async ValueTask<JsonNode> GetAsync(
		Uri apiUrl,
		string apiToken,
		string logPrefix,
		CancellationToken cancellationToken = default)
	{
		logPrefix = $"{nameof(GameApiClient).ToLower()}.{logPrefix}";

		httpClient.DefaultRequestHeaders.Clear();
		httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(apiToken);

		var sw = Stopwatch.StartNew();

		var response = await httpClient.GetAsync(
			requestUri: apiUrl,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		sw.Stop();

		var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

		var request = response.RequestMessage;

		var apiLogEvent = new
		{
			Path = request?.RequestUri?.AbsoluteUri.NoLongerThan(512) ?? string.Empty,
			Method = request?.Method.ToString().NoLongerThan(8) ?? string.Empty,
			Prefix = logPrefix,
			ServerName = Environment.MachineName.NoLongerThan(128),
			RequestHeaders = request?.Headers + request?.Content?.Headers?.ToString(),
			RequestContent = string.Empty,
			ResponseHeaders = response.Headers.ToString(),
			ResponseContent = responseContent.NoLongerThan(4096),
			ResponseCode = (short)response.StatusCode,
			ExecutionTimeMs = (int)sw.ElapsedMilliseconds,
			RecordedOn = DateTimeOffset.UtcNow
		};
		logger.LogInformation("{apiLogEvent}", JsonSerializer.Serialize(apiLogEvent));

		response.EnsureSuccessStatusCode();

		return JsonNode.Parse(responseContent)!;
	}

	private async ValueTask<JsonNode> PostAsync(
		Uri apiUrl,
		string apiToken,
		IDictionary<string, string> parameters,
		string logPrefix,
		CancellationToken cancellationToken = default)
	{
		logPrefix = $"{nameof(GameApiClient).ToLower()}.{logPrefix}";

		var requestContent = new FormUrlEncodedContent(parameters);

		httpClient.DefaultRequestHeaders.Clear();
		httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(apiToken);

		var sw = Stopwatch.StartNew();

		var response = await httpClient.PostAsync(
			requestUri: apiUrl,
			content: requestContent,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		sw.Stop();

		var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

		var request = response.RequestMessage;

		var apiLogEvent = new
		{
			Path = request?.RequestUri?.AbsoluteUri.NoLongerThan(512) ?? string.Empty,
			Method = request?.Method.ToString().NoLongerThan(8) ?? string.Empty,
			Prefix = logPrefix,
			ServerName = Environment.MachineName.NoLongerThan(128),
			RequestHeaders = request?.Headers + request?.Content?.Headers?.ToString(),
			RequestContent = (await requestContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)).NoLongerThan(4096),
			ResponseHeaders = response.Headers.ToString(),
			ResponseContent = responseContent.NoLongerThan(4096),
			ResponseCode = (short)response.StatusCode,
			ExecutionTimeMs = (int)sw.ElapsedMilliseconds,
			RecordedOn = DateTimeOffset.UtcNow
		};
		logger.LogInformation("{apiLogEvent}", JsonSerializer.Serialize(apiLogEvent));

		response.EnsureSuccessStatusCode();

		return JsonNode.Parse(responseContent)!;
	}
}
