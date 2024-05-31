using System.Globalization;
using System.Text.RegularExpressions;

namespace Tgs.GameInterface.CQ9Gaming.Core.Extensions;

public static class DateTimeExtensions
{
	const string Rfc3339DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.fffK";

	public static string DatetimeConvertToString(this DateTimeOffset time)
	{
		var zone = TimeZoneInfo.CreateCustomTimeZone(id: "eastern time", baseUtcOffset: TimeSpan.FromHours(-4), "eastern time", "eastern time");
		return TimeZoneInfo.ConvertTime(time.ToUniversalTime(), zone).ToString(Rfc3339DateTimeFormat);
	}
}
