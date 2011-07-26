ALTER TABLE [Exchange].[Market]
    ADD CONSTRAINT [IX_Market]
    UNIQUE (FromCurrencyId, ToCurrencyId);