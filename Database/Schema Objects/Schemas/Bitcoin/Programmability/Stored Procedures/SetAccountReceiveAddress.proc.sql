CREATE PROCEDURE [Bitcoin].[SetAccountReceiveAddress]
	@AccountId INT,
	@ReceiveAddress VARCHAR(34)
AS

BEGIN TRY
	IF @AccountId IS NULL RAISERROR(51003, 16, 1, '@AccountId');
	IF @ReceiveAddress IS NULL RAISERROR(51003, 16, 1, '@ReceiveAddress');

	DECLARE @CurrencyId VARCHAR(10);
	SELECT @CurrencyId = CurrencyId FROM Banking.Account
		WHERE AccountId = @AccountId;

	IF @CurrencyId <> 'EXU' AND @CurrencyId <> 'BTC'
		RAISERROR('Unexpected currency.', 16, 1);

	MERGE INTO Bitcoin.AccountReceiveAddress t
		USING (SELECT @AccountId, @ReceiveAddress) AS s (AccountId, ReceiveAddress)
		ON t.AccountId = s.AccountId
		WHEN MATCHED AND s.ReceiveAddress IS NULL THEN
			DELETE
		WHEN MATCHED THEN
			UPDATE SET ReceiveAddress = s.ReceiveAddress
		WHEN NOT MATCHED THEN
			INSERT (AccountId, ReceiveAddress)
				VALUES (s.AccountId, s.ReceiveAddress);

	RETURN 0;
END TRY
BEGIN CATCH
	RETURN ERROR_NUMBER();
END CATCH

RETURN 0