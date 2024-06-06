using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

internal sealed class FundInGetService(IGameApiClient apiClient) : IGetService<FundInQuery, FundInResult>
{
	public async ValueTask<FundInResult> GetAsync(FundInQuery query, CancellationToken cancellationToken = default)
	{
		var response = await apiClient.PlayerWithdrawAsync(
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
