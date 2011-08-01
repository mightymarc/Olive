ALTER TABLE [Exchange].[Order]
	ADD CONSTRAINT [CK_Order_DifferentAccounts] 
	CHECK  (FromAccountId <> ToAccountId);
