namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class GameUrlQuery(
	Uri ApiUrl,
	string ApiToken,
	string UserToken,
	string GameCode,
	bool IsMobile,
	string Language);
