CREATE PROCEDURE Exchange.MatchOrder
(
	@LeftOrderId INT
)

AS

DECLARE @TC INT = 0, @RC INT;

IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1;

BEGIN TRY
	DECLARE @LeftSourceAccountId INT;
	DECLARE @LeftDestAccountId INT;
	DECLARE @RightSourceAccountId INT;
	DECLARE @RightDestAccountId INT;
	DECLARE @LeftAccountHoldId INT;
	DECLARE @RightAccountHoldId INT;
	DECLARE @RightPrice DECIMAL(18, 4);
	DECLARE @RightPriceInvert DECIMAL(18, 4);
	DECLARE @RightOrderId INT;
	DECLARE @LeftVolume DECIMAL(18, 4);
	DECLARE @RightVolume DECIMAL(18, 4); -- Relative to its currency, not left's.

	SELECT TOP 1
		@RightPrice = Ro.Price,
		@RightPriceInvert = ROUND(RO.Price, 4) + CASE WHEN RO.Price % 0.0001 > 0 THEN 1 ELSE 0 END,
		@RightVolume = RO.Volume,
		@LeftSourceAccountId = LO.SourceAccountId,
		@LeftDestAccountId = LO.DestAccountId,
		@RightSourceAccountId = RO.SourceAccountId,
		@RightDestAccountId = RO.DestAccountId,
		@LeftVolume = LO.Volume,
		@LeftAccountHoldId = LO.AccountHoldId,
		@RightAccountHoldId = RO.AccountHoldId,
		@RightOrderId = RO.OrderId
	FROM
		Exchange.[Order] LO,
		Exchange.[Order] RO,
		Banking.Account LSA,
		Banking.Account LDA,
		Banking.Account RSA,
		Banking.Account RDA
	WHERE
		LO.OrderId = @LeftOrderId AND
		1 / RO.Price >= LO.Price AND
		LSA.AccountId = LO.SourceAccountId AND
		LDA.AccountId = LO.DestAccountId AND
		RSA.AccountId = RO.SourceAccountId AND
		RDA.AccountId = Ro.DestAccountId AND
		LSA.CurrencyId = RDA.CurrencyId AND
		LDA.CurrencyId = RSA.CurrencyId AND
		RO.Volume > 0 -- Could use a view here instead, such as ActiveOrder
	ORDER BY
		ROUND(1 / RO.Price, 4) + CASE WHEN 1 / RO.Price % 0.0001 > 0 THEN 1 ELSE 0 END DESC,
		RO.CreatedAt ASC;

	IF @RightOrderId IS NULL
	BEGIN
		IF '$(TargetEnv)' = 'Dev'
		BEGIN
			SELECT 'Did not find any right order to match with.';
		END

		SELECT 'All orders of opposite market follows:';

		SELECT
			RO.*
		FROM
			Exchange.[Order] LO,
			Exchange.[Order] RO,
			Banking.Account LSA,
			Banking.Account LDA,
			Banking.Account RSA,
			Banking.Account RDA
		WHERE
			LO.OrderId = @LeftOrderId AND
			--1 / RO.Price >= LO.Price AND
			LSA.AccountId = LO.SourceAccountId AND
			LDA.AccountId = LO.DestAccountId AND
			RSA.AccountId = RO.SourceAccountId AND
			RDA.AccountId = Ro.DestAccountId AND
			LSA.CurrencyId = RDA.CurrencyId AND
			LDA.CurrencyId = RSA.CurrencyId AND
			RO.Volume > 0 -- Could use a view here instead, such as ActiveOrder
		ORDER BY
			ROUND(1 / RO.Price, 4) + CASE WHEN 1 / RO.Price % 0.0001 > 0 THEN 1 ELSE 0 END DESC,
			RO.CreatedAt ASC;

		IF @TC = 0 COMMIT TRAN

		RETURN 0
	END

	DECLARE @FromVolume DECIMAL(18, 4);
	DECLARE @ToVolume DECIMAL(18, 4);

	IF @LeftVolume <= @RightPrice * @RightVolume
	BEGIN
		PRINT 'The size of the order is constrained by the left volume ' + CONVERT(NVARCHAR, @LeftVolume);

		-- Constrained by the left order.
		SELECT @FromVolume = @LeftVolume;
		SELECT @ToVolume = ROUND(@LeftVolume / @RightPrice, 4);
	END
	ELSE
	BEGIN
		PRINT 'The size of the order is constrained by the right volume * right price ' +
			CONVERT(NVARCHAR, @RightVolume * @RightPrice);

		SELECT @FromVolume = ROUND(@RightPrice * @RightVolume, 4);
		SELECT @ToVolume = @RightVolume;
	END

	PRINT '@FromVolume = ' + CONVERT(NVARCHAR, @FromVolume);
	PRINT '@ToVolume = ' + CONVERT(NVARCHAR, @ToVolume);

	-- Create order match
	INSERT INTO Exchange.OrderMatch (LeftOrderId, RightOrderId, FromVolume, ToVolume)
		VALUES (@LeftOrderId, @RightOrderId, @FromVolume, @ToVolume);

	IF @@ROWCOUNT <> 1 RAISERROR('Failed to insert order match.', 16, 1);

	-- Update left order and hold.
	DECLARE @LeftVolumeDelta DECIMAL(18, 4) = -@FromVolume;
	EXEC @RC = Exchange.UpdateOrdervolume @LeftOrderId, @LeftVolumeDelta;
	IF @RC <> 0 RAISERROR('Failed to update left order volume. RC %d.', 16, 1, @RC);

	-- Update right order and hold.
	DECLARE @RightVolumeDelta DECIMAL(18, 4) = -@ToVolume;
	EXEC @RC = Exchange.UpdateOrdervolume @RightOrderId, @RightVolumeDelta;
	IF @RC <> 0 RAISERROR('Failed to update right order volume. RC %d.', 16, 1, @RC);

	-- Transfer
	DECLARE @TransferReason NVARCHAR(250) = N'Exchange order', @TransferId BIGINT;

	EXEC @RC = Banking.CreateTransfer @LeftSourceAccountId, @RightDestAccountId, @TransferReason, @FromVolume, @TransferId OUTPUT;
	IF @RC <> 0 RAISERROR('Failed to execute left to right transfer. RC %d.', 16, 1, @RC);

	SELECT @TransferId = NULL;

	EXEC @RC = Banking.CreateTransfer @RightSourceAccountId, @LeftDestAccountId, @TransferReason, @ToVolume, @TransferId OUTPUT;
	IF @RC <> 0 RAISERROR('Failed to execute right to left transfer. RC %d.', 16, 1, @RC);

	-- Match again if any volume remains
	-- TODO

	IF @TC = 0 COMMIT TRAN;
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

	RETURN ERROR_NUMBER();
END CATCH

