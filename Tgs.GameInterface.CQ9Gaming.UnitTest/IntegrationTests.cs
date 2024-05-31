using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tgs.GameInterface.CQ9Gaming.Core;
using Tgs.GameInterface.CQ9Gaming.Core.Extensions;
using Tgs.GameInterface.CQ9Gaming.Core.Models;
using Tgs.GameInterface.CQ9Gaming.Services;

namespace Tgs.GameInterface.CQ9Gaming.UnitTest;

public sealed class IntegrationTests
{
    [Fact]
    public async void PlayerCreate()
    {
        var command = new PlayerCreateCommand(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029");
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerCreatedAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>())
            .Returns(ValueTask.CompletedTask);
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/PlayerCreate", JsonContent.Create(command)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        _ = apiClient.Received().PlayerCreatedAsync(
            command.ApiUrl,
            command.ApiToken,
            command.PlayerCode);
    }

    [Fact]
    public async void PlayerLogin()
    {
        var query = new PlayerLoginQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029");
        var userToken = Guid.NewGuid().ToString("N");
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerLoginAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>())
            .Returns(ValueTask.FromResult(userToken));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/PlayerLogin", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<JsonValue>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.Equal(userToken, response.GetValue<string>());
        _ = apiClient.Received().PlayerLoginAsync(
            query.ApiUrl,
            query.ApiToken,
            query.PlayerCode);
    }

    [Fact]
    public async void PlayerLogout()
    {
        var command = new PlayerLogoutCommand(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029");
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerLogoutAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>())
            .Returns(ValueTask.CompletedTask);
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/PlayerLogout", JsonContent.Create(command)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        _ = apiClient.Received().PlayerLogoutAsync(
            command.ApiUrl,
            command.ApiToken,
            command.PlayerCode);
    }

    [Fact]
    public async void PlayerIsGaming()
    {
        var query = new PlayerIsGamingQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029");
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerTokenAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>())
            .Returns(ValueTask.FromResult("login"));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/PlayerIsGaming", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<JsonValue>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.True(response.GetValue<bool>());
        _ = apiClient.Received().PlayerTokenAsync(
            query.ApiUrl,
            query.ApiToken,
            query.PlayerCode);
    }

    [Fact]
    public async void GameUrl()
    {
        var token = Guid.NewGuid().ToString("N");
        var query = new GameUrlQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            UserToken: Guid.NewGuid().ToString("N"),
            GameCode: Guid.NewGuid().ToString("N"),
            IsMobile: true,
            Language: "zh-cn");
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerGamelinkAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            userToken: Arg.Any<string>(),
            gameHall: Arg.Any<string>(),
            gameCode: Arg.Any<string>(),
            gamePlat: Arg.Any<string>(),
            lang: Arg.Any<string>())
            .Returns(ValueTask.FromResult(new PlayerGamelinkResponse(
                Url: "https://xxx.xxx.xxx/xxx",
                Token: token)));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/GameUrl", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<GameUrlResult>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.Equal("https://xxx.xxx.xxx/xxx", response.Url);
        Assert.Equal(token, response.Token);
        _ = apiClient.Received().PlayerGamelinkAsync(
            query.ApiUrl,
            query.ApiToken,
            query.UserToken,
            Gamehall.CQ9,
            query.GameCode,
            query.IsMobile ? "mobile" : "web",
            query.Language);
    }

    [Fact]
    public async void FundIn()
    {
        var query = new FundInQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029",
            OrderId: Guid.NewGuid().ToString("N"),
            Amount: Random.Shared.Next());
        var balance = 10m;
        var res = new JsonObject
        {
            ["balance"] = balance,
            ["currency"] = "CNY"
        }.ToJsonString();
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerWithdrawAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>(),
            mtCode: Arg.Any<string>(),
            amount: Arg.Any<decimal>())
            .Returns(ValueTask.FromResult(new PlayerWithdrawResponse(
                Balance: balance,
                Res: res)));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/FundIn", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<FundInResult>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.Equal(balance, response.Balance);
        Assert.Equal(res, response.Res);
        _ = apiClient.Received().PlayerWithdrawAsync(
            query.ApiUrl,
            query.ApiToken,
            query.PlayerCode,
            query.OrderId,
            query.Amount);
    }

    [Fact]
    public async void FundOut()
    {
        var query = new FundOutQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029",
            OrderId: Guid.NewGuid().ToString("N"),
            Amount: Random.Shared.Next());
        var balance = 10m;
        var res = new JsonObject
        {
            ["balance"] = balance,
            ["currency"] = "CNY"
        }.ToJsonString();
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerDepositAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>(),
            mtCode: Arg.Any<string>(),
            amount: Arg.Any<decimal>())
            .Returns(ValueTask.FromResult(new PlayerDepositResponse(
                Balance: balance,
                Res: res)));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync("/CQ9Gaming/FundOut", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<FundOutResult>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.Equal(balance, response.Balance);
        Assert.Equal(res, response.Res);
        _ = apiClient.Received().PlayerDepositAsync(
            query.ApiUrl,
            query.ApiToken,
            query.PlayerCode,
            query.OrderId,
            query.Amount);
    }

    [Fact]
    public async void PlayerBalance()
    {
        var query = new PlayerBalanceQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            PlayerCode: "4ucmqplxb029");
        var balance = (decimal)Random.Shared.Next();
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.PlayerBalanceAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            account: Arg.Any<string>())
            .Returns(ValueTask.FromResult(new PlayerBalanceResponse(
                IsPlayerExists: true,
                Balance: balance)));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync($"/CQ9Gaming/PlayerBalance", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<PlayerBalanceResult>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.True(response.IsPlayerExists);
        Assert.Equal(balance, response.Balance);
        _ = apiClient.Received().PlayerBalanceAsync(
            query.ApiUrl,
            query.ApiToken,
            query.PlayerCode);
    }

    [Fact]
    public async void GameWagerBetLog()
    {
        var now = DateTimeOffset.UtcNow.Truncate(TimeSpan.FromSeconds(1)); // parse 成 query string 後會不準確
        var betTime = now;
        var createTime = betTime.AddMinutes(Random.Shared.NextDouble());
        var endRoundTime = betTime.AddMinutes(Random.Shared.NextDouble());
        var round = Guid.NewGuid().ToString("N");
        var gametype = "slot";
        var account = "4ucmqplxe65x";
        var bet = (decimal)Random.Shared.Next();
        var validBet = (decimal)Random.Shared.Next();
        var jackpot = (decimal)Random.Shared.Next();
        var rake = (decimal)Random.Shared.Next();
        var roomfee = (decimal)Random.Shared.Next();
        var win = (decimal)Random.Shared.Next();
        var query = new GameWagerBetLogQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            StartTime: now.AddHours(1),
            EndTime: now);
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.OrderViewAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            startTime: Arg.Any<DateTimeOffset>(),
            endTime: Arg.Any<DateTimeOffset>(),
            page: query.Page,
            pageSize: query.PageSize)
            .Returns(new[] { new OrderViewResponseItem(
                Round: round,
                Gamehall: Gamehall.CQ9,
                Gametype: gametype,
                Gameplat: "mobile",
                Gamecode: "CC12",
                Account: account,
                Bet: bet,
                ValidBet: validBet,
                Win: win,
                Roomfee: roomfee,
                Rake: rake,
                Jackpot: jackpot,
                Bettime: betTime,
                Endroundtime: endRoundTime,
                AdditionalData: string.Empty) }.ToAsyncEnumerable());
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync($"/CQ9Gaming/GameWagerBetLog", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<GameWagerResult[]>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.Single(response);
        var data = response[0];
        var winLoss = gametype.ToLower() switch
        {
            "table" => win - rake - roomfee,
            "live" => win - rake - roomfee,
            _ => win - bet
        };
        var rValidBet = gametype.ToLower() switch
        {
            "table" => validBet,
            "live" => validBet,
            _ => bet
        };
        var settle = rValidBet + winLoss;
        Assert.Equal(round, data.ExternalWagerId);
        Assert.Equal("CC12", data.ExternalGameId);
        Assert.Equal(account, data.PlayerCode);
        Assert.Equal(betTime, data.ExternalCreatedOn);
        Assert.Equal(endRoundTime, data.ExternalClosedOn);
        Assert.Equal(bet, data.Bet);
        Assert.Equal(rValidBet, data.ValidBet);
        Assert.Equal(settle, data.Settle);
        Assert.Equal(winLoss, data.WinLoss);
        Assert.Equal(jackpot, data.Jackpot);
        _ = apiClient.Received().OrderViewAsync(
            query.ApiUrl,
            query.ApiToken,
            query.StartTime,
            query.EndTime,
            query.Page,
            query.PageSize);
    }

    [Fact]
    public async void FundStatus()
    {
        var query = new FundStatusQuery(
            ApiUrl: new Uri("https://api.cqgame.games"),
            ApiToken: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
            OrderId: Guid.NewGuid().ToString("N"));
        var sample = Random.Shared.Next(2) > 0;
        var apiClient = Substitute.For<IGameApiClient>();
        _ = apiClient.TransactionRecordAsync(
            apiUrl: Arg.Any<Uri>(),
            apiToken: Arg.Any<string>(),
            mtCode: Arg.Any<string>())
            .Returns(ValueTask.FromResult(sample));
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                services.Remove(services.First(d => d.ServiceType == typeof(IGameApiClient)));
                services.AddSingleton(apiClient);
            }));
        var client = factory.CreateClient();

        var responseMessage = await client.PostAsync($"/CQ9Gaming/FundStatus", JsonContent.Create(query)).ConfigureAwait(true);

        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadFromJsonAsync<JsonValue>().ConfigureAwait(true);
        Assert.NotNull(response);
        Assert.Equal(sample, response.GetValue<bool>());
        _ = apiClient.Received().TransactionRecordAsync(
            query.ApiUrl,
            query.ApiToken,
            query.OrderId);
    }
}
