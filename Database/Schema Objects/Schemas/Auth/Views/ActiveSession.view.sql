CREATE VIEW [Auth].[ActiveSession]
	WITH SCHEMABINDING
	AS SELECT [SessionId], [ExpiresAt], [UserId] FROM [Auth].[Session] WHERE ExpiresAt > GETUTCDATE();
