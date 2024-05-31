using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

namespace Tgs.GameInterface.CQ9Gaming.Core.UnitTest;

public sealed class GameApiClient_PlayerWithdrawTest
{
	[Fact]
	public async Task PlayerWithdrawAsync_RespondStatusCode0_ResultBalance()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var account = "4ucmqplxe65x";
		var mtCode = Guid.NewGuid().ToString("N");
		var amount = Random.Shared.Next();
		var balance = Random.Shared.Next();
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Post, new Uri(apiUrl, "/gameboy/player/withdraw").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactFormData(new SortedDictionary<string, string>
			{
				["account"] = account,
				["mtcode"] = mtCode,
				["amount"] = amount.ToString("0.####"),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = new JsonObject
				{
					["balance"] = balance,
				},
				["status"] = new JsonObject
				{
					["code"] = "0",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var result = await apiClient.PlayerWithdrawAsync(
			apiUrl,
			apiToken,
			account,
			mtCode,
			amount)
			.ConfigureAwait(true);

		Assert.Equal(balance, result.Balance);
	}

	[Fact]
	public async Task PlayerWithdrawAsync_RespondStatusCode1_TaskFaultedErrorCodeException()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var account = "4ucmqplxe65x";
		var mtCode = Guid.NewGuid().ToString("N");
		var amount = Random.Shared.Next();
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Post, new Uri(apiUrl, "/gameboy/player/withdraw").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactFormData(new SortedDictionary<string, string>
			{
				["account"] = account,
				["mtcode"] = mtCode,
				["amount"] = amount.ToString("0.####"),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = null,
				["status"] = new JsonObject
				{
					["code"] = "1",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var exception = await Assert.ThrowsAnyAsync<ErrorCodeException>(async ()
			=> await apiClient.PlayerWithdrawAsync(
				apiUrl,
				apiToken,
				account,
				mtCode,
				amount)
				.ConfigureAwait(true))
			.ConfigureAwait(true);

		Assert.Equal("1", exception.Code);
	}

	[Fact]
	public async Task PlayerWithdrawAsync_RespondStatusCode2_TaskFaultedErrorCodeException()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var account = "4ucmqplxe65x";
		var mtCode = Guid.NewGuid().ToString("N");
		var amount = Random.Shared.Next();
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Post, new Uri(apiUrl, "/gameboy/player/withdraw").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactFormData(new SortedDictionary<string, string>
			{
				["account"] = account,
				["mtcode"] = mtCode,
				["amount"] = amount.ToString("0.####"),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = null,
				["status"] = new JsonObject
				{
					["code"] = "2",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var exception = await Assert.ThrowsAnyAsync<ErrorCodeException>(async ()
			=> await apiClient.PlayerWithdrawAsync(
				apiUrl,
				apiToken,
				account,
				mtCode,
				amount)
				.ConfigureAwait(true))
			.ConfigureAwait(true);

		Assert.Equal("2", exception.Code);
	}

	[Fact]
	public async Task PlayerWithdrawAsync_RespondStatusCode100_TaskFaultedErrorCodeException()
	{
		var apiUrl = new Uri("https://api.cqgame.games");
		var apiToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE";
		var account = "4ucmqplxe65x";
		var mtCode = Guid.NewGuid().ToString("N");
		var amount = Random.Shared.Next();
		var handler = new MockHttpMessageHandler();
		handler.Expect(HttpMethod.Post, new Uri(apiUrl, "/gameboy/player/withdraw").ToString())
			.WithHeaders("Authorization", apiToken)
			.WithExactFormData(new SortedDictionary<string, string>
			{
				["account"] = account,
				["mtcode"] = mtCode,
				["amount"] = amount.ToString("0.####"),
			})
			.Respond(HttpStatusCode.OK, JsonContent.Create(new JsonObject
			{
				["data"] = null,
				["status"] = new JsonObject
				{
					["code"] = "100",
				}
			}));
		var httpClient = handler.ToHttpClient();
		var logger = NullLogger<GameApiClient>.Instance;
		var apiClient = new GameApiClient(logger, httpClient);

		var exception = await Assert.ThrowsAnyAsync<ErrorCodeException>(async ()
			=> await apiClient.PlayerWithdrawAsync(
				apiUrl,
				apiToken,
				account,
				mtCode,
				amount)
				.ConfigureAwait(true))
			.ConfigureAwait(true);

		Assert.Equal("100", exception.Code);
	}
}
