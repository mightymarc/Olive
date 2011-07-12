-- Returns:
-- 0: Success
-- 1: Unknown error
-- 100: User not found.
-- 101: E-mail is null
-- 102: PasswordHash is null
-- 103: SessionId not null
-- 104: Hash is wrong.
CREATE PROCEDURE [Auth].[CreateSession]
(
	@Email VARCHAR(100),
	@PasswordHash varchar(100),
	@SessionId varchar(100) output
)

AS

BEGIN TRY

-- Check params
if @Email is null BEGIN RAISERROR(51003, 16, 1, '@Email'); END;
if @PasswordHash IS NULL BEGIN RAISERROR(51003, 16, 1, '@PasswordHash'); END;
if @SessionId is not null BEGIN RAISERROR(51004, 16, 1, '@SessionId'); END;

declare @UserId int

declare @CorrectPasswordHash varchar(100)

select @CorrectPasswordHash = PasswordHash, @UserId = UserId from dbo.[User] where Email = @Email;

if @CorrectPasswordHash is null or @UserId is null
begin
	RAISERROR(51009, 16, 1);
	RETURN;
END

IF @CorrectPasswordHash <> @PasswordHash
BEGIN
	RAISERROR(51009, 16, 1);
	RETURN;
END

select @SessionId = NEWID()

insert into Auth.[Session] (SessionId, UserId) values (@SessionId, @UserId);

if @@ROWCOUNT <> 1
BEGIN
	RAISERROR(51010, 16, 1);
	RETURN;
END

END TRY
BEGIN CATCH
	RETURN ERROR_NUMBER();
END CATCH

RETURN 0;

