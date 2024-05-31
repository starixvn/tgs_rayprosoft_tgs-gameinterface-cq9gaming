using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask PlayerLogoutAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, "/gameboy/player/logout");
		var parameters = new SortedDictionary<string, string>
		{
			["account"] = account,
		};

		var jsonNode = await PostAsync(
			apiUrl,
			apiToken,
			parameters,
			logPrefix: nameof(PlayerLogoutAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code != "0")
			throw new ErrorCodeException(code);
	}
}
