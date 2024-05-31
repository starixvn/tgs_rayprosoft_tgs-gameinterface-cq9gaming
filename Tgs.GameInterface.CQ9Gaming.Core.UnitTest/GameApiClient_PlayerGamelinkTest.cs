using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest;

public sealed class GameApiClient_PlayerGamelinkTest
{
	[Fact]
	public async Task PlayerGamelinkAsync_RespondStatusCode0_ResultPlayerGamelinkResult()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var userToken = Guid.NewGuid().ToString("N");
		var gameHall = Guid.NewGuid().ToString("N");
		var gameCode = Guid.NewGuid().ToString("N");
		var gamePlat = "mobile";
		var lang = "zh-cn";
		var gameToken = Guid.NewGuid().ToString("N");
		var gameUrl = "https://xxx.xxx.xxx/xxx";
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Post, new Uri(apiUrl, "/gameboy/player/gamelink").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactFormData(new SortedDictionary<string, string>
			{
				["usertoken"] = userToken,
				["gamehall"] = gameHall,
				["gamecode"] = gameCode,
				["gameplat"] = gamePlat,
				["lang"] = lang,
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["token"] = gameToken,
					["url"] = gameUrl,
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.PlayerGamelinkAsync(
			apiUrl,
			apiToken,
			userToken,
			gameHall,
			gameCode,
			gamePlat,
			lang)
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Equal(gameToken, result.Token);
		Assert.Equal(gameUrl, result.Url);
	}

	[Fact]
	public async Task PlayerGamelinkAsync_RespondStatusCode3_TaskFaultedErrorCodeException()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var userToken = Guid.NewGuid().ToString("N");
		var gameHall = Guid.NewGuid().ToString("N");
		var gameCode = Guid.NewGuid().ToString("N");
		var gamePlat = "mobile";
		var lang = "zh-cn";
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Post, new Uri(apiUrl, "/gameboy/player/gamelink").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactFormData(new SortedDictionary<string, string>
			{
				["usertoken"] = userToken,
				["gamehall"] = gameHall,
				["gamecode"] = gameCode,
				["gameplat"] = gamePlat,
				["lang"] = lang,
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = null,
				["status"] = new JsonObject
				{
					["code"] = "3",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var exception = await Assert.ThrowsAnyAsync<ErrorCodeException>(async ()
			=> await apiClient.PlayerGamelinkAsync(
				apiUrl,
				apiToken,
				userToken,
				gameHall,
				gameCode,
				gamePlat,
				lang)
				.ConfigureAwait(true))
			.ConfigureAwait(true);

		Assert.Equal("3", exception.Code);
	}
}
