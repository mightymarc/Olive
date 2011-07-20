-- Returns:
-- 100: @SourceAccountId is null
-- 101: @DestAccountId is null
-- 102: @Amount is null
-- 103: @Description is null
-- 104: @TransferId must be null
-- 105: Source account would get illegal negative balance.
-- 106: Failed to insert transfer.
-- 107: Amount is <= 0.
-- 108: Accounts have different currencies.
CREATE PROCEDURE Banking.CreateTransfer
(
	@SourceAccountId int,
	@DestAccountId int,
	@Description nvarchar(250),
	@Amount decimal(18, 8),
	@TransferId bigint output
)

AS

if @SourceAccountId is null RAISERROR(51003, 16, 1, '@SourceAccountId');
if @DestAccountId is null RAISERROR(51003, 16, 1, '@DestAccountId');
if @Amount is null RAISERROR(51003, 16, 1, '@Amount');
if @Description is null RAISERROR(51003, 16, 1, '@Description');
if @TransferId is not null RAISERROR(51004, 16, 1, '@TransferId');
IF @SourceAccountId = @DestAccountId RAISERROR(51007, 16, 1);
if @Amount <= 0 RAISERROR(51005, 16, 1);

BEGIN TRAN
BEGIN TRY

-- Make sure the source account has enough funds.
DECLARE @SourceAccountBeforeBalance decimal(18, 8)
DECLARE @SourceAccountAllowsNegativeBalance bit
SELECT @SourceAccountBeforeBalance = Available, @SourceAccountAllowsNegativeBalance = AllowNegative FROM Banking.AccountWithBalance
	WHERE AccountId = @SourceAccountId
		
IF @SourceAccountAllowsNegativeBalance = 0 AND @SourceAccountBeforeBalance < @Amount
	RAISERROR(51006, 16, 1);

-- Make sure the accounts have the same currency.
DECLARE @SourceAccountCurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @SourceAccountId);

IF @SourceAccountCurrencyId IS NULL
	RAISERROR(51001, 16, 1);

DECLARE @DestAccountCurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @DestAccountId);

IF @DestAccountCurrencyId IS NULL
	RAISERROR(51002, 16, 1);

IF @SourceAccountCurrencyId <> @DestAccountCurrencyId
	RAISERROR(51008, 16, 1);
	
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
	RAISERROR(51010, 16, 1);

SELECT @TransferId = CONVERT(BIGINT, SCOPE_IDENTITY());

COMMIT TRAN

RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	RETURN ERROR_NUMBER();

	--EXEC dbo.RethrowError
END CATCH
