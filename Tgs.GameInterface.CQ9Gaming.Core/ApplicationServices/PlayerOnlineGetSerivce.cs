using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class PlayerIsGamingGetService(IGameApiClient apiClient) : IGetService<PlayerOnlineQuery, bool>
{
	public async ValueTask<bool> GetAsync(PlayerOnlineQuery query, CancellationToken cancellationToken)
	{
		var status = await apiClient.PlayerTokenAsync(
			query.ApiUrl,
			query.ApiToken,
			query.PlayerCode,
			cancellationToken)
			.ConfigureAwait(false);
		return status.Equals("login", StringComparison.InvariantCultureIgnoreCase) ||
			   status.Equals("gaming", StringComparison.InvariantCultureIgnoreCase);
	}
}
