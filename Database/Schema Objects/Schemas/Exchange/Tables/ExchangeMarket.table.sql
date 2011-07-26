CREATE TABLE [Exchange].[Market]
(
	MarketId INT IDENTITY NOT NULL,
	FromCurrencyId VARCHAR(10) NOT NULL,
	ToCurrencyId VARCHAR(10) NOT NULL
);
