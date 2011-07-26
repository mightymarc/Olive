ALTER TABLE [Exchange].[Market]
	ADD CONSTRAINT [CK_DifferentCurrencies] 
	CHECK  (FromCurrencyId <> ToCurrencyId);
