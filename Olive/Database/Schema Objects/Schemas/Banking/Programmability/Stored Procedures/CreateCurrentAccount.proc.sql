CREATE PROCEDURE Banking.[CreateCurrentAccount]
	@UserId int,
	@CurrencyId varchar(100),
	@AccountId int output
AS

if @UserId is null raiserror('@UserId parameter must not equal null.', 16, 1)
if @CurrencyId is null raiserror('@CurrencyId parameter must not equal null.', 16, 1)
if @AccountId is not null raiserror('@AccountId parameter must equal null.', 16, 1)

BEGIN TRAN
BEGIN TRY
	INSERT INTO Banking.Account (CurrencyId, [Type]) VALUES (@CurrencyId, 'Current');
	SELECT @AccountId = CAST(SCOPE_IDENTITY() as int)

	IF @@ROWCOUNT <> 1 OR @AccountId IS NULL
		RAISERROR('Failed to create account.', 16, 1);

	INSERT INTO Banking.AccountUser (AccountId, UserId, CanDeposit, CanWithdraw)
	VALUES (@AccountId, @UserId, 1, 1);

	IF @@ROWCOUNT <> 1
		RAISERROR('Failed to allow user access to the new account.', 16, 1);

	COMMIT TRAN
	RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	
	exec dbo.RethrowError
END CATCH


