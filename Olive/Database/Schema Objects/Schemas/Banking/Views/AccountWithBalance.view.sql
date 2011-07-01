CREATE VIEW Banking.AccountWithBalance
WITH SCHEMABINDING
AS

SELECT
	AccountId,
	DisplayName,
	CurrencyId,
	[Type],
	AllowNegative,
		(SELECT ISNULL(SUM(TD.Amount), 0) FROM Banking.[Transfer] TD WHERE TD.DestAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(TS.Amount), 0) FROM Banking.[Transfer] TS WHERE TS.SourceAccountId = A.AccountId) 
	Balance
FROM
	Banking.[Account] A;

