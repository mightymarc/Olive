ALTER TABLE [Exchange].[OrderMatch]
	ADD CONSTRAINT [FK_Order_RightOrder] 
	FOREIGN KEY (RightOrderId)
	REFERENCES Exchange.[Order] (OrderId);	

