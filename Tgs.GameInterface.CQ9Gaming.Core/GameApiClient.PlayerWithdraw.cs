using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;
using Tgs.GameInterface.CQ9Gaming.Core.Models;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask<PlayerWithdrawResponse> PlayerWithdrawAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		string mtCode,
		decimal amount,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, "/gameboy/player/withdraw");
		var parameters = new SortedDictionary<string, string>
		{
			["account"] = account,
			["mtcode"] = mtCode,
			["amount"] = amount.ToString("0.####"),
		};

		var jsonNode = await PostAsync(
			apiUrl,
			apiToken,
			parameters,
			logPrefix: nameof(PlayerWithdrawAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code != "0")
			throw new ErrorCodeException(code);

		var data = jsonNode["data"]!;
		var balance = data["balance"]!.GetValue<decimal>();
		var res = jsonNode.ToJsonString();

		return new(
			balance,
			res
		);
	}
}
