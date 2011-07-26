ALTER TABLE [Exchange].[Market]
	ADD CONSTRAINT [FK_Market_ToCurrency] 
	FOREIGN KEY (ToCurrencyId)
	REFERENCES dbo.Currency (CurrencyId);	

