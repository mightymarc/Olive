-- Returns:
-- 100: @FromAccountId is null
-- 101: @ToAccountId is null
-- 102: @Volume is null
-- 103: @Description is null
-- 104: @TransferId must be null
-- 105: Source account would get illegal negative balance.
-- 106: Failed to insert transfer.
-- 107: Amount is <= 0.
-- 108: Accounts have different currencies.
CREATE PROCEDURE Banking.CreateTransfer
(
	@FromAccountId int,
	@ToAccountId int,
	@FromComment nvarchar(250),
	@ToComment nvarchar(250),
	@Volume decimal(18, 8),
	@TransferId bigint output
)

AS

if @FromAccountId is null RAISERROR(51003, 16, 1, '@FromAccountId');
if @ToAccountId is null RAISERROR(51003, 16, 1, '@ToAccountId');
if @Volume is null RAISERROR(51003, 16, 1, '@Volume');
if @FromComment is null RAISERROR(51003, 16, 1, '@FromComment');
if @ToComment is null RAISERROR(51003, 16, 1, '@ToComment');
if @TransferId is not null RAISERROR(51004, 16, 1, '@TransferId');
IF @FromAccountId = @ToAccountId RAISERROR(51007, 16, 1);
if @Volume <= 0 RAISERROR(51005, 16, 1);

DECLARE @TC INT = @@TRANCOUNT;

IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1

BEGIN TRY

-- Make sure the source account has enough funds.
DECLARE @FromAccountBeforeBalance decimal(18, 8)
DECLARE @FromAccountAllowsNegativeBalance bit
SELECT @FromAccountBeforeBalance = Available, @FromAccountAllowsNegativeBalance = AllowNegative FROM Banking.AccountWithBalance
	WHERE AccountId = @FromAccountId

IF @FromAccountAllowsNegativeBalance = 0 AND @FromAccountBeforeBalance < @Volume
BEGIN
	IF '$(TargetEnv)' = 'Dev'
	BEGIN
		PRINT '@FromAccountAllowsNegativeBalance = 0 (FALSE)';
		PRINT '@FromAccountBeforeBalance = ' + CONVERT(NVARCHAR, @FromAccountBeforeBalance);
		PRINT '@Volume = ' + CONVERT(NVARCHAR, @Volume);

		SELECT * FROM Banking.[AccountWithBalance];
	END

	RAISERROR(51006, 16, 1);
END

-- Make sure the accounts have the same currency.
DECLARE @FromAccountCurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @FromAccountId);

IF @FromAccountCurrencyId IS NULL
	RAISERROR(51001, 16, 1);

DECLARE @ToAccountCurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @ToAccountId);

IF @ToAccountCurrencyId IS NULL
	RAISERROR(51002, 16, 1);

IF @FromAccountCurrencyId <> @ToAccountCurrencyId
	RAISERROR(51008, 16, 1);

IF '$(TargetEnv)' = 'Dev'
BEGIN
	PRINT 'Creating transfer of ' + CONVERT(VARCHAR, @Volume) + ' ' + @FromAccountCurrencyId +
		' from account ' + CONVERT(VARCHAR, @FromAccountId) + ' (' + @FromComment +
		') to ' + CONVERT(VARCHAR, @ToAccountId) + ' (' + @ToComment + ')';
END
	
INSERT INTO Banking.[Transfer]
(
	FromAccountId, 
	ToAccountId, 
	Volume,
	[FromComment],
	[ToComment]
) VALUES (
	@FromAccountId,
	@ToAccountId,
	@Volume,
	@FromComment,
	@ToComment
);
	
IF @@ROWCOUNT <> 1
	RAISERROR(51010, 16, 1);

SELECT @TransferId = CONVERT(BIGINT, SCOPE_IDENTITY());

IF @TC = 0 COMMIT TRAN

RETURN 0
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1

	IF '$(TargetEnv)' = 'Dev'
		SELECT ERROR_MESSAGE();

	RETURN ERROR_NUMBER();
END CATCH
