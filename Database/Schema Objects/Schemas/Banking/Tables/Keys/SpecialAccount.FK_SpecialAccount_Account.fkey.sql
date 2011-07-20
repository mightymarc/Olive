ALTER TABLE [Banking].[SpecialAccount]
	ADD CONSTRAINT [FK_SpecialAccount_Account] 
	FOREIGN KEY (AccountId)
	REFERENCES Banking.Account (AccountId);	

