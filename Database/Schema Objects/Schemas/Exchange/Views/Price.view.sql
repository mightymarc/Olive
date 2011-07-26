CREATE VIEW [Exchange].[Price]

AS 
	
SELECT
	M.MarketId MarketId, 
	O.Price Price, 
	SUM(O.Volume) Volume
FROM
	Exchange.[Order] O
INNER JOIN
	Banking.Account SA ON SA.AccountId = O.SourceAccountId
INNER JOIN
	Banking.Account DA ON DA.AccountId = O.DestAccountId
INNER JOIN
	Exchange.Market M ON M.FromCurrencyId = SA.CurrencyId AND
		M.ToCurrencyId = DA.CurrencyId
GROUP BY
	MarketId, Price;

