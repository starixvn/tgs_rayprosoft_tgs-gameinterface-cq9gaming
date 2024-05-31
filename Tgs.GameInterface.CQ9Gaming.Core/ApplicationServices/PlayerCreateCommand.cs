namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class PlayerCreateCommand(
	Uri ApiUrl,
	string ApiToken,
	string PlayerCode);
