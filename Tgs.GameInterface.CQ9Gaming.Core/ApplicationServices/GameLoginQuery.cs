namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class GameLoginQuery(
	Uri ApiUrl,
	string ApiToken,
	string PlayerCode);
