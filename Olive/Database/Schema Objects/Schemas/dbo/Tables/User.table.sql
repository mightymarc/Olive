CREATE TABLE [dbo].[User] (
    [UserId]       INT           IDENTITY (1, 1) NOT NULL,
    [PasswordHash] VARCHAR (100) NOT NULL,
    [PasswordSalt] VARCHAR (100) NOT NULL,
);

