namespace Tgs.GameInterface.CQ9Gaming.Core.Exceptions;

public sealed class ErrorCodeException(string code) : Exception(ConvertErrorMesaage(code))
{
	public string Code { get; } = code;

	private static string ConvertErrorMesaage(string code) => code switch
	{
		"0" => "success",
		"1" => "Insufficient balance.",
		"2" => "Player not found.",
		"3" => "Token invalid.",
		"4" => "Authorization invalid.",
		"5" => "Bad parameters.",
		"6" => "Already has same account.",
		"8" => "Data not found.",
		"9" => "MTCode duplicate.",
		"10" => "Time format error.",
		"11" => "Query time is out of range.",
		"12" => "Time zone must be UTC-4.",
		"13" => "Game is not found.",
		"14" => "Your account or password is incorrect.",
		"15" => "Account or password must use the following characters:a-z A-Z 0-9 -_",
		"23" => "Game is under maintenance.(Single CQ9 game is under maintenance)",
		"24" => "Account too long.",
		"26" => "CQ9 Gaming is under maintain.(All CQ9 games are under maintenance)",
		"28" => "Currency is not support.",
		"29" => "No default pool type",
		"30" => "Pool uninitialize.",
		"31" => "Currency does not match Agent's currency.",
		"33" => "Transaction in progress, please check later.",
		"35" => "StartTime and EndTime are future time.",
		"38" => "This parameter is available for general agent only",
		"39" => "Deposit and withdrawal of the account has been forbidden",
		"100" => "Something wrong.",
		"101" => "Auth service error.",
		"102" => "User service error.",
		"103" => "Transaction service error",
		"104" => "Game Manager service error",
		"105" => "Wallet service error.",
		"106" => "Tviewer service error.",
		"107" => "Orderview service error.",
		"108" => "Report service error.",
		"110" => "Promote service error.",
		"111" => "PromoteRed service error.",
		"112" => "LuckyBag service error.",
		"200" => "This owner has been frozen.",
		"201" => "This owner has been disable.",
		"202" => "This player has been disable.",
		_ => $"遊戲供應商未提供錯誤代碼{code}"
	};
}
