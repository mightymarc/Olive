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
		(SELECT ISNULL(SUM(TS.Amount), 0) FROM Banking.[Transfer] TS WHERE TS.SourceAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(Amount), 0) FROM Banking.AccountHoldView AHV WHERE AHV.AccountId = A.AccountId)
	Available
FROM
	Banking.[Account] A;
