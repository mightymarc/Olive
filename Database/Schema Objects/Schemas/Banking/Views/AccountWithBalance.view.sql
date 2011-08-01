CREATE VIEW Banking.AccountWithBalance
WITH SCHEMABINDING
AS

SELECT
	AccountId,
	DisplayName,
	CurrencyId,
	[Type],
	AllowNegative,
		(SELECT ISNULL(SUM(TD.Volume), 0) FROM Banking.[Transfer] TD WHERE TD.ToAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(TS.Volume), 0) FROM Banking.[Transfer] TS WHERE TS.FromAccountId = A.AccountId)
	Balance,
	(SELECT ISNULL(SUM(Volume), 0) FROM Banking.AccountHoldView AHV WHERE AHV.AccountId = A.AccountId) 
	Held,
		(SELECT ISNULL(SUM(TD.Volume), 0) FROM Banking.[Transfer] TD WHERE TD.ToAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(TS.Volume), 0) FROM Banking.[Transfer] TS WHERE TS.FromAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(Volume), 0) FROM Banking.AccountHoldView AHV WHERE AHV.AccountId = A.AccountId) Available,
	[AnyCanDeposit]
FROM
	Banking.[Account] A;