ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [FK_Transfer_ToAccount] 
		FOREIGN KEY ([ToAccountId]) 
		REFERENCES [Banking].[Account] ([AccountId]);

