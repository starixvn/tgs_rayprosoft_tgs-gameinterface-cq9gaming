namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class PlayerOnlineQuery(
	Uri ApiUrl,
	string ApiToken,
	string PlayerCode);
