using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

using Tgs.GameInterface.CQ9Gaming.Core.Models;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class PlayerBalanceGetServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var balance = Random.Shared.Next();
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerBalanceAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult(new PlayerBalanceResponse(
				IsPlayerExists: true,
				Balance: balance
			)));
		var service = new PlayerBalanceGetService(client);
		var query = new PlayerBalanceQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var response = await service.GetAsync(query, default).ConfigureAwait(true);

		_ = client.Received().PlayerBalanceAsync(
			apiUrl,
			apiToken,
			playerCode);
		Assert.NotNull(response);
		Assert.True(response.IsPlayerExists);
		Assert.Equal(balance, response.Balance);
	}
}
