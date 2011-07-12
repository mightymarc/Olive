CREATE PROCEDURE [Bitcoin].[CreateTransaction]
	@TransactionId VARCHAR(64),
	@AccountId INT,
	@AccountHoldId INT,
	@Amount DECIMAL(18, 8)

AS

BEGIN TRY
	BEGIN TRAN

	INSERT INTO Bitcoin.[Transaction] (TransactionId, AccountId, AccountHoldId, Amount)
		VALUES (@TransactionId, @AccountId, @AccountHoldId, @Amount);

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	RETURN ERROR_NUMBER();
END CATCH
