ALTER TABLE [Exchange].[Order]
	ADD CONSTRAINT [CK_Order_Price] 
	CHECK  (Price > 0);
