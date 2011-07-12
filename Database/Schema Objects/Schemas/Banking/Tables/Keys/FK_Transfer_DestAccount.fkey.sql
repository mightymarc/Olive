ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [FK_Transfer_DestAccount] 
		FOREIGN KEY ([DestAccountId]) 
		REFERENCES [Banking].[Account] ([AccountId]);

