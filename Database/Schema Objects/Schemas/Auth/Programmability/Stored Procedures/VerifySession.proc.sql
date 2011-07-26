CREATE PROCEDURE [Auth].[VerifySession]
(
	@SessionId UNIQUEIDENTIFIER,
	@UserId INT OUTPUT
)

AS

IF @UserId IS NOT NULL RAISERROR(51003, 16, 1, '@UserId');
IF @SessionId IS NULL RAISERROR(51003, 16, 1, '@SessionId');

SELECT @UserId = UserId 
FROM Auth.[ActiveSession]
WHERE SessionId = @SessionId;

IF @UserId IS NULL
BEGIN
	-- The specified session does not exist or is expired.
	RETURN 51011;
END

RETURN 0;

