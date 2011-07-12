ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [CK_Transfer_DifferentAccounts] CHECK ([SourceAccountId]<>[DestAccountId]);

