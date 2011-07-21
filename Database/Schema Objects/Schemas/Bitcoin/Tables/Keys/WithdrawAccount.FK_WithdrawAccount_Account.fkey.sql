ALTER TABLE [Bitcoin].[WithdrawAccount]
	ADD CONSTRAINT [FK_WithdrawAccount_Account] 
	FOREIGN KEY (AccountId)
	REFERENCES Banking.Account (AccountId);