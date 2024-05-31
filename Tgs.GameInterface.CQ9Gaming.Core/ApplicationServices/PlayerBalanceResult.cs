namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class PlayerBalanceResult(
	bool IsPlayerExists,
	decimal Balance);
