ALTER TABLE [Banking].[Account]
    ADD CONSTRAINT [FK_Account_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([CurrencyId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

