-- Returns
-- 0: Success (deleted)
-- 101: SessionId parameter is null
-- 100: Session not found.
CREATE PROCEDURE Auth.[DeleteSession]
(
	@SessionId uniqueidentifier
)

AS

if @SessionId is null return 101;

delete from Auth.[ActiveSession] where SessionId = @SessionId;

IF @@ROWCOUNT = 1
	RETURN 0

RETURN 100

return convert(int, 1 - @@rowcount);
