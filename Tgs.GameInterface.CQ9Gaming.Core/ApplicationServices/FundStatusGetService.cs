using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

internal sealed class FundStatusGetService(IGameApiClient apiClient) : IGetService<FundStatusQuery, bool>
{
	public ValueTask<bool> GetAsync(FundStatusQuery query, CancellationToken cancellationToken = default)
		=> apiClient.TransactionRecordAsync(
			apiUrl: query.ApiUrl,
			apiToken: query.ApiToken,
			mtCode: query.OrderId,
			cancellationToken: cancellationToken);
}
