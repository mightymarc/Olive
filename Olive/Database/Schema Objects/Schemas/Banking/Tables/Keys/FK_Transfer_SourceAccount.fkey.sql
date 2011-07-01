ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [FK_Transfer_SourceAccount] FOREIGN KEY ([SourceAccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

