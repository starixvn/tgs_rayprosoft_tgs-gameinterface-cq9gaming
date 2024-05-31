namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class GameRecordResult(
	string PlayerCode,
	string ExternalRoundId,
	string ExternalBetId,
	string ExternalGameId,
	DateTimeOffset ExternalCreatedOn,
	DateTimeOffset ExternalClosedOn,
	bool IsFreegame,
	decimal Bet,
	decimal ValidBet,
	decimal Settle,
	decimal WinLoss,
	decimal Jackpot,
	string AdditionalData);
