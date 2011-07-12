CREATE TABLE [Bitcoin].[Transaction]
(
	TransactionId VARCHAR(64) NOT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	AccountId INT NOT NULL,
	AccountHoldId INT,
	Amount DECIMAL(18, 8) NOT NULL
);
