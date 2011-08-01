CREATE VIEW [Exchange].[ActiveOrderView]

AS

SELECT
	O.OrderId,
	O.Price,
	1 / ROUND(O.Price, 4) + CASE WHEN O.Price % 0.0001 > 0 THEN 1 ELSE 0 END PriceInvert,
	O.Volume,
	O.FromAccountId,
	O.ToAccountId,
	O.AccountHoldId,
	SA.CurrencyId SourceCurrencyId,
	DA.CurrencyId DestCurrencyId,
	O.CreatedAt
FROM
	Exchange.[Order] O
INNER JOIN
	Banking.Account SA ON O.FromAccountId = SA.AccountId
INNER JOIN
	Banking.Account DA ON O.ToAccountId = DA.AccountId
WHERE
	O.Volume > 0;
