ALTER TABLE [Banking].[AccountUser]
    ADD CONSTRAINT [FK_AccountUser_Account] FOREIGN KEY ([AccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

