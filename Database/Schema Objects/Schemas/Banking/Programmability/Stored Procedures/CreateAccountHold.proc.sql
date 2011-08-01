CREATE PROCEDURE [Banking].[CreateAccountHold]
    @AccountId INT,
    @Volume DECIMAL(18, 8),
    @Reason NVARCHAR(150),
    @ExpiresAt DATETIME,
    @AccountHoldId INT OUTPUT

AS

DECLARE @TC INT = @@TRANCOUNT;

IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1;

BEGIN TRY
    IF @AccountId IS NULL RAISERROR(51003, 16, 1, '@AccountId');
    IF @Volume IS NULL RAISERROR(51003, 16, 1, '@Volume');
    IF @Reason IS NULL RAISERROR(51003, 16, 1, '@Reason');
    IF @AccountHoldId IS NOT NULL RAISERROR(51004, 16, 1, '@AccountHoldId');

    DECLARE @Available DECIMAL(18, 8)

    SELECT @Available = Available
        FROM Banking.AccountWithBalance 
        WHERE AccountId = @AccountId;

    IF @Available < @Volume
        RAISERROR(51013, 16, 1);

    INSERT INTO Banking.AccountHold (AccountId, Volume, ExpiresAt, Reason)
        VALUES (@AccountId, @Volume, @ExpiresAt, @Reason);

    SELECT @AccountHoldId = CONVERT(INT, SCOPE_IDENTITY());

    IF @TC = 0
        COMMIT TRAN

    RETURN 0;
END TRY
BEGIN CATCH
    IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

    RETURN ERROR_NUMBER();
END CATCH
