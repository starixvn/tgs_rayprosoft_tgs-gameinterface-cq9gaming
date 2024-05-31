using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core;

internal sealed partial class GameApiClient
{
	public async ValueTask<bool> TransactionRecordAsync(
		Uri apiUrl,
		string apiToken,
		string mtCode,
		CancellationToken cancellationToken = default)
	{
		apiUrl = new(apiUrl, $"/gameboy/transaction/record/{mtCode}");

		var jsonNode = await GetAsync(
			apiUrl,
			apiToken,
			logPrefix: nameof(TransactionRecordAsync),
			cancellationToken)
			.ConfigureAwait(false);

		var code = jsonNode["status"]!["code"]!.GetValue<string>();
		if (code == "8")
			return false;
		if (code != "0")
			throw new ErrorCodeException(code);

		var data = jsonNode["data"]!;
		var dataStatus = data["status"]!.GetValue<string>();
		var isProcessing = dataStatus.Equals("processing", StringComparison.OrdinalIgnoreCase);
		var succeed = isProcessing
			? throw new Exception($"The status of this transaction number is processing.({mtCode})")
			: dataStatus.Equals("success", StringComparison.OrdinalIgnoreCase);

		return succeed;
	}
}
