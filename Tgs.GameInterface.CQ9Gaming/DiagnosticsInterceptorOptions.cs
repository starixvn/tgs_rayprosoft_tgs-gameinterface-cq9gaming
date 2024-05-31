using System.Diagnostics;

namespace Tgs.GameInterface.CQ9Gaming;

public class DiagnosticsInterceptorOptions
{
	public ActivitySource InterceptorActivitySource { get; set; } =
		new ActivitySource($"Tgs.GameInterface.CQ9Gaming.DiagnosticsInterceptor");
}
