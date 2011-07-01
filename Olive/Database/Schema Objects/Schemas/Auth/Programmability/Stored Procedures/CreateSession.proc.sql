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
	raiserror('User account not found.', 16, 1);

if @CorrectPasswordHash <> @PasswordHash
	raiserror('Incorrect password hash.', 16, 1);

select @SessionId = NEWID()

insert into Auth.[Session] (SessionId, UserId) values (@SessionId, @UserId);

if @@ROWCOUNT <> 1
	raiserror('Failed to create session.', 16, 1);

return 0;

