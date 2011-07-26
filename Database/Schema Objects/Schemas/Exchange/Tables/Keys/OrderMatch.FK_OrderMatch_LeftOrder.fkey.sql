ALTER TABLE [Exchange].[OrderMatch]
	ADD CONSTRAINT [FK_OrderMatch_LeftOrder] 
	FOREIGN KEY (LeftOrderId)
	REFERENCES Exchange.[Order] (OrderId);	

