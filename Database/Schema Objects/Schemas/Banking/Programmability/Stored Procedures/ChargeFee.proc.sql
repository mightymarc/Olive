CREATE PROCEDURE [Banking].[ChargeFee]
(
--	@UserId INT,
	@SourceAccountId INT,
	@Fee DECIMAL(18, 8),
	@Reason NVARCHAR(200),
	@RandomSeed INT
)

AS

IF '$(TargetEnv)' <> 'Dev' AND @RandomSeed IS NOT NULL RAISERROR('Can not use a random seed outside dev environment.', 16, 1);
--IF @UserId IS NULL RAISERROR(51003, 16, 1, '@UserId');
IF @SourceAccountId IS NULL RAISERROR(51003, 16, 1, '@SourceAccountId');
IF @Fee IS NULL RAISERROR(51003, 16, 1, '@Fee');
IF @Reason IS NULL RAISERROR(51003, 16, 1, '@Reason');

DECLARE @TC INT = @@TRANCOUNT, @RC INT;
IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1;

BEGIN TRY
	-- TODO: Replace this when account owners are established.
	DECLARE @UserId INT = (SELECT TOP 1 UserId FROM Banking.AccountUser
		WHERE AccountId = @SourceAccountId AND CanWithdraw = 1);

	IF @UserId IS NULL
	BEGIN
		PRINT 'The specified SourceAccountId, ' + CONVERT(VARCHAR, @SourceAccountId) + ', has no User connected to it.';
		RAISERROR('Account owner not found.', 16, 1);
	END

	DECLARE @Candidates TABLE
	(
		UserId INT,
		Depth INT
	);

	WITH UserCTE(UserId, Depth, ParentUserId) AS
	(
		SELECT UserId, 0, ParentUserId
		FROM [Auth].[User]
		WHERE UserId = @UserId

		UNION ALL

		SELECT U.UserId, P.Depth + 1, U.ParentUserId
		FROM [Auth].[User] U
		JOIN UserCTE P ON U.UserId = P.ParentUserId
	)
	INSERT INTO @Candidates (UserId, Depth)
	SELECT UserId, Depth
	FROM UserCTE
	WHERE Depth > 0;

	DECLARE @k FLOAT = dbo.GetSetting('Fee K value');
	DECLARE @N INT = (SELECT COUNT(UserId) FROM @Candidates);
	DECLARE @WinningNumber FLOAT = CASE WHEN @RandomSeed IS NULL THEN RAND() ELSE RAND(@RandomSeed) END;
	DECLARE @WinningUserId INT;
	DECLARE @p_1 FLOAT = ((@k - 1) * POWER(@k, @N - 1)) / (POWER(@k, @N) - 1);

	IF '$(TargetEnv)' = 'Dev'
	BEGIN
		PRINT 'Fee lottery will award the amount ' + CONVERT(VARCHAR, @Fee) + ' from account ' + CONVERT(VARCHAR, @SourceAccountId) +
			', which is owned by user ' + CONVERT(VARCHAR, @UserId) + '. There are ' + CONVERT(VARCHAR, @N) +
			' parent referrers and the winning number is ' + CONVERT(VARCHAR, @WinningNumber) + '.' +
			' p(1) is calculated to ' + CONVERT(VARCHAR, @p_1) + '. (See selected users)';

		SELECT UserId Candidate_UserId, Depth Candidate_Depth FROM @Candidates;

		SELECT
			C.UserId,
			@p_1 * POWER(@k, 1 - C.Depth) P,
			(
				SELECT ISNULL(SUM(@p_1 * POWER(@k, 1 - IC.Depth)), 0) FROM @Candidates IC WHERE IC.Depth > C.Depth
			) RunningSum
		FROM
			@Candidates C
	END

	DECLARE @WinningUserIdP FLOAT;

	-- {1<=n<=N, k>0, p(1) = ((k-1) k^(N-1))/(k^N-1), p(n) = p_1 k^(1-n)}
	SELECT TOP 1
		@WinningUserId = W.UserId,
		@WinningUserIdP = W.P
	FROM
	(
		SELECT
			C.UserId,
			@p_1 * POWER(@k, 1 - C.Depth) P,
			(
				SELECT ISNULL(SUM(@p_1 * POWER(@k, 1 - IC.Depth)), 0) FROM @Candidates IC WHERE IC.Depth > C.Depth
			) RunningSum
		FROM
			@Candidates C
	) W
	WHERE
		W.RunningSum < @WinningNumber AND W.P + W.RunningSum >= @WinningNumber
	ORDER BY
		W.RunningSum DESC;

	IF @WinningUserId IS NULL
	BEGIN
		RAISERROR('Failed to decide on a winning user id.', 16, 1);
	END

	IF '$(TargetEnv)' = 'Dev'
	BEGIN
		PRINT 'The user id of the winner is ' + CONVERT(VARCHAR, @WinningUserId) + '.';
	END

	DECLARE @DestAccountId INT;
	DECLARE @CurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @SourceAccountId);

	DECLARE @BankLotteryP FLOAT;

	IF @WinningUserId = 1 -- Not sure if this will remain static
	BEGIN
		IF '$(TargetEnv)' = 'Dev'
		BEGIN
			PRINT 'The winner is the bank and will lottery the fee based on account available (only current accounts):';

			declare @rnd float
			set @rnd = rand() * (select sum(Available) from Banking.AccountWithBalance where [Type] = 'Current' and CurrencyId = @CurrencyId)

			SELECT AccountId,
			    StartingNumber,
			    EndingNumber,
			    @rnd as DrawNumber,
			    CASE
					WHEN StartingNumber <= @rnd and @rnd < EndingNumber THEN 'Winner'
					ELSE ''
				END Won
			FROM 
			(
				SELECT
					t1.AccountId,
				    ISNULL
					(
						(
							SELECT SUM(t2.Available)
							FROM AccountWithBalance t2
							WHERE
								t2.AccountId < t1.AccountId AND
									t2.[Type] = 'Current' AND
									t2.CurrencyId = @CurrencyId
						), 0
					) StartingNumber,
				    ISNULL
					(
						(
							SELECT SUM(t2.Available)
							FROM AccountWithBalance t2
							WHERE t2.AccountId <= t1.AccountId AND
								t2.[Type] = 'Current' AND
								t2.CurrencyId = @CurrencyId
						), 0
					) EndingNumber
				FROM AccountWithBalance t1
				WHERE
					t1.[Type] = 'Current' AND
					t1.CurrencyId = @CurrencyId
			) RunningSum
			ORDER BY
				AccountId ASC;
		END

		-- This approach came from an answer:
		-- http://stackoverflow.com/questions/6855895/drawing-a-raffle-ticket-winner-in-t-sql

		DECLARE @AllMoney DECIMAL(18, 8) = (SELECT SUM(Available) from AccountWithBalance WHERE [Type] = 'Current' AND CurrencyId = @CurrencyId);

		SELECT TOP 1 @DestAccountId = A1.AccountId, @BankLotteryP = A1.Available / @AllMoney
		FROM Banking.AccountWithBalance A1
		WHERE
			A1.[Type] = 'Current' AND
			A1.CurrencyId = @CurrencyId AND
			(
				SELECT SUM(Available)
				FROM AccountWithBalance A2
				WHERE
					A2.AccountId <= A2.AccountId AND
					A2.[Type] = 'Current' AND
					A2.CurrencyId = @CurrencyId
			) > @WinningNumber * @AllMoney
		ORDER BY
			A1.AccountId;
	END
	ELSE
	BEGIN
		EXEC @RC = Banking.GetOrCreateUserCurrentAccount @WinningUserId, @CurrencyId, '', @DestAccountId OUTPUT;

		IF @RC <> 0 RAISERROR('Failed to get or create current account for winning user. RC %d', 16, 1, @RC);
	END

	IF @SourceAccountId <> @DestAccountId
	BEGIN
		DECLARE @TID BIGINT;
		DECLARE @TransferFromReason NVARCHAR(250) = 'Paid fee for ' + @Reason;
		DECLARE @TransferToReason NVARCHAR(250) = 'Won fee ' + CASE WHEN @WinningUserId = 1 THEN ' from having money in bank (P=' + 
			CONVERT(VARCHAR, @BankLotteryP) + ')' ELSE + ' from having referred users (P=' +  CONVERT(VARCHAR, @WinningUserIdP) + ')' END;

		EXEC @RC = Banking.CreateTransfer @SourceAccountId, @DestAccountId, @TransferFromReason, @TransferToReason, @Fee, @TID OUTPUT;

		IF @RC <> 0
		BEGIN
			IF '$(TargetEnv)' = 'Dev'
			BEGIN
				PRINT 'Was trying to transfer ' + CONVERT(VARCHAR, @Fee) + ' from ' + CONVERT(VARCHAR, @SourceAccountId) +
					' to ' + CONVERT(VARCHAR, @DestAccountId) + '.';
			END

			RAISERROR('Failed to create transaction for fee. RC %d', 16, 1, @RC);
		END
	END
	ELSE
	BEGIN
		PRINT 'The source account won his own transaction, through the bank lottery.'
	END

	IF @TC = 0 COMMIT TRAN;

	RETURN 0;
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

	PRINT ERROR_MESSAGE();

	RETURN ERROR_NUMBER();
END CATCH
