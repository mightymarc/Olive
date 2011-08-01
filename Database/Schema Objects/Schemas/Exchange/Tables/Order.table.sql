CREATE TABLE [Exchange].[Order]
(
	OrderId INT NOT NULL IDENTITY,
	FromAccountId INT NOT NULL,
	ToAccountId INT NOT NULL,

	-- Because trading performs multiplication/division, the sum of the number of decimal
	-- places must not exceed 8. (In this case, 4+4=8)
	Volume DECIMAL(18, 4) NOT NULL,
	Price DECIMAL(18, 4) NOT NULL,
	CreatedAt DATETIME NOT NULL DEFAULT(GETUTCDATE()),
	AccountHoldId INT
);
