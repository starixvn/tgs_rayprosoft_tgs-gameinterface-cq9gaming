using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.Models;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class GameRecordBetLogQueryServiceTest
{
	[Fact]
	public async Task Handle_GameType_Slot_WinLossAndSettleAlgorithm()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var now = DateTimeOffset.UtcNow;
		var startTime = now.AddHours(-1);
		var endTime = now;

		var client = Substitute.For<IGameApiClient>();
		_ = client.OrderViewAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			startTime: Arg.Any<DateTimeOffset>(),
			endTime: Arg.Any<DateTimeOffset>(),
			page: Arg.Any<int>(),
			pageSize: Arg.Any<int>())
			.Returns(new[] { new OrderViewResponseItem(
				Account: "4ucmqplxb029",
				Roundnumber:"6230414220",
				Round: "6230414220",
				Gamehall: "cq9",
				Gametype: "slot",
				Gameplat: "web",
				Gamecode: "160",
				Freegame:0,
				Bet: 0.76m,
				ValidBet: 0.0m,
				Win: 216.0m,
				Roomfee: 0.0m,
				Rake: 0.0m,
				Jackpot: 0.0m,
				Bettime: DateTimeOffset.Parse("2024-05-29T02:09:01.619-04:00"),
				Endroundtime: DateTimeOffset.Parse("2024-05-29T02:09:11.813-04:00"),
				AdditionalData: new JsonObject
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
					["endroundtime"] = "2024-05-29T02:09:11.813-04:00",
					["createtime"] = "2024-05-29T02:09:11.762-04:00",
					["bettime"] = "2024-05-29T02:09:01.619-04:00",
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
				}.ToJsonString()) }.ToAsyncEnumerable());
		var service = new GameRecordBetLogQueryService(client);
		var query = new GameRecordBetLogQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			StartTime: startTime,
			EndTime: endTime);

		var response = await service.QueryAsync(query, default).ToArrayAsync().ConfigureAwait(true);

		Assert.NotNull(response);
		Assert.Single(response);
		var data = response[0];
		Assert.Equal("4ucmqplxb029", data.PlayerCode);
		Assert.Equal("6230414220", data.ExternalRoundId);
		Assert.Equal("6230414220", data.ExternalBetId);
		Assert.Equal("160", data.ExternalGameId);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:09:01.619-04:00"), data.ExternalCreatedOn);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:09:11.813-04:00"), data.ExternalClosedOn);
		Assert.False(data.IsFreegame);
		Assert.Equal(0.76m, data.Bet);
		Assert.Equal(0.76m, data.ValidBet);
		Assert.Equal(216.00m, data.Settle);
		Assert.Equal(215.24m, data.WinLoss);
		Assert.Equal(0.0m, data.Jackpot);
	}

	[Fact]
	public async Task Handle_GameType_Slot_WinLossAndSettleAlgorithm_Freegame()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var now = DateTimeOffset.UtcNow;
		var startTime = now.AddHours(-1);
		var endTime = now;

		var client = Substitute.For<IGameApiClient>();
		_ = client.OrderViewAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			startTime: Arg.Any<DateTimeOffset>(),
			endTime: Arg.Any<DateTimeOffset>(),
			page: Arg.Any<int>(),
			pageSize: Arg.Any<int>())
			.Returns(new[] { new OrderViewResponseItem(
				Account: "4ucmqplxb029",
				Roundnumber:"6230452578",
				Round: "6230452578",
				Gamehall: "cq9",
				Gametype: "slot",
				Gameplat: "web",
				Gamecode: "160",
				Freegame:18,
				Bet: 0.76m,
				ValidBet: 0.0m,
				Win: 192.4m,
				Roomfee: 0.0m,
				Rake: 0.0m,
				Jackpot: 0.0m,
				Bettime: DateTimeOffset.Parse("2024-05-29T02:37:50.907-04:00"),
				Endroundtime: DateTimeOffset.Parse("2024-05-29T02:39:40.143-04:00"),
				AdditionalData: new JsonObject
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
				}.ToJsonString()) }.ToAsyncEnumerable());
		var service = new GameRecordBetLogQueryService(client);
		var query = new GameRecordBetLogQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			StartTime: startTime,
			EndTime: endTime);

		var response = await service.QueryAsync(query, default).ToArrayAsync().ConfigureAwait(true);

		Assert.NotNull(response);
		Assert.Single(response);
		var data = response[0];
		Assert.Equal("4ucmqplxb029", data.PlayerCode);
		Assert.Equal("6230452578", data.ExternalRoundId);
		Assert.Equal("6230452578", data.ExternalBetId);
		Assert.Equal("160", data.ExternalGameId);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:37:50.907-04:00"), data.ExternalCreatedOn);
		Assert.Equal(DateTimeOffset.Parse("2024-05-29T02:39:40.143-04:00"), data.ExternalClosedOn);
		Assert.True(data.IsFreegame);
		Assert.Equal(0.76m, data.Bet);
		Assert.Equal(0.76m, data.ValidBet);
		Assert.Equal(192.4m, data.Settle);
		Assert.Equal(191.64m, data.WinLoss);
		Assert.Equal(0.0m, data.Jackpot);
	}

	[Fact]
	public async Task Handle_GameType_Arcade_WinLossAndSettleAlgorithm()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var now = DateTimeOffset.UtcNow;
		var startTime = now.AddHours(-1);
		var endTime = now;
		var betTime = now.AddMinutes(-30);
		var endRoundTime = betTime.AddMinutes(Random.Shared.Next(3, 5));
		var round = Guid.NewGuid().ToString("N");
		var gamecode = Guid.NewGuid().ToString("N");
		var account = "4ucmqplxe65x";
		var gametype = "arcade";
		var bet = 13m;
		var validBet = 0m;
		var jackpot = 0m;
		var rake = 0m;
		var roomfee = 0m;
		var win = 12m;
		var client = Substitute.For<IGameApiClient>();
		_ = client.OrderViewAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			startTime: Arg.Any<DateTimeOffset>(),
			endTime: Arg.Any<DateTimeOffset>(),
			page: Arg.Any<int>(),
			pageSize: Arg.Any<int>())
			.Returns(new[] { new OrderViewResponseItem(
				Account: account,
				Roundnumber:round,
				Round: round,
				Gamehall: Gamehall.CQ9,
				Gametype: gametype,
				Gameplat: "web",
				Gamecode: gamecode,
				Freegame:0,
				Bet: bet,
				ValidBet: validBet,
				Win: win,
				Roomfee: roomfee,
				Rake: rake,
				Jackpot: jackpot,
				Bettime: betTime,
				Endroundtime: endRoundTime,
				AdditionalData: string.Empty) }.ToAsyncEnumerable());
		var service = new GameRecordBetLogQueryService(client);
		var query = new GameRecordBetLogQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			StartTime: startTime,
			EndTime: endTime);

		var response = await service.QueryAsync(query, default).ToArrayAsync().ConfigureAwait(true);

		Assert.NotNull(response);
		Assert.Single(response);
		var data = response[0];
		Assert.Equal(-1m, data.WinLoss);
		Assert.Equal(12m, data.Settle);
	}

	[Fact]
	public async Task Handle_GameType_Live_WinLossAndSettleAlgorithm()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var now = DateTimeOffset.UtcNow;
		var startTime = now.AddHours(-1);
		var endTime = now;

		var client = Substitute.For<IGameApiClient>();
		_ = client.OrderViewAsync(
			apiUrl: Arg.Is(apiUrl),
			apiToken: Arg.Is(apiToken),
			startTime: Arg.Is(startTime),
			endTime: Arg.Is(endTime),
			page: Arg.Any<int>(),
			pageSize: Arg.Any<int>())
			.Returns(new[] { new OrderViewResponseItem(
				Account: "4ucmqplxb029",
				Roundnumber:"10014289",
				Round: "GINKGO240528220000007",
				Gamehall: "cq9",
				Gametype: "live",
				Gameplat: "web",
				Gamecode: "GINKGO01",
				Freegame:0,
				Bet: 10.0m,
				ValidBet: 10.0m,
				Win: 10.0m,
				Roomfee: 0.0m,
				Rake: 0.0m,
				Jackpot: 0.0m,
				Bettime: DateTimeOffset.Parse("2024-05-28T22:48:03.802-04:00"),
				Endroundtime: DateTimeOffset.Parse("2024-05-28T22:48:20.441-04:00"),
				AdditionalData: new JsonObject
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
				}.ToJsonString()) }.ToAsyncEnumerable());
		var service = new GameRecordBetLogQueryService(client);
		var query = new GameRecordBetLogQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			StartTime: startTime,
			EndTime: endTime);

		var response = await service.QueryAsync(query, default).ToArrayAsync().ConfigureAwait(true);

		Assert.NotNull(response);
		Assert.Single(response);
		var data = response[0];
		Assert.Equal("4ucmqplxb029", data.PlayerCode);
		Assert.Equal("10014289", data.ExternalRoundId);
		Assert.Equal("GINKGO240528220000007", data.ExternalBetId);
		Assert.Equal("GINKGO01", data.ExternalGameId);
		Assert.Equal(DateTimeOffset.Parse("2024-05-28T22:48:03.802-04:00"), data.ExternalCreatedOn);
		Assert.Equal(DateTimeOffset.Parse("2024-05-28T22:48:20.441-04:00"), data.ExternalClosedOn);
		Assert.False(data.IsFreegame);
		Assert.Equal(10.0m, data.Bet);
		Assert.Equal(10.0m, data.ValidBet);
		Assert.Equal(20.0m, data.Settle);
		Assert.Equal(10.0m, data.WinLoss);
		Assert.Equal(0.0m, data.Jackpot);
	}

	[Fact]
	public async Task Handle_GameType_Table_WinLossAndSettleAlgorithm()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var now = DateTimeOffset.UtcNow;
		var startTime = now.AddHours(-1);
		var endTime = now;
		var betTime = now.AddMinutes(-30);
		var endRoundTime = betTime.AddMinutes(Random.Shared.Next(3, 5));
		var round = Guid.NewGuid().ToString("N");
		var gamecode = Guid.NewGuid().ToString("N");
		var account = "4ucmqplxe65x";
		var gametype = "table";
		var bet = 15m;
		var validBet = 15m;
		var jackpot = 0m;
		var rake = 0m;
		var roomfee = 0m;
		var win = -15m;
		var client = Substitute.For<IGameApiClient>();
		_ = client.OrderViewAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			startTime: Arg.Any<DateTimeOffset>(),
			endTime: Arg.Any<DateTimeOffset>(),
			page: Arg.Any<int>(),
			pageSize: Arg.Any<int>())
			.Returns(new[] { new OrderViewResponseItem(
				Account: account,
				Roundnumber:round,
				Round: round,
				Gamehall: Gamehall.CQ9,
				Gametype: gametype,
				Gameplat: "web",
				Gamecode: gamecode,
				Freegame:0,
				Bet: bet,
				ValidBet: validBet,
				Win: win,
				Roomfee: roomfee,
				Rake: rake,
				Jackpot: jackpot,
				Bettime: betTime,
				Endroundtime: endRoundTime,
				AdditionalData: string.Empty) }.ToAsyncEnumerable());
		var service = new GameRecordBetLogQueryService(client);
		var query = new GameRecordBetLogQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			StartTime: startTime,
			EndTime: endTime);

		var response = await service.QueryAsync(query, default).ToArrayAsync().ConfigureAwait(true);

		Assert.NotNull(response);
		Assert.Single(response);
		var data = response[0];
		Assert.Equal(-15m, data.WinLoss);
		Assert.Equal(0m, data.Settle);
	}

	[Fact]
	public async Task Handle_GameType_Fish_WinLossAndSettleAlgorithm()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var now = DateTimeOffset.UtcNow;
		var startTime = now.AddHours(-1);
		var endTime = now;
		var betTime = now.AddMinutes(-30);
		var endRoundTime = betTime.AddMinutes(Random.Shared.Next(3, 5));
		var round = Guid.NewGuid().ToString("N");
		var gamecode = Guid.NewGuid().ToString("N");
		var account = "4ucmqplxe65x";
		var gametype = "fish";
		var bet = 126m;
		var validBet = 0m;
		var jackpot = 0m;
		var rake = 0m;
		var roomfee = 0m;
		var win = 26m;
		var client = Substitute.For<IGameApiClient>();
		_ = client.OrderViewAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			startTime: Arg.Any<DateTimeOffset>(),
			endTime: Arg.Any<DateTimeOffset>(),
			page: Arg.Any<int>(),
			pageSize: Arg.Any<int>())
			.Returns(new[] { new OrderViewResponseItem(
				Account: account,
				Roundnumber:round,
				Round: round,
				Gamehall: Gamehall.CQ9,
				Gametype: gametype,
				Gameplat: "web",
				Gamecode: gamecode,
				Freegame:0,
				Bet: bet,
				ValidBet: validBet,
				Win: win,
				Roomfee: roomfee,
				Rake: rake,
				Jackpot: jackpot,
				Bettime: betTime,
				Endroundtime: endRoundTime,
				AdditionalData: string.Empty) }.ToAsyncEnumerable());
		var service = new GameRecordBetLogQueryService(client);
		var query = new GameRecordBetLogQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			StartTime: startTime,
			EndTime: endTime);

		var response = await service.QueryAsync(query, default).ToArrayAsync().ConfigureAwait(true);

		Assert.NotNull(response);
		Assert.Single(response);
		var data = response[0];
		Assert.Equal(-100m, data.WinLoss);
		Assert.Equal(26m, data.Settle);
	}
}
