ALTER TABLE [Exchange].[Order]
	ADD CONSTRAINT [CK_Order_VolumeRemainingXorAccountHold] 
	CHECK  
	(
		-- AccountHoldId IS NULL XOR ReaminingVolume > 0
		(CASE WHEN AccountHoldId IS NULL THEN 0 ELSE 1 END) +
		(CASE WHEN Volume > 0 THEN 0 ELSE 1 END)
		=
		1
	);
