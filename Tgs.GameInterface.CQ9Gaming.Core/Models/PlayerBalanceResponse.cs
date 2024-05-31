namespace Tgs.GameInterface.CQ9Gaming.Core.Models;

public sealed record class PlayerBalanceResponse(
	bool IsPlayerExists,
	decimal Balance);
