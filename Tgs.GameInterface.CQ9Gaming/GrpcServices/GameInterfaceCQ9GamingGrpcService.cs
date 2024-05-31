using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tgs.GameInterface.CQ9Gaming.Protos;
using Tgs.ServiceUtility;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

namespace Tgs.GameInterface.CQ9Gaming.GrpcServices;

public class GameInterfaceCQ9GamingGrpcService(
	ILogger<GameInterfaceCQ9GamingGrpcService> logger,
	IGetService<PlayerBalanceQuery, PlayerBalanceResult> playerBalanceService,
	ICommandService<PlayerCreateCommand> playerCreateService,
	ICommandService<PlayerLogoutCommand> playerLogoutService,
	IGetService<PlayerOnlineQuery, bool> playerOnlineService,
	IGetService<FundInQuery, FundInResult> fundInService,
	IGetService<FundOutQuery, FundOutResult> fundOutService,
	IGetService<FundStatusQuery, bool> fundStatusService,
	IGetService<GameLoginQuery, string> gameLoginGetService,
	IGetService<GameUrlQuery, GameUrlResult> gameUrlGetService,
	IQueryService<GameRecordBetLogQuery, GameRecordResult> gameRecordBetLogService) : GameInterfaceCQ9Gaming.GameInterfaceCQ9GamingBase
{
	public override Task<CheckingServiceReply> CheckingService(CheckingServiceRequest request, ServerCallContext context)
	{
		logger.LogWarning("LogOn:{logOn} Message: Create a check connection.({machineName})", DateTime.Now.ToString("o"), request.MachineName);

		return Task.FromResult(new CheckingServiceReply
		{
			MachineName = Environment.MachineName
		});
	}

	public override async Task<FundReply> FundIn(FundRequest request, ServerCallContext context)
	{
		var query = new FundInQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode,
			OrderId: request.OrderId,
			Amount: request.Amount);

		var result = await fundInService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new FundReply()
		{
			Balance = result.Balance,
			Resp = result.Resp
		};
	}

	public override async Task<FundReply> FundOut(FundRequest request, ServerCallContext context)
	{
		var query = new FundOutQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode,
			OrderId: request.OrderId,
			Amount: request.Amount);

		var result = await fundOutService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new FundReply()
		{
			Balance = result.Balance,
			Resp = result.Resp
		};
	}

	public override async Task<BaseReply> FundStatus(FundStatusRequest request, ServerCallContext context)
	{
		var query = new FundStatusQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			OrderId: request.OrderId);

		var result = await fundStatusService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new BaseReply()
		{
			Status = result
		};
	}

	public override async Task<GameLoginReply> GameLogin(GameLoginRequest request, ServerCallContext context)
	{
		var query = new GameLoginQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode);

		var result = await gameLoginGetService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new GameLoginReply()
		{
			UserToken = result
		};
	}

	public override async Task<GameRecordReply> GameRecord(GameRecordRequest request, ServerCallContext context)

	{
		var query = new GameRecordBetLogQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			request.StartTime.ToDateTime(),
			request.EndTime.ToDateTime(),
			request.Page,
			request.PageSize);

		var results = await gameRecordBetLogService
			.QueryAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ToArrayAsync()
			.ConfigureAwait(false)
			?? Enumerable.Empty<GameRecordResult>();

		var reply = new GameRecordReply();

		if (results.Any())
		{
			foreach (var result in results)
			{
				reply.Items.Add(new GameRecordInfo
				{
					PlayerCode = result.PlayerCode,
					ExternalRoundId = result.ExternalRoundId,
					ExternalBetId = result.ExternalBetId,
					ExternalGameId = result.ExternalGameId,
					ExternalCreatedOn = result.ExternalCreatedOn.ToTimestamp(),
					ExternalClosedOn = result.ExternalClosedOn.ToTimestamp(),
					IsFreegame = result.IsFreegame,
					Bet = result.Bet,
					ValidBet = result.ValidBet,
					Settle = result.Settle,
					WinLoss = result.WinLoss,
					Jackpot = result.Jackpot,
					AdditionalData = result.AdditionalData
				});
			}
		}

		return reply;
	}

	public override async Task<GameUrlReply> GameUrl(GameUrlRequest request, ServerCallContext context)
	{
		var query = new GameUrlQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			UserToken: request.UserToken,
			GameCode: request.GameCode,
			IsMobile: request.IsMobile,
			Language: request.Language);

		var result = await gameUrlGetService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new GameUrlReply()
		{
			GameUrl = result.Url
		};
	}

	public override async Task<PlayerBalanceReply> PlayerBalance(PlayerBalanceRequest request, ServerCallContext context)
	{
		var query = new PlayerBalanceQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode);

		var result = await playerBalanceService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new PlayerBalanceReply()
		{
			IsPlayerExist = result.IsPlayerExists,
			Balance = result.Balance
		};
	}

	public override async Task<BaseReply> PlayerCreate(PlayerCreateRequest request, ServerCallContext context)
	{
		var command = new PlayerCreateCommand(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode);

		await playerCreateService.ExecuteAsync(
		   command: command,
		   cancellationToken: context.CancellationToken)
		   .ConfigureAwait(false);

		return new BaseReply()
		{
			Status = true
		};
	}

	public override async Task<BaseReply> PlayerLogout(PlayerLogoutRequest request, ServerCallContext context)
	{
		var command = new PlayerLogoutCommand(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode);

		await playerLogoutService.ExecuteAsync(
			command: command,
			cancellationToken: context.CancellationToken)
		   .ConfigureAwait(false);

		return new BaseReply()
		{
			Status = true
		};
	}

	public override async Task<PlayerOnlineReply> PlayerOnline(PlayerOnlineRequest request, ServerCallContext context)
	{
		var query = new PlayerOnlineQuery(
			ApiUrl: new Uri(request.ApiUrl),
			ApiToken: request.ApiToken,
			PlayerCode: request.PlayerCode);

		var result = await playerOnlineService.GetAsync(
			query: query,
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new PlayerOnlineReply()
		{
			Online = result
		};
	}
}
