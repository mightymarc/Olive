ALTER TABLE [Bitcoin].[Transaction]
	ADD CONSTRAINT [FK_Transaction_Account] 
	FOREIGN KEY (AccountId)
	REFERENCES Banking.Account (AccountId);	

