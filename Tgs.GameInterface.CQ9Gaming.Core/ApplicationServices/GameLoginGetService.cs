using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class GameLoginGetService(IGameApiClient apiClient) : IGetService<GameLoginQuery, string>
{
	public ValueTask<string> GetAsync(GameLoginQuery query, CancellationToken cancellationToken)
		=> apiClient.PlayerLoginAsync(
			query.ApiUrl,
			query.ApiToken,
			query.PlayerCode,
			cancellationToken);
}
