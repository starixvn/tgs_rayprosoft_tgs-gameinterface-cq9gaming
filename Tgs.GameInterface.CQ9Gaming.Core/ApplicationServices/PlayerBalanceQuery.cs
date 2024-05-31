namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class PlayerBalanceQuery(
	Uri ApiUrl,
	string ApiToken,
	string PlayerCode);
