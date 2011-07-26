ALTER TABLE [Bitcoin].[AccountReceiveAddress]
	ADD CONSTRAINT [FK_AccountReceiveAddress_Account] 
	FOREIGN KEY (AccountId)
	REFERENCES Banking.Account (AccountId);	

