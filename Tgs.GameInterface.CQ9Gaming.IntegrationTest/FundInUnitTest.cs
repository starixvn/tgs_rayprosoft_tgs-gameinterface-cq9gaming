using System.Text.Json;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.IntegrationTest;

public class FundInUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("https://api.cqgame.games/");
		var apiToken = "apiToken";
		var playerCode = "4ucmqplxb029";
		var orderId = Guid.NewGuid().ToString("N");
		var amount = 1.0m;

		var web = new GameInterfaceCQ9GamingApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<FundInQuery, FundInResult>>();

			var query = new FundInQuery(
				ApiUrl: apiUrl,
				ApiToken: apiToken,
				PlayerCode: playerCode,
				OrderId: orderId,
				Amount: amount);

			_ = fakeService.GetAsync(
				query: Arg.Is(query),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new FundInResult(
				Balance: 0.0m,
				Resp: JsonSerializer.Serialize(new
				{
					Balance = 0.0m
				})));

			_ = services.AddTransient(_ => fakeService);
		}));

		var sut = new Client.GameInterfaceCQ9GamingClient(new Protos.GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient(
				GrpcChannel.ForAddress(
					address: web.Server.BaseAddress,
					channelOptions: new GrpcChannelOptions { HttpHandler = web.Server.CreateHandler() })));

		var actual = await sut.FundInAsync(
			apiUrl: apiUrl,
			apiToken: apiToken,
			playerCode: playerCode,
			orderId: orderId,
			amount: amount);

		Assert.NotNull(actual);
		Assert.Equal(0.0m, actual.Balance.ToDecimal().GetValueOrDefault(0.0m));
	}
}
