using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask PlayerCreatedAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, "/gameboy/player");
		var parameters = new SortedDictionary<string, string>
		{
			["account"] = account,
			["password"] = DefaultPassword,
			["nickname"] = account,
		};

		var jsonNode = await PostAsync(
			apiUrl,
			apiToken,
			parameters,
			logPrefix: nameof(PlayerCreatedAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code is not ("0" or "6"))
			throw new ErrorCodeException(code);
	}
}
