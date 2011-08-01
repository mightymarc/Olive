CREATE TABLE [Lending].[Loan]
(
	CreatedAt DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	[State] VARCHAR(50) NOT NULL DEFAULT('Applying'), -- Applying, Applied, Collecting, Refunded, Repaying, Repaid 
	AppliedAt DATETIME,
	RefundedAt DATETIME,
	RepayStartedAt DATETIME,
	CollectDeadline DATETIME,
	RepayDeadline DATETIME,
	InterestRate DECIMAL(10, 4),
	UserId INT NOT NULL,
	RepayAccountId INT,
	DepositAccountId INT,
	DesiredVolume DECIMAL(18, 8),
	ApplicationFeeAccountHoldId INT
);
