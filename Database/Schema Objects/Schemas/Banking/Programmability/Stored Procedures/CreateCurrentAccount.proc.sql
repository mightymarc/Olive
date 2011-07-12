CREATE PROCEDURE Banking.[CreateCurrentAccount]
	@UserId int,
	@CurrencyId varchar(100),
	@DisplayName nvarchar(150),
	@AccountId int output
AS

IF @UserId IS NULL RAISERROR(51003, 16, 1, '@UserId');
IF @CurrencyId IS NULL RAISERROR(51003, 16, 1, '@CurrencyId');
IF @AccountId IS NOT NULL RAISERROR(51004, 16, 1, '@AccountId');

BEGIN TRAN

BEGIN TRY
	INSERT INTO Banking.Account (CurrencyId, [Type], [DisplayName]) VALUES (@CurrencyId, 'Current', @DisplayName);
	SELECT @AccountId = CAST(SCOPE_IDENTITY() as int)

	IF @@ROWCOUNT <> 1 OR @AccountId IS NULL
		RAISERROR(51010, 16, 1);

	INSERT INTO Banking.AccountUser (AccountId, UserId, CanDeposit, CanWithdraw)
	VALUES (@AccountId, @UserId, 1, 1);

	IF @@ROWCOUNT <> 1
		RAISERROR(51010, 16, 1);

	COMMIT TRAN
	RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	
	exec dbo.RethrowError
END CATCH
