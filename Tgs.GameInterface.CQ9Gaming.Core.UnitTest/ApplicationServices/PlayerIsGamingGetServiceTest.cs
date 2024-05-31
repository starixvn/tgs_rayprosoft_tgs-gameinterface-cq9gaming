using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class PlayerIsGamingGetServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("login"));
		var service = new PlayerIsGamingGetService(client);
		var query = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		await service.GetAsync(query, default).ConfigureAwait(true);

		_ = client.Received().PlayerTokenAsync(
			apiUrl,
			apiToken,
			playerCode);
	}

	[Fact]
	public async Task Handle_ApiReturnLogin_ServiceReturnTrue()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("login"));
		var service = new PlayerIsGamingGetService(client);
		var query = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.True(result);
	}

	[Fact]
	public async Task Handle_ApiReturnGaming_ServiceReturnTrue()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("gaming"));
		var service = new PlayerIsGamingGetService(client);
		var query = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.True(result);
	}

	[Fact]
	public async Task Handle_ApiReturnLogout_ServiceReturnFalse()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerTokenAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>())
			.Returns(ValueTask.FromResult("logout"));
		var service = new PlayerIsGamingGetService(client);
		var query = new PlayerOnlineQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.False(result);
	}
}
