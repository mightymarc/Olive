ALTER TABLE [Exchange].[Order]
	ADD CONSTRAINT [CK_Order_Volume] 
	CHECK  (Volume >= 0);
