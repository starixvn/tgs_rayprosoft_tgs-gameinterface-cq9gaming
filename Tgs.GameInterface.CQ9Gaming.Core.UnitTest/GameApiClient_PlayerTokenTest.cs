using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest;

public sealed class GameApiClient_PlayerTokenTest
{
	[Fact]
	public async Task PlayerTokenAsync_RespondStatusCode0_ResultUserToken()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var account = "4ucmqplxe65x";
		var status = "login";
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, $"/gameboy/player/token/{account}").ToString())
			.WithHeaders("Authorization", apiToken)
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["status"] = status,
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.PlayerTokenAsync(
			apiUrl,
			apiToken,
			account)
			.ConfigureAwait(true);

		Assert.Equal(status, result);
	}

	[Fact]
	public async Task PlayerTokenAsync_RespondStatusCode2_TaskFaultedErrorCodeException()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var account = "4ucmqplxe65x";
		var status = "login";
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, $"/gameboy/player/token/{account}").ToString())
			.WithHeaders("Authorization", apiToken)
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["status"] = status,
				},
				["status"] = new JsonObject
				{
					["code"] = "2",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var exception = await Assert.ThrowsAnyAsync<ErrorCodeException>(async ()
			=> await apiClient.PlayerTokenAsync(
				apiUrl,
				apiToken,
				account)
				.ConfigureAwait(true))
			.ConfigureAwait(true);

		Assert.Equal("2", exception.Code);
	}
}
