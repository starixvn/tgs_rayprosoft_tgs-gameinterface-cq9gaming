using System.Runtime.CompilerServices;
using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class GameRecordBetLogQueryService(IGameApiClient apiClient) : IQueryService<GameRecordBetLogQuery, GameRecordResult>
{
	public async IAsyncEnumerable<GameRecordResult> QueryAsync(
		GameRecordBetLogQuery query,
		[EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var responseItems = await apiClient
			.OrderViewAsync(
				apiUrl: query.ApiUrl,
				apiToken: query.ApiToken,
				startTime: query.StartTime,
				endTime: query.EndTime,
				page: query.Page,
				pageSize: query.PageSize,
				cancellationToken: cancellationToken)
			.ToArrayAsync(
				cancellationToken: cancellationToken)
			.ConfigureAwait(false)
			?? [];

		if (responseItems.Length == 0)
			yield break;

		foreach (var responseItem in responseItems)
		{
			var winLoss = responseItem.Gametype.ToLower() switch
			{
				"table" => responseItem.Win - responseItem.Rake - responseItem.Roomfee,
				"live" => responseItem.Win - responseItem.Rake - responseItem.Roomfee,
				_ => responseItem.Win - responseItem.Bet
			};

			var validBet = responseItem.Gametype.ToLower() switch
			{
				"table" => responseItem.ValidBet,
				"live" => responseItem.ValidBet,
				_ => responseItem.Bet
			};

			var settle = validBet + winLoss;

			yield return new GameRecordResult(
				PlayerCode: responseItem.Account,
				ExternalRoundId: responseItem.Roundnumber,
				ExternalBetId: responseItem.Round,
				ExternalGameId: responseItem.Gamecode,
				ExternalCreatedOn: responseItem.Bettime,
				ExternalClosedOn: responseItem.Endroundtime,
				IsFreegame: responseItem.Freegame > 0,
				Bet: responseItem.Bet,
				ValidBet: validBet,
				Settle: settle,
				WinLoss: winLoss,
				Jackpot: responseItem.Jackpot,
				AdditionalData: responseItem.AdditionalData);
		}
	}
}
