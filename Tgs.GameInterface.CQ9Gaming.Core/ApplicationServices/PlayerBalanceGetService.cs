using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class PlayerBalanceGetService(IGameApiClient apiClient) : IGetService<PlayerBalanceQuery, PlayerBalanceResult>
{
	public async ValueTask<PlayerBalanceResult> GetAsync(PlayerBalanceQuery query, CancellationToken cancellationToken = default)
	{
		var result = await apiClient.PlayerBalanceAsync(
			query.ApiUrl,
			query.ApiToken,
			query.PlayerCode,
			cancellationToken)
			.ConfigureAwait(false);
		return new(
			result.IsPlayerExists,
			result.Balance);
	}
}
