using System.Text.Json;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.IntegrationTest;

public class FundStatusUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("https://api.cqgame.games/");
		var apiToken = "apiToken";
		var orderId = Guid.NewGuid().ToString("N");

		var web = new GameInterfaceCQ9GamingApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<FundStatusQuery, bool>>();

			var query = new FundStatusQuery(
				ApiUrl: apiUrl,
				ApiToken: apiToken,
				OrderId: orderId);

			_ = fakeService.GetAsync(
				query: Arg.Is(query),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(true);

			_ = services.AddTransient(_ => fakeService);
		}));

		var sut = new Client.GameInterfaceCQ9GamingClient(new Protos.GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient(
				GrpcChannel.ForAddress(
					address: web.Server.BaseAddress,
					channelOptions: new GrpcChannelOptions { HttpHandler = web.Server.CreateHandler() })));

		var actual = await sut.FundStatusAsync(
			apiUrl: apiUrl,
			apiToken: apiToken,
			orderId: orderId);

		Assert.True(actual);
	}
}
