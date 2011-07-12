CREATE TABLE [Auth].[Session]
(
	SessionId uniqueidentifier not null default(newid()),
	ExpiresAt datetime default(dateadd(hour, 1, getutcdate())) not null,
	CreatedAt datetime default(getutcdate()) not null,
	UserId int not null
);
