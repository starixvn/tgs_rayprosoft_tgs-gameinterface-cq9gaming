using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class PlayerLoginGetServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var userToken = Guid.NewGuid().ToString("N");
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerLoginAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult(userToken));
		var service = new GameLoginGetService(client);
		var query = new GameLoginQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.Equal(userToken, result);
		_ = client.Received().PlayerLoginAsync(
			apiUrl,
			apiToken,
			playerCode);
	}
}
