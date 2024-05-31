using System.Diagnostics;

namespace Tgs.GameInterface.CQ9Gaming.Client;

public class DiagnosticsInterceptorOptions
{
	public ActivitySource InterceptorActivitySource { get; set; } =
		new ActivitySource($"Tgs.GameInterface.CQ9Gaming.Client.DiagnosticsInterceptor");
}
