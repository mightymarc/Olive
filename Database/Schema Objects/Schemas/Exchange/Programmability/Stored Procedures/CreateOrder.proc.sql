CREATE PROCEDURE [Exchange].[CreateOrder]
(
	@SourceAccountId INT,
	@DestAccountId INT,
	@Price DECIMAL(18, 4),
	@Volume DECIMAL(18, 4),
	@OrderId INT OUTPUT
)

AS

DECLARE @TC INT = @@TRANCOUNT;

IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1

BEGIN TRY
	-- Create account hold.
	PRINT 'Creating account hold for order.'
	DECLARE @HoldReason VARCHAR(200)
	SELECT @HoldReason = 'Hold for exchange order'

	DECLARE @AccountHoldId INT

	DECLARE @RC INT = 0
	EXEC @RC = Banking.CreateAccountHold @SourceAccountId, @Volume, @HoldReason, NULL, @AccountHoldId OUTPUT;

	IF @RC <> 0 OR @AccountHoldId IS NULL
	BEGIN
		DECLARE @ErrorMessage VARCHAR(250);
		SELECT @ErrorMessage = 'Failed to create account hold ' + CONVERT(VARCHAR, @RC);
		RAISERROR(@ErrorMessage, 16, 1);
		--RETURN;
	END

	DECLARE @MarketId INT;
	SELECT
		@MarketId = MarketId
	FROM 
		Exchange.Market M
	INNER JOIN
		Banking.Account SA ON SA.CurrencyId = M.FromCurrencyId
	INNER JOIN
		Banking.Account DA ON DA.CurrencyId = M.ToCurrencyId
	WHERE 
		SA.AccountId = @SourceAccountId AND
		DA.AccountId = @DestAccountId;

	IF @MarketId IS NULL
	BEGIN
		RAISERROR('Unable to find the market for the order.', 16, 1);
		RETURN;
	END

	PRINT 'Inserting new order.';

	INSERT INTO Exchange.[Order] (SourceAccountId, DestAccountId, Volume, Price, AccountHoldId)
		VALUES (@SourceAccountId, @DestAccountId, @Volume, @Price, @AccountHoldId);

	SELECT @OrderId = CONVERT(INT, SCOPE_IDENTITY());

	IF @OrderId IS NULL
	BEGIN
		PRINT 'Failed to insert order.';
		RAISERROR('Failed to insert order', 16, 1);
	END

	IF @TC = 0 COMMIT TRAN;

	RETURN 0;
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

	SELECT 'Error: ' + ERROR_MESSAGE();

	RETURN ERROR_NUMBER();
END CATCH
