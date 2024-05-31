namespace Tgs.GameInterface.CQ9Gaming.Core.Models;

public sealed record class OrderViewResponseItem(
	string Account,
	string Roundnumber,
	string Round,
	string Gamehall,
	string Gametype,
	string Gameplat,
	string Gamecode,
	int Freegame,
	decimal Bet,
	decimal ValidBet,
	decimal Win,
	decimal Roomfee,
	decimal Rake,
	decimal Jackpot,
	DateTimeOffset Bettime,
	DateTimeOffset Endroundtime,
	string AdditionalData);
