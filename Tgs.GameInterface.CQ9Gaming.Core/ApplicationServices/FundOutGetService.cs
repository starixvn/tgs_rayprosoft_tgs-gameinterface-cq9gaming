using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class FundOutGetService(IGameApiClient apiClient) : IGetService<FundOutQuery, FundOutResult>
{
	public async ValueTask<FundOutResult> GetAsync(FundOutQuery query, CancellationToken cancellationToken = default)
	{
		var response = await apiClient.PlayerDepositAsync(
			query.ApiUrl,
			query.ApiToken,
			query.PlayerCode,
			query.OrderId,
			query.Amount,
			cancellationToken)
			.ConfigureAwait(false);

		return new(
			response.Balance,
			response.Resp
		);
	}
}
