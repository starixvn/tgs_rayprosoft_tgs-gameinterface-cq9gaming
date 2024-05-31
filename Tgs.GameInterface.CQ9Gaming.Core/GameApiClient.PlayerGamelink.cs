using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;
using Tgs.GameInterface.CQ9Gaming.Core.Models;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask<PlayerGamelinkResponse> PlayerGamelinkAsync(
		Uri apiUrl,
		string apiToken,
		string userToken,
		string gameHall,
		string gameCode,
		string gamePlat,
		string lang,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, "/gameboy/player/gamelink");
		var parameters = new SortedDictionary<string, string>
		{
			["usertoken"] = userToken,
			["gamehall"] = gameHall,
			["gamecode"] = gameCode,
			["gameplat"] = gamePlat,
			["lang"] = lang,
		};

		var jsonNode = await PostAsync(
			apiUrl,
			apiToken,
			parameters,
			logPrefix: nameof(PlayerGamelinkAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code != "0")
			throw new ErrorCodeException(code);

		var data = jsonNode["data"]!;
		var url = data["url"]!.GetValue<string>();
		var token = data["token"]!.GetValue<string>();

		return new(
			Url: url,
			Token: token
		);
	}
}
