using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;
using Tgs.GameInterface.CQ9Gaming.Core.Models;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask<PlayerBalanceResponse> PlayerBalanceAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, $"/gameboy/player/balance/{account}");

		var jsonNode = await GetAsync(
			apiUrl,
			apiToken,
			logPrefix: nameof(PlayerBalanceAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code == "2")
			return new(IsPlayerExists: false, Balance: default);
		if (code != "0")
			throw new ErrorCodeException(code);

		var data = jsonNode["data"]!;
		var balance = data["balance"]!.GetValue<decimal>();

		return new(
			IsPlayerExists: true,
			Balance: balance
		);
	}
}
