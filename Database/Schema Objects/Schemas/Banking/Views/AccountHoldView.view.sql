CREATE VIEW [Banking].[AccountHoldView]
	WITH SCHEMABINDING
	AS SELECT AccountHoldId, AccountId, Volume, ExpiresAt, Reason
	FROM Banking.AccountHold
	WHERE ExpiresAt IS NULL OR ExpiresAt > GETUTCDATE();
