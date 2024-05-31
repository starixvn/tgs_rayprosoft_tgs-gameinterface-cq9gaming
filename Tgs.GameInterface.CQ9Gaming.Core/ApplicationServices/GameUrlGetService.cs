using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class GameUrlGetService(IGameApiClient apiClient) : IGetService<GameUrlQuery, GameUrlResult>
{
	public async ValueTask<GameUrlResult> GetAsync(GameUrlQuery query, CancellationToken cancellationToken)
	{
		var result = await apiClient.PlayerGamelinkAsync(
			query.ApiUrl,
			query.ApiToken,
			query.UserToken,
			Gamehall.CQ9,
			query.GameCode,
			query.IsMobile ? "mobile" : "web",
			query.Language,
			cancellationToken)
			.ConfigureAwait(false);
		return new(
			Url: result.Url,
			Token: result.Token
		);
	}
}
