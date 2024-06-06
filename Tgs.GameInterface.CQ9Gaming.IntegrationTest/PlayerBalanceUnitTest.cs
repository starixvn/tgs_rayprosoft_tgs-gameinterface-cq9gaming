using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.IntegrationTest;

public class PlayerBalanceUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("https://api.cqgame.games/");
		var apiToken = "apiToken";
		var playerCode = "4ucmqplxb029";

		var web = new GameInterfaceCQ9GamingApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<PlayerBalanceQuery, PlayerBalanceResult>>();

			var query = new PlayerBalanceQuery(
				ApiUrl: apiUrl,
				ApiToken: apiToken,
				PlayerCode: playerCode);

			_ = fakeService.GetAsync(
				query: Arg.Is(query),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new PlayerBalanceResult(
				IsPlayerExists: true,
				Balance: 100m));

			_ = services.AddTransient(_ => fakeService);
		}));

		var sut = new Client.GameInterfaceCQ9GamingClient(new Protos.GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient(
				GrpcChannel.ForAddress(
					address: web.Server.BaseAddress,
					channelOptions: new GrpcChannelOptions { HttpHandler = web.Server.CreateHandler() })));

		var actual = await sut.PlayerBalanceAsync(
			apiUrl: apiUrl,
			apiToken: apiToken,
			playerCode: playerCode);

		Assert.NotNull(actual);
		Assert.True(actual.IsPlayerExist);
		Assert.Equal(100m, actual.Balance.ToDecimal().GetValueOrDefault(0.0m));
	}
}
