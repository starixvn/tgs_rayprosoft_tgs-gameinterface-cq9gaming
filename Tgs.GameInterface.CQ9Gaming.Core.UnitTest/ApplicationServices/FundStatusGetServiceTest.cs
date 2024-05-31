using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest.ApplicationServices;

public sealed class FundStatusGetServiceTest
{
	[Fact]
	public async Task Handle_ParameterAndResultTransfer()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var orderId = Guid.NewGuid().ToString("N");
		var client = Substitute.For<IGameApiClient>();
		_ = client.TransactionRecordAsync(
			apiUrl: Arg.Any<Uri>(),
			apiToken: Arg.Any<string>(),
			mtCode: Arg.Any<string>())
			.Returns(ValueTask.FromResult(true));
		var service = new FundStatusGetService(client);
		var query = new FundStatusQuery(
			apiUrl,
			apiToken,
			orderId);

		var result = await service.GetAsync(query, default).ConfigureAwait(true);

		Assert.True(result);
		_ = client.Received().TransactionRecordAsync(
			apiUrl,
			apiToken,
			orderId);
	}
}
