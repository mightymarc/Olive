CREATE PROCEDURE [Banking].[ChargeFee]
(
	@UserId INT,
	@SourceAccountId INT,
	@Fee DECIMAL(18, 8),
	@Reason NVARCHAR(200),
	@RandomSeed INT
)

AS

IF '$(TargetEnv)' <> 'Dev' AND @RandomSeed IS NOT NULL RAISERROR('Can not use a random seed outside dev environment.', 16, 1);
IF @UserId IS NULL RAISERROR(51003, 16, 1, '@UserId');
IF @SourceAccountId IS NULL RAISERROR(51003, 16, 1, '@SourceAccountId');
IF @Fee IS NULL RAISERROR(51003, 16, 1, '@Fee');
IF @Reason IS NULL RAISERROR(51003, 16, 1, '@Reason');

DECLARE @TC INT = @@TRANCOUNT, @RC INT;
IF @TC = 0 BEGIN TRAN ELSE SAVE TRAN TR1;

BEGIN TRY
	DECLARE @Candidates TABLE
	(
		UserId INT,
		Depth INT
	);

	WITH UserCTE(UserId, Depth) AS
	(
		SELECT UserId, 0
		FROM [Auth].[User]
		WHERE UserId = @UserId

		UNION ALL

		SELECT U.UserId, P.Depth + 1
		FROM [Auth].[User] U
		JOIN UserCTE P ON U.UserId = ISNULL(P.UserId, 0)
	)
	INSERT INTO @Candidates (UserId, Depth)
	SELECT UserId, Depth
	FROM UserCTE
	WHERE Depth > 0;

	DECLARE @k FLOAT = 2;
	DECLARE @N INT = (SELECT COUNT(UserId) FROM @Candidates);
	DECLARE @WinningNumber FLOAT = CASE WHEN @RandomSeed IS NULL THEN RAND() ELSE RAND(@RandomSeed) END;
	DECLARE @WinningUserId INT;
	DECLARE @p_1 FLOAT = ((@k - 1) * POWER(@k, @N - 1)) / (POWER(@k, @N) - 1);

	-- {1<=n<=N, k>0, p(1) = ((k-1) k^(N-1))/(k^N-1), p(n) = p_1 k^(1-n)}
	SELECT TOP 1
		@WinningUserId = W.UserId
	FROM
	(
		SELECT
			C.UserId,
			@p_1 * POWER(@k, 1 - C.Depth) P
		FROM
			@Candidates C
	) W
	WHERE
		W.P > @WinningNumber
	ORDER BY
		W.P ASC;

	DECLARE @DestAccountId INT;

	IF @WinningUserId = 1 -- Not sure if this will remain static
	BEGIN
		-- This approach came from an answer:
		-- http://stackoverflow.com/questions/6855895/drawing-a-raffle-ticket-winner-in-t-sql

		SELECT TOP 1 @DestAccountId = A1.AccountId
		FROM Banking.AccountWithBalance A1
		WHERE
			A1.[Type] = 'Current' AND
			(
				SELECT SUM(Available)
				FROM AccountWithBalance A2
				WHERE
					A2.AccountId <= A2.AccountId AND
					A2.[Type] = 'Current'
			) > @WinningNumber * (SELECT SUM(Available) from AccountWithBalance)
		ORDER BY
			A1.AccountId;
	END
	ELSE
	BEGIN
		DECLARE @CurrencyId INT = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @SourceAccountId);

		EXEC @RC = Banking.GetOrCreateUserCurrentAccount @WinningUserId, @CurrencyId, '', @DestAccountId OUTPUT;

		IF @RC <> 0 RAISERROR('Failed to get or create current account for winning user. RC %d', 16, 1, @RC);
	END

	DECLARE @TID BIGINT;
	EXEC @RC = Banking.CreateTransfer @SourceAccountId, @DestAccountId, 'Fee', @Fee, @TID OUTPUT;

	IF @TC = 0 COMMIT TRAN;

	RETURN 0;
END TRY
BEGIN CATCH
	IF @TC = 0 ROLLBACK TRAN ELSE ROLLBACK TRAN TR1;

	RETURN ERROR_NUMBER();
END CATCH
