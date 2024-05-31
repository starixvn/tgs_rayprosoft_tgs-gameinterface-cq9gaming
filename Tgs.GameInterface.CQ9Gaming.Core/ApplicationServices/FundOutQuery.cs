namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class FundOutQuery(
	Uri ApiUrl,
	string ApiToken,
	string PlayerCode,
	string OrderId,
	decimal Amount);
