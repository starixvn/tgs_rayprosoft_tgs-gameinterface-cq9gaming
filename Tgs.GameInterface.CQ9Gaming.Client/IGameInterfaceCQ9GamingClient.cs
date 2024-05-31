using Tgs.GameInterface.CQ9Gaming.Protos;

namespace Tgs.GameInterface.CQ9Gaming.Client;

public interface IGameInterfaceCQ9GamingClient
{
	ValueTask<string> CheckingServiceAsync(string machineName, CancellationToken cancellationToken = default);

	ValueTask<FundReply> FundInAsync(Uri apiUrl, string apiToken, string playerCode, string orderId, decimal amount, CancellationToken cancellationToken = default);

	ValueTask<FundReply> FundOutAsync(Uri apiUrl, string apiToken, string playerCode, string orderId, decimal amount, CancellationToken cancellationToken = default);

	ValueTask<bool> FundStatusAsync(Uri apiUrl, string apiToken, string orderId, CancellationToken cancellationToken = default);

	ValueTask<GameLoginReply> GameLoginAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default);

	ValueTask<GameUrlReply> GameUrlAsync(Uri apiUrl, string apiToken, string userToken, string gameCode, bool isMobile, string language, CancellationToken cancellationToken = default);

	IAsyncEnumerable<GameRecordInfo> GameRecordAsync(Uri apiUrl, string apiToken, DateTimeOffset startTime, DateTimeOffset endTime, int page = 1, int pageSize = 500, CancellationToken cancellationToken = default);

	ValueTask<PlayerBalanceReply> PlayerBalanceAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default);

	ValueTask PlayerCreateAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default);

	ValueTask PlayerLogoutAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default);

	ValueTask<bool> PlayerOnlineAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default);
}
