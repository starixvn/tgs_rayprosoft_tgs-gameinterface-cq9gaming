using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.Models;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class GameUrlGetServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var userToken = Guid.NewGuid().ToString("N");
		var gameCode = Guid.NewGuid().ToString("N");
		var gamePlat = "mobile";
		var lang = "zh-cn";
		var token = Guid.NewGuid().ToString("N");
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerGamelinkAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			userToken: Arg.Any<string>(),
			gameHall: Arg.Any<string>(),
			gameCode: Arg.Any<string>(),
			gamePlat: Arg.Any<string>(),
			lang: Arg.Any<string>())
			.Returns(ValueTask.FromResult(new PlayerGamelinkResponse(
				Url: "https://xxx.xxx.xxx/xxx",
				Token: token)));
		var service = new GameUrlGetService(client);
		var query = new GameUrlQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			UserToken: userToken,
			GameCode: gameCode,
			IsMobile: gamePlat == "mobile",
			Language: lang);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.Equal(token, result.Token);
		Assert.Equal("https://xxx.xxx.xxx/xxx", result.Url);
		_ = client.Received().PlayerGamelinkAsync(
			apiUrl,
			apiToken,
			userToken,
			Gamehall.CQ9,
			gameCode,
			gamePlat,
			lang);
	}
}
