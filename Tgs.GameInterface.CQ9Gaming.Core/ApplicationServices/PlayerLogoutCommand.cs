namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class PlayerLogoutCommand(
	Uri ApiUrl,
	string ApiToken,
	string PlayerCode);
