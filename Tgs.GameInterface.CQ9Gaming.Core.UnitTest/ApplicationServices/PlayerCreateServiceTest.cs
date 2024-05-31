using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class PlayerCreateCommandServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var client = Substitute.For<IGameApiClient>();

		var service = new PlayerCreateCommandService(client);

		var command = new PlayerCreateCommand(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		await service.ExecuteAsync(command, default).ConfigureAwait(true);

		_ = client
			.Received(1)
			.PlayerCreatedAsync(
			apiUrl: Arg.Is(apiUrl),
			apiToken: Arg.Is(apiToken),
			account: Arg.Is(playerCode));
	}
}
