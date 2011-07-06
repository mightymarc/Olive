-- Returns:
-- 0: Success
-- 1: Unknown error
-- 100: Bad user id/password hash.
CREATE PROCEDURE [Auth].[CreateSession]
(
	@UserId int,
	@PasswordHash varchar(100),
	@SessionId varchar(100) output
)

AS

-- Check params
if @UserId is null raiserror('@UserId is null.', 16, 1);
if @PasswordHash is null raiserror('@PasswordHash is null.', 16, 1);
if @SessionId is not null raiserror('@SessionId is not null.', 16, 1);

declare @CorrectPasswordHash varchar(100) = (select PasswordHash from dbo.[User] where UserId = @UserId)

if @CorrectPasswordHash is null
	return 100;


if @CorrectPasswordHash <> @PasswordHash
	return 100;

select @SessionId = NEWID()

insert into Auth.[Session] (SessionId, UserId) values (@SessionId, @UserId);

if @@ROWCOUNT <> 1
	return 1;

return 0;

