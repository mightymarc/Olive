CREATE PROCEDURE [Exchange].[UpdateOrderVolume]
(
	@OrderId INT,
	@VolumeDelta DECIMAL(18, 4)
)

AS

DECLARE @TC INT = @@TRANCOUNT;
IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1;

BEGIN TRY
	IF @OrderId IS NULL
		RAISERROR(51003, 16, 1, '@OrderId');

	IF @VolumeDelta IS NULL
		RAISERROR(51003, 16, 1, '@VolumeDelta');

	DECLARE @OldVolume DECIMAL(18, 4), @AccountHoldId INT;
	SELECT @OldVolume = Volume, @AccountHoldId = AccountHoldId FROM Exchange.[Order] WHERE OrderId = @OrderId;

	IF @VolumeDelta > 0
		RAISERROR(51015, 16, 1, @OrderId);

	IF @OldVolume IS NULL
		RAISERROR(51016, 16, 1, @OrderId);

	IF @AccountHoldId IS NULL
		RAISERROR(51017, 16, 1, 'The specified order does not have a hold on it.');

	IF @OldVolume <= 0
		RAISERROR(51017, 16, 1, 'The specified order has a volume of zero or less.');

	IF '$(TargetEnv)' = 'Dev'
	BEGIN
		PRINT 'When updating order volume for #' + CONVERT(NVARCHAR, @OrderId) + ' old volume is ' +
			CONVERT(NVARCHAR, @Oldvolume) + ' and delta is ' + CONVERT(NVARCHAR, @VolumeDelta) + '.';
	END

	IF @OldVolume + @VolumeDelta = 0
	BEGIN
		UPDATE Exchange.[Order] SET AccountHoldId = NULL, Volume = 0 WHERE OrderId = @OrderId;

		IF @@ROWCOUNT <> 1
			RAISERROR(51018, 16, 1, 'Remove hold and set volume to zero');

		DELETE FROM Banking.AccountHold WHERE AccountHoldId = @AccountHoldId;

		IF @@ROWCOUNT <> 1
			RAISERROR(51019, 16, 1, 'Account hold');
	END
	ELSE IF @OldVolume + @VolumeDelta > 0
	BEGIN
		UPDATE Exchange.[Order] SET Volume += @VolumeDelta WHERE OrderId = @OrderId;

		IF @@ROWCOUNT <> 1
			RAISERROR(51018, 16, 1, 'New order volume');

		UPDATE Banking.[AccountHold] SET Amount += @VolumeDelta WHERE AccountHoldId = @AccountHoldId;

		IF @@ROWCOUNT <> 1
			RAISERROR(51018, 16, 1, 'Updated hold amount');
	END
	ELSE
		RAISERROR(51017, 16, 1, 'Cannot subtract from the order so that it ends up negative.');

	IF @TC = 0 COMMIT TRAN;
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

	RETURN ERROR_NUMBER();
END CATCH

