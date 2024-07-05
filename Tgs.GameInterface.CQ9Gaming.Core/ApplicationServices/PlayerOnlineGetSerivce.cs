using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

internal sealed class PlayerOnlineGetSerivce(IGameApiClient apiClient) : IGetService<PlayerOnlineQuery, bool>
{
	public async ValueTask<bool> GetAsync(PlayerOnlineQuery query, CancellationToken cancellationToken)
	{
		var status = await apiClient.PlayerTokenAsync(
			query.ApiUrl,
			query.ApiToken,
			query.PlayerCode,
			cancellationToken)
			.ConfigureAwait(false);

		/*
		 * usertoken只要沒有進行維護或是登出行為將會永久性有效
		 * 因為玩家沒有進行登出，所以還是會在登入狀態
		 */
		return status.Equals("gaming", StringComparison.InvariantCultureIgnoreCase);
	}
}
