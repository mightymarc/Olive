CREATE PROCEDURE Banking.CreateTransfer
(
	@SourceAccountId int,
	@DestAccountId int,
	@Description nvarchar(250),
	@Amount decimal(18, 8),
	@TransferId bigint output
)

AS

if @SourceAccountId is null raiserror('@SourceAccountId parameter must not be null.', 16, 1)
if @DestAccountId is null raiserror('@DestAccountId parameter must not be null.', 16, 1)
if @Amount is null raiserror('@Amount parameter must not be null.', 16, 1)
if @Description is null raiserror('@Description parameter must not be null.', 16, 1)
if @TransferId is not null raiserror('@TransferId parameter must be null', 16, 1)

BEGIN TRAN
BEGIN TRY
	-- Make sure the source account has enough funds.
	DECLARE @SourceAccountBeforeBalance decimal(18, 8)
	DECLARE @SourceAccountAllowsNegativeBalance bit
	SELECT @SourceAccountBeforeBalance = [Balance], @SourceAccountAllowsNegativeBalance = AllowNegative FROM Banking.AccountWithBalance
		WHERE AccountId = @SourceAccountId
		
	IF @SourceAccountAllowsNegativeBalance = 0 AND @SourceAccountBeforeBalance < @Amount
	BEGIN
		raiserror('Cannot set the account to a negative balance.', 16, 1)
	END
	
	INSERT INTO Banking.[Transfer]
	(
		SourceAccountId, 
		DestAccountId, 
		Amount,
		[Description]
	) VALUES (
		@SourceAccountId,
		@DestAccountId,
		@Amount,
		@Description
	)
	
	IF @@ROWCOUNT <> 1
	BEGIN
		ROLLBACK TRAN
		raiserror('Failed to insert transfer row.', 16, 1)
	END

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	exec dbo.RethrowError
END CATCH

		