using System.Text.Json.Nodes;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.IntegrationTest;

public class GameRecordUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("https://api.cqgame.games/");
		var apiToken = "apiToken";
		var startTime = DateTimeOffset.UtcNow.AddMinutes(-10);
		var endTime = DateTimeOffset.UtcNow;
		var page = 1;
		var pageSize = 5000;

		var web = new GameInterfaceCQ9GamingApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IQueryService<GameRecordBetLogQuery, GameRecordResult>>();

			var query = new GameRecordBetLogQuery(
				ApiUrl: apiUrl,
				ApiToken: apiToken,
				StartTime: startTime,
				EndTime: endTime,
				Page: page,
				PageSize: pageSize);

			_ = fakeService.QueryAsync(
				query: Arg.Is(query),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new GameRecordResult[] {
			new(
				PlayerCode: "4ucmqplxb029",
				ExternalRoundId: "10014289",
				ExternalBetId: "GINKGO240528220000007",
				ExternalGameId: "GINKGO01",
				ExternalCreatedOn: DateTimeOffset.Parse("2024-05-28T22:48:03.802-04:00"),
				ExternalClosedOn: DateTimeOffset.Parse("2024-05-28T22:48:20.441-04:00"),
				IsFreegame: false,
				Bet: 10.0m,
				ValidBet: 10.0m,
				Settle: 20.0m,
				WinLoss: 10.0m,
				Jackpot: 0.0m,
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
				}.ToJsonString())}.ToAsyncEnumerable());

			_ = services.AddTransient(_ => fakeService);
		}));

		var sut = new Client.GameInterfaceCQ9GamingClient(new Protos.GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient(
				GrpcChannel.ForAddress(
					address: web.Server.BaseAddress,
					channelOptions: new GrpcChannelOptions { HttpHandler = web.Server.CreateHandler() })));

		var actual = await sut.GameRecordAsync(
			apiUrl: apiUrl,
			apiToken: apiToken,
			startTime: startTime,
			endTime: endTime,
			page: page,
			pageSize: pageSize)
			.ToArrayAsync();

		Assert.NotNull(actual);
		var data = actual[0];
		Assert.Equal("4ucmqplxb029", data.PlayerCode);
		Assert.Equal("10014289", data.ExternalRoundId);
		Assert.Equal("GINKGO240528220000007", data.ExternalBetId);
		Assert.Equal("GINKGO01", data.ExternalGameId);
		Assert.Equal(DateTimeOffset.Parse("2024-05-28T22:48:03.802-04:00"), data.ExternalCreatedOn.ToDateTimeOffset());
		Assert.Equal(DateTimeOffset.Parse("2024-05-28T22:48:20.441-04:00"), data.ExternalClosedOn.ToDateTimeOffset());
		Assert.False(data.IsFreegame);
		Assert.Equal(10.0m, data.Bet.ToDecimal().GetValueOrDefault(0.0m));
		Assert.Equal(10.0m, data.ValidBet.ToDecimal().GetValueOrDefault(0.0m));
		Assert.Equal(20.0m, data.Settle.ToDecimal().GetValueOrDefault(0.0m));
		Assert.Equal(10.0m, data.WinLoss.ToDecimal().GetValueOrDefault(0.0m));
		Assert.Equal(0.0m, data.Jackpot.ToDecimal().GetValueOrDefault(0.0m));
		Assert.NotEmpty(data.AdditionalData);
	}
}
