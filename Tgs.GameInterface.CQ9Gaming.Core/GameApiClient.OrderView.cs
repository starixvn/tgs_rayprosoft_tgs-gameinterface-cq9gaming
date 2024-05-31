using System.Runtime.CompilerServices;
using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;
using Tgs.GameInterface.CQ9Gaming.Core.Extensions;
using Tgs.GameInterface.CQ9Gaming.Core.Models;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async IAsyncEnumerable<OrderViewResponseItem> OrderViewAsync(
		Uri apiUrl,
		string apiToken,
		DateTimeOffset startTime,
		DateTimeOffset endTime,
		int page = 1,
		int pageSize = 500,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, "/gameboy/order/view");
		var parameters = new SortedDictionary<string, string>
		{
			["starttime"] = startTime.DatetimeConvertToString(),
			["endtime"] = endTime.DatetimeConvertToString(),
			["page"] = page.ToString(),
			["pagesize"] = pageSize.ToString(),
		};

		var jsonNode = await GetWithParameterAsync(
			apiUrl,
			apiToken,
			parameters,
			logPrefix: "order_view",
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code == "8")
			yield break;
		if (code != "0")
			throw new ErrorCodeException(code);

		var datas = jsonNode["data"]!["Data"]!.AsArray();
		if (datas is null)
			yield break;

		foreach (var data in datas)
		{
			if (data is null)
				continue;

			var freegame = 0;

			if (!data["gametype"]!.GetValue<string>().Equals("live", StringComparison.OrdinalIgnoreCase))
			{
				var details = data["detail"]?.AsArray();
				if (details is not null)
				{
					foreach (var detail in details)
					{
						if (detail is null)
						{
							continue;
						}

						if ((detail["freegame"]?.GetValue<int>() ?? 0) > 0)
						{
							freegame = detail["freegame"]!.GetValue<int>();
						}
					}
				}
			}

			yield return new OrderViewResponseItem(
				Account: data["account"]!.GetValue<string>(),
				Roundnumber: data["gametype"]!.GetValue<string>().Equals("live", StringComparison.OrdinalIgnoreCase) ? data["roundnumber"]!.GetValue<string>() : data["round"]!.GetValue<string>(),
				Round: data["round"]!.GetValue<string>(),
				Gamehall: data["gamehall"]!.GetValue<string>(),
				Gametype: data["gametype"]!.GetValue<string>(),
				Gameplat: data["gameplat"]!.GetValue<string>(),
				Gamecode: data["gamecode"]!.GetValue<string>(),
				Freegame: freegame,
				Bet: data["bet"]!.GetValue<decimal>(),
				ValidBet: data["validbet"]!.GetValue<decimal>(),
				Win: data["win"]!.GetValue<decimal>(),
				Roomfee: data["roomfee"]!.GetValue<decimal>(),
				Rake: data["rake"]!.GetValue<decimal>(),
				Jackpot: data["jackpot"]!.GetValue<decimal>(),
				Bettime: DateTimeOffset.Parse(data["bettime"]!.GetValue<string>()),
				Endroundtime: DateTimeOffset.Parse(data["endroundtime"]!.GetValue<string>()),
				AdditionalData: data.ToJsonString());
		}
	}
}
