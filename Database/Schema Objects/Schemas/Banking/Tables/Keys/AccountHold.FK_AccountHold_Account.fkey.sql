ALTER TABLE [Banking].[AccountHold]
	ADD CONSTRAINT [FK_AccountHold_Account] 
	FOREIGN KEY (AccountId)
	REFERENCES Banking.Account (AccountId);	

