ALTER TABLE [Exchange].[OrderMatch]
	ADD CONSTRAINT [CK_LeftOrderRightAndOrderAreDifferent] 
	CHECK  (LeftOrderId <> RightOrderId);
