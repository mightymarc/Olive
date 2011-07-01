CREATE PROCEDURE Auth.[DeleteSession]
(
	@SessionId uniqueidentifier
)

AS

if @SessionId is null raiserror('@SessionId is null.', 16, 1);

delete from Auth.[Session] where SessionId = @SessionId;

return convert(int, 1 - @@rowcount);
