using Tgs.GameInterface.CQ9Gaming.Core.Models;

namespace Tgs.GameInterface.CQ9Gaming.Core;

public interface IGameApiClient
{
	ValueTask PlayerCreatedAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default);

	ValueTask<string> PlayerLoginAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default);

	ValueTask PlayerLogoutAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default);

	ValueTask<string> PlayerTokenAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default);

	ValueTask<PlayerGamelinkResponse> PlayerGamelinkAsync(
		Uri apiUrl,
		string apiToken,
		string userToken,
		string gameHall,
		string gameCode,
		string gamePlat,
		string lang,
		CancellationToken cancellationToken = default);

	ValueTask<PlayerWithdrawResponse> PlayerWithdrawAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		string mtCode,
		decimal amount,
		CancellationToken cancellationToken = default);

	ValueTask<PlayerDepositResponse> PlayerDepositAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		string mtCode,
		decimal amount,
		CancellationToken cancellationToken = default);

	ValueTask<PlayerBalanceResponse> PlayerBalanceAsync(
		Uri apiUrl,
		string apiToken,
		string account,
		CancellationToken cancellationToken = default);

	IAsyncEnumerable<OrderViewResponseItem> OrderViewAsync(
		Uri apiUrl,
		string apiToken,
		DateTimeOffset startTime,
		DateTimeOffset endTime,
		int page = 1,
		int pageSize = 500,
		CancellationToken cancellationToken = default);

	ValueTask<bool> TransactionRecordAsync(
		Uri apiUrl,
		string apiToken,
		string mtCode,
		CancellationToken cancellationToken = default);
}
