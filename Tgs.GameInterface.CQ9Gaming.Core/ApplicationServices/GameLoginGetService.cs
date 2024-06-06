using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

internal sealed class GameLoginGetService(IGameApiClient apiClient) : IGetService<GameLoginQuery, string>
{
	public ValueTask<string> GetAsync(GameLoginQuery query, CancellationToken cancellationToken)
		=> apiClient.PlayerLoginAsync(
			query.ApiUrl,
			query.ApiToken,
			query.PlayerCode,
			cancellationToken);
}
