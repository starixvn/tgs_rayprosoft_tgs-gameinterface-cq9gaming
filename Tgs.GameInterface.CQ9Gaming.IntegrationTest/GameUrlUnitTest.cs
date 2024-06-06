using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.IntegrationTest;

public class GameUrlUnitTest
{
	[Fact]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("https://api.cqgame.games/");
		var apiToken = "apiToken";
		var userToken = "UserToken";
		var gameCode = "1";
		var isMobile = true;
		var language = "EN";

		var web = new GameInterfaceCQ9GamingApplication(builder => _ = builder.ConfigureServices(services =>
		{
			var fakeService = Substitute.For<IGetService<GameUrlQuery, GameUrlResult>>();

			var query = new GameUrlQuery(
				ApiUrl: apiUrl,
				ApiToken: apiToken,
				UserToken: userToken,
				GameCode: gameCode,
				IsMobile: isMobile,
				Language: language);

			_ = fakeService.GetAsync(
				query: Arg.Is(query),
				cancellationToken: Arg.Any<CancellationToken>())
			.Returns(new GameUrlResult(
				Url: "https://api.cqgame.games/",
				Token: "token"));

			_ = services.AddTransient(_ => fakeService);
		}));

		var sut = new Client.GameInterfaceCQ9GamingClient(new Protos.GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient(
				GrpcChannel.ForAddress(
					address: web.Server.BaseAddress,
					channelOptions: new GrpcChannelOptions { HttpHandler = web.Server.CreateHandler() })));

		var actual = await sut.GameUrlAsync(
			apiUrl: apiUrl,
			apiToken: apiToken,
			userToken: userToken,
			gameCode: gameCode,
			isMobile: isMobile,
			language: language);

		Assert.NotNull(actual);
		Assert.Equal("https://api.cqgame.games/", actual.GameUrl);
	}
}
