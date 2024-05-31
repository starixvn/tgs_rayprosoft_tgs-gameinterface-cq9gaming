using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.CQ9Gaming.Core.Extensions;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest;

public sealed class GameApiClient_OrderViewTest
{
	[Fact]
	public async Task OrderViewAsync_RespondStatusCode0_ResultItems_Slot()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-30);
		var endTime = DateTimeOffset.UtcNow;
		var page = Random.Shared.Next(10);
		var pageSize = Random.Shared.Next(500, 2000);
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, "/gameboy/order/view").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactQueryString(new SortedDictionary<string, string>
			{
				["starttime"] = startTime.DatetimeConvertToString(),
				["endtime"] = endTime.DatetimeConvertToString(),
				["page"] = page.ToString(),
				["pagesize"] = pageSize.ToString(),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["TotalSize"] = 1,
					["Data"] = new JsonArray
					{
						new JsonObject
						{
							["gamehall"] = "cq9",
							["gametype"] = "slot",
							["gameplat"] = "web",
							["gamecode"] = "160",
							["account"] = "4ucmqplxb029",
							["round"] = "6230414220",
							["balance"] = 364.24,
							["win"] = 216,
							["bet"] = 0.76,
							["validbet"] = 0,
							["jackpot"] = 0,
							["jackpotcontribution"] = new JsonArray(),
							["jackpottype"] = "",
							["status"] = "complete",
							["endroundtime"] = "2024-05-29T02:39:40.143-04:00",
							["createtime"] = "2024-05-29T02:39:40.099-04:00",
							["bettime"] = "2024-05-29T02:37:50.907-04:00",
							["detail"] = new JsonArray{
								new JsonObject{
									["freegame"] = 0,
								},
								new JsonObject{
									[ "luckydraw"] = 0,
								},
								new JsonObject{
									[ "bonus"] = 0,
								}
							},
							["singlerowbet"] =false,
							["gamerole"] ="",
							["bankertype"] ="",
							["rake"] = 0,
							["roomfee"] = 0,
							["tabletype"] = "",
							["tableid"] = "",
							["roundnumber"] = "",
							["bettype"] = new JsonArray(),
							["gameresult"]= new JsonObject
							{
								["points"] = new JsonArray(),
								["cards"] = new JsonArray()
							},
							["currency"] = "CNY",
							["donate"] = 0,
							["isdonate"] = false,
						},
					},
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.OrderViewAsync(
			apiUrl,
			apiToken,
			startTime,
			endTime,
			page,
			pageSize)
			.ToArrayAsync()
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Single(result);
		var resultItem = result[0];
		Assert.Equal("4ucmqplxb029", resultItem.Account);
		Assert.Equal("6230414220", resultItem.Roundnumber);
		Assert.Equal("6230414220", resultItem.Round);
		Assert.Equal("cq9", resultItem.Gamehall);
		Assert.Equal("slot", resultItem.Gametype);
		Assert.Equal("web", resultItem.Gameplat);
		Assert.Equal("160", resultItem.Gamecode);
		Assert.Equal(0, resultItem.Freegame);
		Assert.Equal(0.76m, resultItem.Bet);
		Assert.Equal(0.0m, resultItem.ValidBet);
		Assert.Equal(216.0m, resultItem.Win);
		Assert.Equal(0.0m, resultItem.Roomfee);
		Assert.Equal(0.0m, resultItem.Rake);
		Assert.Equal(0.0m, resultItem.Jackpot);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:37:50.907-04:00"), resultItem.Bettime);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:39:40.143-04:00"), resultItem.Endroundtime);
	}

	[Fact]
	public async Task OrderViewAsync_RespondStatusCode0_ResultItems_Slot_Freegame()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-30);
		var endTime = DateTimeOffset.UtcNow;
		var page = Random.Shared.Next(10);
		var pageSize = Random.Shared.Next(500, 2000);
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, "/gameboy/order/view").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactQueryString(new SortedDictionary<string, string>
			{
				["starttime"] = startTime.DatetimeConvertToString(),
				["endtime"] = endTime.DatetimeConvertToString(),
				["page"] = page.ToString(),
				["pagesize"] = pageSize.ToString(),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["TotalSize"] = 1,
					["Data"] = new JsonArray
					{
						new JsonObject
						{
							["gamehall"] = "cq9",
							["gametype"] = "slot",
							["gameplat"] = "web",
							["gamecode"] = "160",
							["account"] = "4ucmqplxb029",
							["round"] = "6230414220",
							["balance"] = 1142.98,
							["win"] = 192.4,
							["bet"] = 0.76,
							["validbet"] = 0,
							["jackpot"] = 0,
							["jackpotcontribution"] = new JsonArray(),
							["jackpottype"] = "",
							["status"] = "complete",
							["endroundtime"] = "2024-05-29T02:39:40.143-04:00",
							["createtime"] = "2024-05-29T02:39:40.099-04:00",
							["bettime"] = "2024-05-29T02:37:50.907-04:00",
							["detail"] = new JsonArray{
								new JsonObject{
									["freegame"] = 18,
								},
								new JsonObject{
									[ "luckydraw"] = 0,
								},
								new JsonObject{
									[ "bonus"] = 0,
								}
							},
							["singlerowbet"] =false,
							["gamerole"] ="",
							["bankertype"] ="",
							["rake"] = 0,
							["roomfee"] = 0,
							["tabletype"] = "",
							["tableid"] = "",
							["roundnumber"] = "",
							["bettype"] = new JsonArray(),
							["gameresult"]= new JsonObject
							{
								["points"] = new JsonArray(),
								["cards"] = new JsonArray()
							},
							["currency"] = "CNY",
							["donate"] = 0,
							["isdonate"] = false,
						},
					},
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.OrderViewAsync(
			apiUrl,
			apiToken,
			startTime,
			endTime,
			page,
			pageSize)
			.ToArrayAsync()
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Single(result);
		var resultItem = result[0];
		Assert.Equal("4ucmqplxb029", resultItem.Account);
		Assert.Equal("6230414220", resultItem.Roundnumber);
		Assert.Equal("6230414220", resultItem.Round);
		Assert.Equal("cq9", resultItem.Gamehall);
		Assert.Equal("slot", resultItem.Gametype);
		Assert.Equal("web", resultItem.Gameplat);
		Assert.Equal("160", resultItem.Gamecode);
		Assert.Equal(18, resultItem.Freegame);
		Assert.Equal(0.76m, resultItem.Bet);
		Assert.Equal(0.0m, resultItem.ValidBet);
		Assert.Equal(192.4m, resultItem.Win);
		Assert.Equal(0.0m, resultItem.Roomfee);
		Assert.Equal(0.0m, resultItem.Rake);
		Assert.Equal(0.0m, resultItem.Jackpot);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:37:50.907-04:00"), resultItem.Bettime);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:39:40.143-04:00"), resultItem.Endroundtime);
	}

	[Fact]
	public async Task OrderViewAsync_RespondStatusCode0_ResultItems_Arcade()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-30);
		var endTime = DateTimeOffset.UtcNow;
		var page = Random.Shared.Next(10);
		var pageSize = Random.Shared.Next(500, 2000);
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, "/gameboy/order/view").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactQueryString(new SortedDictionary<string, string>
			{
				["starttime"] = startTime.DatetimeConvertToString(),
				["endtime"] = endTime.DatetimeConvertToString(),
				["page"] = page.ToString(),
				["pagesize"] = pageSize.ToString(),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["TotalSize"] = 1,
					["Data"] = new JsonArray
					{
						new JsonObject
						{
							["gamehall"] = "cq9",
							["gametype"] = "arcade",
							["gameplat"] = "web",
							["gamecode"] = "207",
							["account"] = "test123",
							["round"] = "6228203961",
							["balance"] = 165,
							["win"] = 0,
							["bet"] = 9,
							["validbet"] = 0,
							["jackpot"] = 0,
							["jackpotcontribution"] = new JsonArray(),
							["jackpottype"] = "",
							["status"] = "complete",
							["endroundtime"] = "2024-05-27T04:45:31.72-04:00",
							["createtime"] = "2024-05-27T04:45:31.696-04:00",
							["bettime"] = "2024-05-27T04:45:15.047-04:00",
							["detail"] = new JsonArray{
								new JsonObject{
									["freegame"] = 0,
								},
								new JsonObject{
									[ "luckydraw"] = 0,
								},
								new JsonObject{
									[ "bonus"] = 0,
								}
							},
							["singlerowbet"] =false,
							["gamerole"] ="",
							["bankertype"] ="",
							["rake"] = 0,
							["roomfee"] = 0,
							["tabletype"] = "",
							["tableid"] = "",
							["roundnumber"] = "",
							["bettype"] = new JsonArray(),
							["gameresult"]= new JsonObject
							{
								["points"] = new JsonArray(),
								["cards"] = new JsonArray()
							},
							["currency"] = "CNY",
							["donate"] = 0,
							["isdonate"] = false,
						},
					},
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.OrderViewAsync(
			apiUrl,
			apiToken,
			startTime,
			endTime,
			page,
			pageSize)
			.ToArrayAsync()
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Single(result);
		var resultItem = result[0];
		Assert.Equal("test123", resultItem.Account);
		Assert.Equal("6228203961", resultItem.Roundnumber);
		Assert.Equal("6228203961", resultItem.Round);
		Assert.Equal("cq9", resultItem.Gamehall);
		Assert.Equal("arcade", resultItem.Gametype);
		Assert.Equal("web", resultItem.Gameplat);
		Assert.Equal("207", resultItem.Gamecode);
		Assert.Equal(0, resultItem.Freegame);
		Assert.Equal(9.0m, resultItem.Bet);
		Assert.Equal(0.0m, resultItem.ValidBet);
		Assert.Equal(0.0m, resultItem.Win);
		Assert.Equal(0.0m, resultItem.Roomfee);
		Assert.Equal(0.0m, resultItem.Rake);
		Assert.Equal(0.0m, resultItem.Jackpot);
		Assert.Equal(DateTimeOffset.Parse("2024-05-27T04:45:15.047-04:00"), resultItem.Bettime);
		Assert.Equal(DateTimeOffset.Parse("2024-05-27T04:45:31.72-04:00"), resultItem.Endroundtime);
	}

	[Fact]
	public async Task OrderViewAsync_RespondStatusCode0_ResultItems_Table()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-30);
		var endTime = DateTimeOffset.UtcNow;
		var page = Random.Shared.Next(10);
		var pageSize = Random.Shared.Next(500, 2000);
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, "/gameboy/order/view").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactQueryString(new SortedDictionary<string, string>
			{
				["starttime"] = startTime.DatetimeConvertToString(),
				["endtime"] = endTime.DatetimeConvertToString(),
				["page"] = page.ToString(),
				["pagesize"] = pageSize.ToString(),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["TotalSize"] = 1,
					["Data"] = new JsonArray
					{
						new JsonObject
						{
							["gamehall"] = "cq9",
							["gametype"] = "table",
							["gameplat"] = "web",
							["gamecode"] = "GO03",
							["account"] = "test123",
							["round"] = "GO03m1048279m1m1",
							["balance"] = 185,
							["win"] = -15,
							["bet"] = 15,
							["validbet"] = 15,
							["jackpot"] = 0,
							["jackpotcontribution"] = new JsonArray(),
							["jackpottype"] = "",
							["status"] = "complete",
							["endroundtime"] = "2024-05-27T04:04:32.85-04:00",
							["createtime"] = "2024-05-27T04:04:32.796-04:00",
							["bettime"] = "2024-05-27T04:04:12.497-04:00",
							["detail"] = new JsonArray(),
							["singlerowbet"] =false,
							["gamerole"] ="banker",
							["bankertype"] ="pc",
							["rake"] = 0,
							["roomfee"] = 0,
							["tabletype"] = "-",
							["tableid"] = "-",
							["roundnumber"] = "",
							["bettype"] = new JsonArray(),
							["gameresult"]= new JsonObject
							{
								["points"] = new JsonArray(),
								["cards"] = new JsonArray()
							},
							["currency"] = "CNY",
							["donate"] = 0,
							["isdonate"] = false,
						},
					},
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.OrderViewAsync(
			apiUrl,
			apiToken,
			startTime,
			endTime,
			page,
			pageSize)
			.ToArrayAsync()
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Single(result);
		var resultItem = result[0];
		Assert.Equal("test123", resultItem.Account);
		Assert.Equal("GO03m1048279m1m1", resultItem.Roundnumber);
		Assert.Equal("GO03m1048279m1m1", resultItem.Round);
		Assert.Equal("cq9", resultItem.Gamehall);
		Assert.Equal("table", resultItem.Gametype);
		Assert.Equal("web", resultItem.Gameplat);
		Assert.Equal("GO03", resultItem.Gamecode);
		Assert.Equal(0, resultItem.Freegame);
		Assert.Equal(15.0m, resultItem.Bet);
		Assert.Equal(15.0m, resultItem.ValidBet);
		Assert.Equal(-15.0m, resultItem.Win);
		Assert.Equal(0.0m, resultItem.Roomfee);
		Assert.Equal(0.0m, resultItem.Rake);
		Assert.Equal(0.0m, resultItem.Jackpot);
		Assert.Equal(DateTimeOffset.Parse("2024-05-27T04:04:12.497-04:00"), resultItem.Bettime);
		Assert.Equal(DateTimeOffset.Parse("2024-05-27T04:04:32.85-04:00"), resultItem.Endroundtime);
	}

	[Fact]
	public async Task OrderViewAsync_RespondStatusCode0_ResultEmty()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-30);
		var endTime = DateTimeOffset.UtcNow;
		var page = Random.Shared.Next(10);
		var pageSize = Random.Shared.Next(500, 2000);
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, "/gameboy/order/view").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactQueryString(new SortedDictionary<string, string>
			{
				["starttime"] = startTime.DatetimeConvertToString(),
				["endtime"] = endTime.DatetimeConvertToString(),
				["page"] = page.ToString(),
				["pagesize"] = pageSize.ToString(),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["TotalSize"] = 0,
					["Data"] = new JsonArray(),
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.OrderViewAsync(
			apiUrl,
			apiToken,
			startTime,
			endTime,
			page,
			pageSize)
			.ToArrayAsync()
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Empty(result);
	}

	[Fact]
	public async Task OrderViewAsync_RespondStatusCode0_ResultItems_Live()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-30);
		var endTime = DateTimeOffset.UtcNow;
		var page = Random.Shared.Next(10);
		var pageSize = Random.Shared.Next(500, 2000);

		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Get, new Uri(apiUrl, "/gameboy/order/view").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactQueryString(new SortedDictionary<string, string>
			{
				["starttime"] = startTime.DatetimeConvertToString(),
				["endtime"] = endTime.DatetimeConvertToString(),
				["page"] = page.ToString(),
				["pagesize"] = pageSize.ToString(),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["TotalSize"] = 1,
					["Data"] = new JsonArray
					{
						new JsonObject
						{
							["gamehall"] = "cq9",
							["gametype"] = "live",
							["gameplat"] = "web",
							["gamecode"] = "GINKGO01",
							["account"] = "4ucmqplxb029",
							["round"] = "GINKGO240528220000007",
							["balance"] = 149,
							["win"] = 10,
							["bet"] = 10,
							["validbet"] = 10,
							["jackpot"] = 0,
							["jackpotcontribution"] = new JsonArray(),
							["jackpottype"] = "",
							["status"] = "complete",
							["endroundtime"] = "2024-05-28T22:48:20.441-04:00",
							["createtime"] = "2024-05-28T22:48:20.401-04:00",
							["bettime"] = "2024-05-28T22:48:03.802-04:00",
							["detail"] = new JsonArray(),
							["singlerowbet"] = false,
							["gamerole"] = "player",
							["bankertype"] = "pc",
							["rake"] = 0,
							["roomfee"] = 0,
							["tabletype"] = 1,
							["tableid"] = "AET02",
							["roundnumber"] = "10014289",
							["bettype"] = new JsonArray{ "2" },
							["gameresult"] = new JsonObject
							{
								["points"] = new JsonArray{ "2","8" },
								["cards"] = new JsonArray{
									new JsonObject
									{
										["poker"] = "H13",
										["tag"] = 2,
									},
									new JsonObject
									{
										["poker"] = "D3",
										["tag"] = 1,
									},
									new JsonObject
									{
										["poker"] = "D8",
										["tag"] = 2,
									},
									new JsonObject
									{
										["poker"] = "C9",
										["tag"] = 1,
									}
								},
							},
							["points"] = new JsonArray{
								new JsonObject{
									["bettype"] = "2",
									["win"] = 20,
									["winlose"] = 10,
									["bet"] = 10,
									["validbet"] = 10,
									["odds"] = 1,
									["matchresult"] = "2",
								}
							},
							["currency"] = "CNY",
							["donate"] = 0,
							["isdonate"] = false,
						},
					},
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				},
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.OrderViewAsync(
			apiUrl,
			apiToken,
			startTime,
			endTime,
			page,
			pageSize)
			.ToArrayAsync()
			.ConfigureAwait(true);

		Assert.NotNull(result);
		Assert.Single(result);
		var resultItem = result[0];
		Assert.Equal("4ucmqplxb029", resultItem.Account);
		Assert.Equal("10014289", resultItem.Roundnumber);
		Assert.Equal("GINKGO240528220000007", resultItem.Round);
		Assert.Equal("cq9", resultItem.Gamehall);
		Assert.Equal("live", resultItem.Gametype);
		Assert.Equal("web", resultItem.Gameplat);
		Assert.Equal("GINKGO01", resultItem.Gamecode);
		Assert.Equal(0, resultItem.Freegame);
		Assert.Equal(10.0m, resultItem.Bet);
		Assert.Equal(10.0m, resultItem.ValidBet);
		Assert.Equal(10.0m, resultItem.Win);
		Assert.Equal(0.0m, resultItem.Roomfee);
		Assert.Equal(0.0m, resultItem.Rake);
		Assert.Equal(0.0m, resultItem.Jackpot);
		Assert.Equal(DateTimeOffset.Parse("2024-05-28T22:48:03.802-04:00"), resultItem.Bettime);
		Assert.Equal(DateTimeOffset.Parse("2024-05-28T22:48:20.441-04:00"), resultItem.Endroundtime);
	}
}
