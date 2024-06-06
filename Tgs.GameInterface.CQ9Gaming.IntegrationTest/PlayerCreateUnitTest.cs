using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.IntegrationTest;

public class PlayerCreateUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("https://api.cqgame.games/");
		var apiToken = "apiToken";
		var playerCode = "4ucmqplxb029";
		var fakeService = Substitute.For<ICommandService<PlayerCreateCommand>>();
		var web = new GameInterfaceCQ9GamingApplication(builder => _ = builder.ConfigureServices(services => _ = services.AddTransient(_ => fakeService)));

		var sut = new Client.GameInterfaceCQ9GamingClient(new Protos.GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient(
				GrpcChannel.ForAddress(
					address: web.Server.BaseAddress,
					channelOptions: new GrpcChannelOptions { HttpHandler = web.Server.CreateHandler() })));

		await sut.PlayerCreateAsync(
			apiUrl: apiUrl,
			apiToken: apiToken,
			playerCode: playerCode);

		_ = fakeService
			.Received(1)
			.ExecuteAsync(
			command: Arg.Is(new PlayerCreateCommand(
				ApiUrl: apiUrl,
				ApiToken: apiToken,
				PlayerCode: playerCode)),
			cancellationToken: Arg.Any<CancellationToken>());
	}
}
