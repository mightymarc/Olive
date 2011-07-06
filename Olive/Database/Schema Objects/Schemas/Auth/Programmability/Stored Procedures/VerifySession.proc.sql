CREATE PROCEDURE [Auth].[VerifySession]
(
	@SessionId UNIQUEIDENTIFIER,
	@UserId INT OUTPUT
)

AS

IF @UserId IS NOT NULL RAISERROR('@UserId must be null.', 16, 1);
IF @SessionId IS NULL RAISERROR('@SessionId is null.', 16, 1);

SELECT @UserId = UserId 
FROM Auth.[ActiveSession]
WHERE SessionId = @SessionId;

IF @UserId IS NULL
BEGIN
	-- The specified session does not exist or is expired.
	RETURN 100;
END

RETURN 0;

