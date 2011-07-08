-- Returns
-- 0: Success (deleted)
-- 101: SessionId parameter is null
-- 100: Session not found.
CREATE PROCEDURE Auth.[DeleteSession]
(
	@SessionId uniqueidentifier
)

AS

if @SessionId is null RAISERROR(51003, 16, 1, '@SessionId');

delete from Auth.[ActiveSession] where SessionId = @SessionId;

IF @@ROWCOUNT = 1
	RETURN 0

-- Session not found.
RAISERROR(51011, 16, 1);

return convert(int, 1 - @@rowcount);
