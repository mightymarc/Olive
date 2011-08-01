ALTER TABLE [Exchange].[Order]
	ADD CONSTRAINT [FK_Order_SourceAccount] 
	FOREIGN KEY (FromAccountId)
	REFERENCES Banking.Account (AccountId);	

