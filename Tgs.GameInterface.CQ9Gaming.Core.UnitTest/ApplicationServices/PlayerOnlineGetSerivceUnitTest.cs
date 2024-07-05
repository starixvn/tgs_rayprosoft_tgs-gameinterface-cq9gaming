using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;
public class PlayerOnlineGetSerivceUnitTest
{
	[Fact]
	public async Task Status_Is_gaming_PlayerOnline_Is_True()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var userToken = Guid.NewGuid().ToString("N");
		var client = Substitute.For<IGameApiClient>();

		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("gaming"));

		var service = new PlayerOnlineGetSerivce(client);
		var command = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var actual = await service.GetAsync(command, default).ConfigureAwait(true);

		Assert.True(actual);
	}

	[Fact]
	public async Task Status_Is_login_PlayerOnline_Is_False()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var userToken = Guid.NewGuid().ToString("N");
		var client = Substitute.For<IGameApiClient>();

		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("login"));

		var service = new PlayerOnlineGetSerivce(client);
		var command = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var actual = await service.GetAsync(command, default).ConfigureAwait(true);

		Assert.False(actual);
	}

	[Fact]
	public async Task Status_Is_logout_PlayerOnline_Is_False()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var userToken = Guid.NewGuid().ToString("N");
		var client = Substitute.For<IGameApiClient>();

		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("logout"));

		var service = new PlayerOnlineGetSerivce(client);
		var command = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var actual = await service.GetAsync(command, default).ConfigureAwait(true);

		Assert.False(actual);
	}
}
