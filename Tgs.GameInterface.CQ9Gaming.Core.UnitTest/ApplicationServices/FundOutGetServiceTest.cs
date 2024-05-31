using System.Text.Json.Nodes;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.Models;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class FundOutGetServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var playerCode = "4ucmqplxb029";
		var orderId = Guid.NewGuid().ToString("N");
		var amount = 10m;
		var res = new JsonObject
		{
			["balance"] = 10m,
			["currency"] = "CNY"
		}.ToJsonString();
		var client = Substitute.For<IGameApiClient>();
		_ = client.PlayerDepositAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			account: Arg.Any<string>(),
			mtCode: Arg.Any<string>(),
			amount: Arg.Any<decimal>())
			.Returns(ValueTask.FromResult(new PlayerDepositResponse(
				Balance: 10m,
				Resp: res)));
		var service = new FundOutGetService(client);
		var query = new FundOutQuery(
			ApiUrl: apiUrl,
			ApiToken: apiToken,
			PlayerCode: playerCode,
			OrderId: orderId,
			Amount: amount);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.Equal(10m, result.Balance);
		Assert.Equal(res, result.Resp);
		_ = client.Received().PlayerDepositAsync(
			apiUrl,
			apiToken,
			playerCode,
			orderId,
			amount);
	}
}
