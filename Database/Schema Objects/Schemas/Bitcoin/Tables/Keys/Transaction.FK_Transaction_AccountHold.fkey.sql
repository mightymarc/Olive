ALTER TABLE [Bitcoin].[Transaction]
	ADD CONSTRAINT [FK_Transaction_AccountHold] 
	FOREIGN KEY (AccountHoldId)
	REFERENCES Banking.AccountHold (AccountHoldId)
	ON DELETE SET NULL;


