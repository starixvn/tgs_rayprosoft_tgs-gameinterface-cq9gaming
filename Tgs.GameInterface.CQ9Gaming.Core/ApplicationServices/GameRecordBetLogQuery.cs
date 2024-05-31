namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed record class GameRecordBetLogQuery(
	Uri ApiUrl,
	string ApiToken,
	DateTimeOffset StartTime,
	DateTimeOffset EndTime,
	int Page = 1,
	int PageSize = 500);
