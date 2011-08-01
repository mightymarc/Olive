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
		@RightPrice = RO.Price,
		@RightPriceInvert = RO.PriceInvert,
		@RightVolume = RO.Volume,
		@LeftSourceAccountId = LO.FromAccountId,
		@LeftDestAccountId = LO.ToAccountId,
		@RightSourceAccountId = RO.FromAccountId,
		@RightDestAccountId = RO.ToAccountId,
		@LeftVolume = LO.Volume,
		@LeftAccountHoldId = LO.AccountHoldId,
		@RightAccountHoldId = RO.AccountHoldId,
		@RightOrderId = RO.OrderId
	FROM
		Exchange.ActiveOrderView LO,
		Exchange.ActiveOrderView RO
	WHERE
		LO.OrderId = @LeftOrderId AND
		RO.PriceInvert >= LO.Price AND
		LO.SourceCurrencyId = RO.DestCurrencyId AND
		LO.DestCurrencyId = RO.SourceCurrencyId
	ORDER BY
		RO.PriceInvert DESC,
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
			Exchange.ActiveOrderView LO,
			Exchange.ActiveOrderView RO
		WHERE
			LO.OrderId = @LeftOrderId AND
			LO.SourceCurrencyId = RO.DestCurrencyId AND
			LO.DestCurrencyId = RO.SourceCurrencyId
		ORDER BY
			RO.PriceInvert DESC,
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

	-- Create transfer from right to left
	PRINT 'Creating transfer from left source, ' + CONVERT(VARCHAR, @LeftSourceAccountId) +
		', to right dest, ' + CONVERT(VARCHAR, @RightDestAccountId) + ' for ' +
		CONVERT(VARCHAR, @FromVolume) + '.';

	EXEC @RC = Banking.CreateTransfer @LeftSourceAccountId, @RightDestAccountId, @TransferReason, @TransferReason, @FromVolume, @TransferId OUTPUT;
	IF @RC <> 0 RAISERROR('Failed to execute left to right transfer. RC %d.', 16, 1, @RC);

	SELECT @TransferId = NULL;

	-- Create transfer from right to left
	PRINT 'Creating transfer from right source, ' + CONVERT(VARCHAR, @RightSourceAccountId) +
		', to left dest, ' + CONVERT(VARCHAR, @LeftDestAccountId) + ' for ' +
		CONVERT(VARCHAR, @ToVolume) + '.';

	EXEC @RC = Banking.CreateTransfer @RightSourceAccountId, @LeftDestAccountId, @TransferReason, @TransferReason, @ToVolume, @TransferId OUTPUT;
	IF @RC <> 0 RAISERROR('Failed to execute right to left transfer. RC %d.', 16, 1, @RC);

	-- Charge right dest. a fee.
	DECLARE @FromFee DECIMAL(18, 4) = @FromVolume * CONVERT(FLOAT, dbo.GetSetting('Exchange match fee ratio'));

	PRINT 'Charging right dest a fee of ' + CONVERT(VARCHAR, @FromFee) + '.';

	EXEC @RC = Banking.ChargeFee @RightDestAccountId, @FromFee, 'Exchange fee', NULL;

	IF @RC <> 0 RAISERROR('Failed to charge fee from right dest account. RC %d.', 16, 1, @RC);

	-- Charge left dest a fee.
	DECLARE @ToFee DECIMAL(18, 4) = @ToVolume * CONVERT(FLOAT, dbo.GetSetting('Exchange match fee ratio'));

	PRINT 'Charging left dest a fee of ' + CONVERT(VARCHAR, @ToFee) + '.';

	EXEC @RC = Banking.ChargeFee @LeftDestAccountId, @ToFee, 'Exchange fee', NULL;

	IF @RC <> 0 RAISERROR('Failed to charge fee from left dest account. RC %d.', 16, 1, @RC);

	-- Recursively match if any left volume remains.
	IF @LeftVolumeDelta + @LeftVolume > 0
	BEGIN
		EXEC @RC = Exchange.MatchOrder @LeftOrderId;

		IF @RC <> 0 RAISERROR('Failed to recursivelt match order. RC %d.', 16, 1, @RC);
	END

	IF @TC = 0 COMMIT TRAN;
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

	PRINT ERROR_MESSAGE();

	RETURN ERROR_NUMBER();
END CATCH

