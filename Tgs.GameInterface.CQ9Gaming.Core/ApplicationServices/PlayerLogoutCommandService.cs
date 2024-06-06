using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

internal sealed class PlayerLogoutCommandService(IGameApiClient apiClient) : ICommandService<PlayerLogoutCommand>
{
	public ValueTask ExecuteAsync(PlayerLogoutCommand command, CancellationToken cancellationToken = default)
		=> apiClient.PlayerLogoutAsync(
			command.ApiUrl,
			command.ApiToken,
			command.PlayerCode,
			cancellationToken);
}
