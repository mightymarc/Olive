-- Returns:
-- 0: Success
-- 1: Unknown error
-- 100: Bad email/password hash.
-- 101: E-mail is null
-- 102: PasswordHash is null
-- 103: SessionId not null
CREATE PROCEDURE [Auth].[CreateSession]
(
	@Email VARCHAR(100),
	@PasswordHash varchar(100),
	@SessionId varchar(100) output
)

AS

-- Check params
if @Email is null return 101;
if @PasswordHash is null return 102;
if @SessionId is not null return 103;

declare @UserId int

declare @CorrectPasswordHash varchar(100)

select @CorrectPasswordHash = PasswordHash, @UserId = UserId from dbo.[User] where Email = @Email;

if @CorrectPasswordHash is null or @UserId is null
	return 100;

if @CorrectPasswordHash <> @PasswordHash
	return 100;

select @SessionId = NEWID()

insert into Auth.[Session] (SessionId, UserId) values (@SessionId, @UserId);

if @@ROWCOUNT <> 1
	return 1;

return 0;

