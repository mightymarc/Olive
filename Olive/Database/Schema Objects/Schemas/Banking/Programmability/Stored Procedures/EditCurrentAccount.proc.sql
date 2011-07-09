CREATE PROCEDURE Banking.[EditCurrentAccount]
	@AccountId int,
	@DisplayName nvarchar(150)
AS

IF @AccountId IS NULL RAISERROR(51003, 16, 1, '@AccountId');

BEGIN TRAN

BEGIN TRY
	UPDATE Banking.Account SET DisplayName = @DisplayName WHERE AccountId = @AccountId;

	IF @@ROWCOUNT = 0
		RAISERROR(51012, 16, 1);

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	
	RETURN ERROR_NUMBER();
END CATCH
