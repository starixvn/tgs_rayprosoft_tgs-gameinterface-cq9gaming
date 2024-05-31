using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest;

public sealed class GameApiClient_TransactionRecordTest
{
	[Fact]
	public async Task TransactionRecordAsync_RespondStatusCode0_ResultTrue()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var mtCode = Guid.NewGuid().ToString("N");
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, $"/gameboy/transaction/record/{mtCode}").ToString())
			.WithHeaders("Authorization", apiToken)
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["status"] = "success",
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.TransactionRecordAsync(
			apiUrl,
			apiToken,
			mtCode)
			.ConfigureAwait(true);

		Assert.True(result);
	}

	[Fact]
	public async Task TransactionRecordAsync_RespondStatusCode8_ResultTrue()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var mtCode = Guid.NewGuid().ToString("N");
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, $"/gameboy/transaction/record/{mtCode}").ToString())
			.WithHeaders("Authorization", apiToken)
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = null,
				["status"] = new JsonObject
				{
					["code"] = "8",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.TransactionRecordAsync(
			apiUrl,
			apiToken,
			mtCode)
			.ConfigureAwait(true);

		Assert.False(result);
	}

	[Fact]
	public async Task TransactionRecordAsync_RespondDataStatusFailed_ResultFalse()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var mtCode = Guid.NewGuid().ToString("N");
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, $"/gameboy/transaction/record/{mtCode}").ToString())
			.WithHeaders("Authorization", apiToken)
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["status"] = "failed",
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.TransactionRecordAsync(
			apiUrl,
			apiToken,
			mtCode)
			.ConfigureAwait(true);

		Assert.False(result);
	}

	[Fact]
	public async Task TransactionRecordAsync_RespondDataStatusProcessing_TaskFaultedException()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var mtCode = Guid.NewGuid().ToString("N");
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, $"/gameboy/transaction/record/{mtCode}").ToString())
			.WithHeaders("Authorization", apiToken)
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["status"] = "processing",
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var exception = await Assert.ThrowsAnyAsync<Exception>(async ()
			=> await apiClient.TransactionRecordAsync(
				apiUrl,
				apiToken,
				mtCode)
				.ConfigureAwait(true))
			.ConfigureAwait(true);
	}
}
