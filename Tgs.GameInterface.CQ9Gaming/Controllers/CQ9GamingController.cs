using Microsoft.AspNetCore.Mvc;
using Tgs.GameInterface.CQ9Gaming.Core.ApplicationServices;

using Tgs.ServiceUtility;

namespace Tgs.GameInterface.CQ9Gaming.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Produces("application/json")]
public sealed class CQ9GamingController : ControllerBase
{
	/// <summary>
	/// Players the create.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="command">The command.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask PlayerCreate(
		[FromServices] ICommandService<PlayerCreateCommand> service,
		[FromBody] PlayerCreateCommand command)
		=> service.ExecuteAsync(command);

	/// <summary>
	/// Players the logout.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask PlayerLogout(
		[FromServices] ICommandService<PlayerLogoutCommand> service,
		[FromBody] PlayerLogoutCommand query)
		=> service.ExecuteAsync(query);

	/// <summary>
	/// Players the online.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<bool> PlayerOnline(
		[FromServices] IGetService<PlayerOnlineQuery, bool> service,
		[FromBody] PlayerOnlineQuery query)
		=> service.GetAsync(query);

	/// <summary>
	/// Games the login.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<string> GameLogin(
		[FromServices] IGetService<GameLoginQuery, string> service,
		[FromBody] GameLoginQuery query)
		=> service.GetAsync(query);

	/// <summary>
	/// Games the URL.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "userToken": "XXXXX",
	///     "gameCode": "XXXXX",
	///     "isMobile": false,
	///     "language": "zh-cn"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<GameUrlResult> GameUrl(
		[FromServices] IGetService<GameUrlQuery, GameUrlResult> service,
		[FromBody] GameUrlQuery query)
		=> service.GetAsync(query);

	/// <summary>
	/// Funds the in.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029",
	///     "orderId": "XXXXX",
	///     "amount": 10
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<FundInResult> FundIn(
		[FromServices] IGetService<FundInQuery, FundInResult> service,
		[FromBody] FundInQuery query)
		=> service.GetAsync(query);

	/// <summary>
	/// Funds the out.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029",
	///     "orderId": "XXXXX",
	///     "amount": 10
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<FundOutResult> FundOut(
		[FromServices] IGetService<FundOutQuery, FundOutResult> service,
		[FromBody] FundOutQuery query)
		=> service.GetAsync(query);

	/// <summary>
	/// Players the balance.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "playerCode": "4ucmqplxb029"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<PlayerBalanceResult> PlayerBalance(
		[FromServices] IGetService<PlayerBalanceQuery, PlayerBalanceResult> service,
		[FromBody] PlayerBalanceQuery query)
		=> service.GetAsync(query);

	/// <summary>
	/// Games the wager bet log.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "startTime": "2024-05-27T16:00:00+08:00",
	///     "endTime": "2024-05-27T16:30:00+08:00",
	///     "page": 1,
	///     "pageSize": 500
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<GameRecordResult[]> GameRecord(
		[FromServices] IQueryService<GameRecordBetLogQuery, GameRecordResult> service,
		[FromBody] GameRecordBetLogQuery query)
		=> service.QueryAsync(query).ToArrayAsync();

	/// <summary>
	/// Funds the status.
	/// </summary>
	/// <param name="service">The service.</param>
	/// <param name="query">The query.</param>
	/// <returns></returns>
	/// <remarks>
	/// <b>Example</b>
	///
	/// Post
	/// {
	///     "apiUrl": "https://api.cqgame.games",
	///     "apiToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyaWQiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJhY2NvdW50IjoicGdiIiwib3duZXIiOiI2NjRkNjFkZDc2ZWViMzc2ZDMwYzZlMWUiLCJwYXJlbnQiOiJzZWxmIiwiY3VycmVuY3kiOiJDTlkiLCJicmFuZCI6ImNxOSIsImp0aSI6IjUzNjA2NTAyNiIsImlhdCI6MTcxNjM0NzM1NywiaXNzIjoiQ3lwcmVzcyIsInN1YiI6IlNTVG9rZW4ifQ._3At-myRESwuAzQkRX6yd3S9gF0Ax8cjMsJ46J7disE",
	///     "orderId": "XXXXX"
	/// }
	/// </remarks>
	[HttpPost]
	public ValueTask<bool> FundStatus(
		[FromServices] IGetService<FundStatusQuery, bool> service,
		[FromBody] FundStatusQuery query)
		=> service.GetAsync(query);
}
