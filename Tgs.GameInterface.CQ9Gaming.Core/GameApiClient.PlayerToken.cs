using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask<string> PlayerTokenAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, $"/gameboy/player/token/{account}");

		var jsonNode = await GetAsync(
			apiUrl,
			apiToken,
			logPrefix: nameof(PlayerTokenAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code != "0")
			throw new ErrorCodeException(code);

		var data = jsonNode["data"]!;
		var status = data["status"]!.GetValue<string>();

		return status;
	}
}
