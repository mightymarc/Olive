CREATE VIEW [Banking].[AccountHoldView]
	WITH SCHEMABINDING
	AS SELECT AccountHoldId, AccountId, Amount, ExpiresAt, Reason
	FROM Banking.AccountHold
	WHERE ExpiresAt > GETUTCDATE();
