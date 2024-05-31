using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask<string> PlayerLoginAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, "/gameboy/player/login");
		var parameters = new SortedDictionary<string, string>
		{
			["account"] = account,
			["password"] = DefaultPassword,
		};

		var jsonNode = await PostAsync(
			apiUrl,
			apiToken,
			parameters,
			logPrefix: nameof(PlayerLoginAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code != "0")
			throw new ErrorCodeException(code);

		var data = jsonNode["data"]!;
		var userToken = data["usertoken"]!.GetValue<string>();

		return userToken;
	}
}
