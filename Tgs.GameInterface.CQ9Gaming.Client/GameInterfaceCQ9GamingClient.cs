using System.Runtime.CompilerServices;
using Google.Protobuf.WellKnownTypes;
using Tgs.GameInterface.CQ9Gaming.Protos;

namespace Tgs.GameInterface.CQ9Gaming.Client;

internal class GameInterfaceCQ9GamingClient(
	GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingClient client) : IGameInterfaceCQ9GamingClient
{
	public async ValueTask<string> CheckingServiceAsync(string machineName, CancellationToken cancellationToken = default)
	{
		var request = new CheckingServiceRequest
		{
			MachineName = machineName,
		};

		var reply = await client.CheckingServiceAsync(
				request: request,
				cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return reply.MachineName;
	}

	public async ValueTask<FundReply> FundInAsync(Uri apiUrl, string apiToken, string playerCode, string orderId, decimal amount, CancellationToken cancellationToken = default)
	{
		var request = new FundRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode,
			OrderId = orderId,
			Amount = amount
		};
		return await client.FundInAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask<FundReply> FundOutAsync(Uri apiUrl, string apiToken, string playerCode, string orderId, decimal amount, CancellationToken cancellationToken = default)
	{
		var request = new FundRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode,
			OrderId = orderId,
			Amount = amount
		};
		return await client.FundOutAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask<bool> FundStatusAsync(Uri apiUrl, string apiToken, string orderId, CancellationToken cancellationToken = default)
	{
		var request = new FundStatusRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			OrderId = orderId
		};
		var reply = await client.FundStatusAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return reply.Status;
	}

	public async ValueTask<GameLoginReply> GameLoginAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default)
	{
		var request = new GameLoginRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode
		};
		return await client.GameLoginAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async IAsyncEnumerable<GameRecordInfo> GameRecordAsync(Uri apiUrl, string apiToken, DateTimeOffset startTime, DateTimeOffset endTime, int page = 1, int pageSize = 500, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var request = new GameRecordRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			StartTime = startTime.ToTimestamp(),
			EndTime = endTime.ToTimestamp(),
			Page = page,
			PageSize = pageSize,
		};

		var reply = await client.GameRecordAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		foreach (var info in reply.Items)
		{
			yield return info;
		}
	}

	public async ValueTask<GameUrlReply> GameUrlAsync(Uri apiUrl, string apiToken, string userToken, string gameCode, bool isMobile, string language, CancellationToken cancellationToken = default)
	{
		var request = new GameUrlRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			UserToken = userToken,
			GameCode = gameCode,
			IsMobile = isMobile,
			Language = language,
		};

		return await client.GameUrlAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask<PlayerBalanceReply> PlayerBalanceAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default)
	{
		var request = new PlayerBalanceRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode
		};

		return await client.PlayerBalanceAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask PlayerCreateAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default)
	{
		var request = new PlayerCreateRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode
		};

		_ = await client.PlayerCreateAsync(
		   request: request,
		   cancellationToken: cancellationToken)
		   .ConfigureAwait(false);
	}

	public async ValueTask PlayerLogoutAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default)
	{
		var request = new PlayerLogoutRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode
		};

		_ = await client.PlayerLogoutAsync(
		   request: request,
		   cancellationToken: cancellationToken)
		   .ConfigureAwait(false);
	}

	public async ValueTask<bool> PlayerOnlineAsync(Uri apiUrl, string apiToken, string playerCode, CancellationToken cancellationToken = default)
	{
		var request = new PlayerOnlineRequest()
		{
			ApiUrl = apiUrl.ToString(),
			ApiToken = apiToken,
			PlayerCode = playerCode
		};

		var reply = await client.PlayerOnlineAsync(
			request: request,
			cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return reply.Online;
	}
}
