ALTER TABLE [Exchange].[Market]
	ADD CONSTRAINT [FK_Market_FromCurrency] 
	FOREIGN KEY (FromCurrencyId)
	REFERENCES dbo.Currency (CurrencyId);	

