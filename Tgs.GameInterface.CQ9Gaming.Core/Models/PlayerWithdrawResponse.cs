namespace Tgs.GameInterface.CQ9Gaming.Core.Models;

public sealed record class PlayerWithdrawResponse(
	decimal Balance,
	string Resp);
