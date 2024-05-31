namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class FundStatusQuery(
	Uri ApiUrl,
	string ApiToken,
	string OrderId);
