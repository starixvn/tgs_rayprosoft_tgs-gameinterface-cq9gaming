using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class PlayerLogoutCommandService(IGameApiClient apiClient) : ICommandService<PlayerLogoutCommand>
{
	public ValueTask ExecuteAsync(PlayerLogoutCommand command, CancellationToken cancellationToken = default)
		=> apiClient.PlayerLogoutAsync(
			command.ApiUrl,
			command.ApiToken,
			command.PlayerCode,
			cancellationToken);
}
