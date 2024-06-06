using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

internal sealed class PlayerCreateCommandService(IGameApiClient apiClient) : ICommandService<PlayerCreateCommand>
{
	public ValueTask ExecuteAsync(PlayerCreateCommand command, CancellationToken cancellationToken = default)
		=> apiClient.PlayerCreatedAsync(
			command.ApiUrl,
			command.ApiToken,
			command.PlayerCode,
			cancellationToken);
}
