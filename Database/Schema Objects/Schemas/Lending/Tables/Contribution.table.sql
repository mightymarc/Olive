CREATE TABLE [Lending].[Contribution]
(
	LoanId INT NOT NULL,
	AccountHoldId INT,
	VolumeContributed DECIMAL(18, 8),
	VolumeRepaid DECIMAL(18, 8),
	InterestRepaid DECIMAL(18, 8)
);
