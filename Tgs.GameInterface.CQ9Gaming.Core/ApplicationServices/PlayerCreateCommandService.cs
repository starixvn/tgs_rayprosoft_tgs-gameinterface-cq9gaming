using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

public sealed class PlayerCreateCommandService(IGameApiClient apiClient) : ICommandService<PlayerCreateCommand>
{
	public ValueTask ExecuteAsync(PlayerCreateCommand command, CancellationToken cancellationToken = default)
		=> apiClient.PlayerCreatedAsync(
			command.ApiUrl,
			command.ApiToken,
			command.PlayerCode,
			cancellationToken);
}
