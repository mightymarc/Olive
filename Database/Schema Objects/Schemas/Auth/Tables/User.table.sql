CREATE TABLE [Auth].[User] (
    [UserId] INT IDENTITY (1, 1) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
    EmailLowercase AS LOWER(Email),
    [PasswordHash] VARCHAR (100) NOT NULL,
    [PasswordSalt] VARCHAR (100) NOT NULL,
);

