﻿/*
Deployment script for Olive
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar TargetEnv "Dev"
:setvar DatabaseName "Olive"
:setvar DefaultDataPath "c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL)
	BEGIN
		DECLARE @rc      int,                       -- return code
				@fn      nvarchar(4000),            -- file name to back up to
				@dir     nvarchar(4000)             -- backup directory

		EXEC @rc = [master].[dbo].[xp_instance_regread] N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'BackupDirectory', @dir output, 'no_output'

		IF (@dir IS NULL)
		BEGIN 
			EXEC @rc = [master].[dbo].[xp_instance_regread] N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'DefaultData', @dir output, 'no_output'
		END

		IF (@dir IS NULL)
		BEGIN
			EXEC @rc = [master].[dbo].[xp_instance_regread] N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\Setup', N'SQLDataRoot', @dir output, 'no_output'
			SELECT @dir = @dir + N'\Backup'
		END

		SELECT  @fn = @dir + N'\' + N'$(DatabaseName)' + N'-' + 
				CONVERT(nchar(8), GETDATE(), 112) + N'-' + 
				RIGHT(N'0' + RTRIM(CONVERT(nchar(2), DATEPART(hh, GETDATE()))), 2) + 
				RIGHT(N'0' + RTRIM(CONVERT(nchar(2), DATEPART(mi, getdate()))), 2) + 
				RIGHT(N'0' + RTRIM(CONVERT(nchar(2), DATEPART(ss, getdate()))), 2) + 
				N'.bak' 
				BACKUP DATABASE [$(DatabaseName)] TO DISK = @fn
	END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [OliveDes], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', FILEGROWTH = 1024 KB)
    LOG ON (NAME = [OliveDes_log], FILENAME = '$(DefaultLogPath)$(DatabaseName)_log.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %) COLLATE Danish_Norwegian_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]
GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


GO
PRINT N'Dropping Permission...';


GO
REVOKE CONNECT TO [dbo] CASCADE
    AS [dbo];


GO
PRINT N'Creating [ServiceUser]...';


GO
CREATE USER [ServiceUser] FOR LOGIN [ServiceUser];


GO
PRINT N'Creating [BankServiceRole]...';


GO
CREATE ROLE [BankServiceRole]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [Auth]...';


GO
CREATE SCHEMA [Auth]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [Banking]...';


GO
CREATE SCHEMA [Banking]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [Bitcoin]...';


GO
CREATE SCHEMA [Bitcoin]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating [Auth].[Session]...';


GO
CREATE TABLE [Auth].[Session] (
    [SessionId] UNIQUEIDENTIFIER NOT NULL,
    [ExpiresAt] DATETIME         NOT NULL,
    [CreatedAt] DATETIME         NOT NULL,
    [UserId]    INT              NOT NULL
);


GO
PRINT N'Creating PK_Session...';


GO
ALTER TABLE [Auth].[Session]
    ADD CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED ([SessionId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [Banking].[Account]...';


GO
CREATE TABLE [Banking].[Account] (
    [AccountId]     INT            IDENTITY (1000, 1) NOT NULL,
    [DisplayName]   NVARCHAR (150) NULL,
    [CurrencyId]    VARCHAR (10)   NOT NULL,
    [Type]          VARCHAR (50)   NOT NULL,
    [AllowNegative] BIT            NOT NULL
);


GO
PRINT N'Creating PK_Account...';


GO
ALTER TABLE [Banking].[Account]
    ADD CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [Banking].[AccountHold]...';


GO
CREATE TABLE [Banking].[AccountHold] (
    [AccountHoldId] INT             IDENTITY (1, 1) NOT NULL,
    [AccountId]     INT             NOT NULL,
    [Amount]        DECIMAL (18, 8) NULL,
    [CreatedAt]     DATETIME        NOT NULL,
    [ExpiresAt]     DATETIME        NULL,
    [Reason]        NVARCHAR (150)  NOT NULL
);


GO
PRINT N'Creating PK_AccountHold...';


GO
ALTER TABLE [Banking].[AccountHold]
    ADD CONSTRAINT [PK_AccountHold] PRIMARY KEY CLUSTERED ([AccountHoldId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [Banking].[AccountUser]...';


GO
CREATE TABLE [Banking].[AccountUser] (
    [AccountId]   INT NOT NULL,
    [UserId]      INT NOT NULL,
    [CanDeposit]  BIT NOT NULL,
    [CanWithdraw] BIT NOT NULL
);


GO
PRINT N'Creating PK_AccountUser...';


GO
ALTER TABLE [Banking].[AccountUser]
    ADD CONSTRAINT [PK_AccountUser] PRIMARY KEY CLUSTERED ([AccountId] ASC, [UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [Banking].[Transfer]...';


GO
CREATE TABLE [Banking].[Transfer] (
    [TransferId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [SourceAccountId] INT             NOT NULL,
    [DestAccountId]   INT             NOT NULL,
    [Amount]          DECIMAL (18, 8) NOT NULL,
    [CreatedAt]       DATETIME        NOT NULL,
    [Description]     NVARCHAR (250)  NOT NULL
);


GO
PRINT N'Creating PK_Transfer...';


GO
ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [PK_Transfer] PRIMARY KEY CLUSTERED ([TransferId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [Bitcoin].[Transaction]...';


GO
CREATE TABLE [Bitcoin].[Transaction] (
    [TransactionId] VARCHAR (64)    NOT NULL,
    [CreatedAt]     DATETIME        NOT NULL,
    [AccountId]     INT             NOT NULL,
    [AccountHoldId] INT             NULL,
    [Amount]        DECIMAL (18, 8) NOT NULL
);


GO
PRINT N'Creating PK_Transaction...';


GO
ALTER TABLE [Bitcoin].[Transaction]
    ADD CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[Currency]...';


GO
CREATE TABLE [dbo].[Currency] (
    [CurrencyId] VARCHAR (10) NOT NULL
);


GO
PRINT N'Creating PK_Currency...';


GO
ALTER TABLE [dbo].[Currency]
    ADD CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([CurrencyId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [UserId]         INT           IDENTITY (1, 1) NOT NULL,
    [Email]          VARCHAR (100) NOT NULL,
    [EmailLowercase] AS            LOWER(Email),
    [PasswordHash]   VARCHAR (100) NOT NULL,
    [PasswordSalt]   VARCHAR (100) NOT NULL
);


GO
PRINT N'Creating PK_User...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating On column: SessionId...';


GO
ALTER TABLE [Auth].[Session]
    ADD DEFAULT (newid()) FOR [SessionId];


GO
PRINT N'Creating On column: ExpiresAt...';


GO
ALTER TABLE [Auth].[Session]
    ADD DEFAULT (dateadd(hour, 1, getutcdate())) FOR [ExpiresAt];


GO
PRINT N'Creating On column: CreatedAt...';


GO
ALTER TABLE [Auth].[Session]
    ADD DEFAULT (getutcdate()) FOR [CreatedAt];


GO
PRINT N'Creating On column: AllowNegative...';


GO
ALTER TABLE [Banking].[Account]
    ADD DEFAULT (0) FOR [AllowNegative];


GO
PRINT N'Creating On column: CreatedAt...';


GO
ALTER TABLE [Banking].[AccountHold]
    ADD DEFAULT (GETUTCDATE()) FOR [CreatedAt];


GO
PRINT N'Creating DF_Transfer_CreatedAt...';


GO
ALTER TABLE [Banking].[Transfer]
    ADD CONSTRAINT [DF_Transfer_CreatedAt] DEFAULT (getutcdate()) FOR [CreatedAt];


GO
PRINT N'Creating On column: CreatedAt...';


GO
ALTER TABLE [Bitcoin].[Transaction]
    ADD DEFAULT (GETUTCDATE()) FOR [CreatedAt];


GO
PRINT N'Creating FK_Session_User...';


GO
ALTER TABLE [Auth].[Session] WITH NOCHECK
    ADD CONSTRAINT [FK_Session_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Account_Currency...';


GO
ALTER TABLE [Banking].[Account] WITH NOCHECK
    ADD CONSTRAINT [FK_Account_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([CurrencyId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_AccountHold_Account...';


GO
ALTER TABLE [Banking].[AccountHold] WITH NOCHECK
    ADD CONSTRAINT [FK_AccountHold_Account] FOREIGN KEY ([AccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_AccountUser_Account...';


GO
ALTER TABLE [Banking].[AccountUser] WITH NOCHECK
    ADD CONSTRAINT [FK_AccountUser_Account] FOREIGN KEY ([AccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_AccountUser_User...';


GO
ALTER TABLE [Banking].[AccountUser] WITH NOCHECK
    ADD CONSTRAINT [FK_AccountUser_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Transfer_DestAccount...';


GO
ALTER TABLE [Banking].[Transfer] WITH NOCHECK
    ADD CONSTRAINT [FK_Transfer_DestAccount] FOREIGN KEY ([DestAccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Transfer_SourceAccount...';


GO
ALTER TABLE [Banking].[Transfer] WITH NOCHECK
    ADD CONSTRAINT [FK_Transfer_SourceAccount] FOREIGN KEY ([SourceAccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Transaction_Account...';


GO
ALTER TABLE [Bitcoin].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Transaction_Account] FOREIGN KEY ([AccountId]) REFERENCES [Banking].[Account] ([AccountId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Transaction_AccountHold...';


GO
ALTER TABLE [Bitcoin].[Transaction] WITH NOCHECK
    ADD CONSTRAINT [FK_Transaction_AccountHold] FOREIGN KEY ([AccountHoldId]) REFERENCES [Banking].[AccountHold] ([AccountHoldId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating CK_Account_Type...';


GO
ALTER TABLE [Banking].[Account] WITH NOCHECK
    ADD CONSTRAINT [CK_Account_Type] CHECK ([Type]='Current' OR [Type]='IncomingMoneybookersUSD');


GO
PRINT N'Creating CK_Transfer_Amount...';


GO
ALTER TABLE [Banking].[Transfer] WITH NOCHECK
    ADD CONSTRAINT [CK_Transfer_Amount] CHECK ([Amount]>(0));


GO
PRINT N'Creating CK_Transfer_DifferentAccounts...';


GO
ALTER TABLE [Banking].[Transfer] WITH NOCHECK
    ADD CONSTRAINT [CK_Transfer_DifferentAccounts] CHECK ([SourceAccountId]<>[DestAccountId]);


GO
PRINT N'Creating [Auth].[CreateSession]...';


GO
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
GO
PRINT N'Creating [Banking].[DestroyEverything]...';


GO
CREATE PROCEDURE [Banking].[DestroyEverything]

AS

IF '$(TargetEnv)' <> 'Dev' AND '$(TargetEnv)' <> 'Beta'
BEGIN
	RAISERROR('Cannot destroy Banking-schema data unless target environment is Dev or Beta.', 16, 1);
	RETURN
END

DELETE FROM Banking.[Transfer]
DELETE FROM Banking.[AccountUser]
DELETE FROM Banking.[Account]
GO
PRINT N'Creating [Banking].[EditCurrentAccount]...';


GO
CREATE PROCEDURE Banking.[EditCurrentAccount]
	@AccountId int,
	@DisplayName nvarchar(150)
AS

IF @AccountId IS NULL RAISERROR(51003, 16, 1, '@AccountId');

BEGIN TRAN

BEGIN TRY
	UPDATE Banking.Account SET DisplayName = @DisplayName WHERE AccountId = @AccountId;

	IF @@ROWCOUNT = 0
		RAISERROR(51012, 16, 1);

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN;
	
	RETURN ERROR_NUMBER();
END CATCH
GO
PRINT N'Creating [Banking].[ReleaseAccountHold]...';


GO
CREATE PROCEDURE [Banking].[ReleaseAccountHold]
	@AccountHoldId INT

AS

BEGIN TRY
	BEGIN TRAN

	DELETE FROM Banking.AccountHold WHERE AccountHoldId = @AccountHoldId;

	IF @@ROWCOUNT = 0
		RAISERROR(51014, 16, 1);

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN

	RETURN ERROR_NUMBER()
END CATCH
GO
PRINT N'Creating [Bitcoin].[CreateTransaction]...';


GO
CREATE PROCEDURE [Bitcoin].[CreateTransaction]
	@TransactionId VARCHAR(64),
	@AccountId INT,
	@AccountHoldId INT,
	@Amount DECIMAL(18, 8)

AS

BEGIN TRY
	BEGIN TRAN

	INSERT INTO Bitcoin.[Transaction] (TransactionId, AccountId, AccountHoldId, Amount)
		VALUES (@TransactionId, @AccountId, @AccountHoldId, @Amount);

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	RETURN ERROR_NUMBER();
END CATCH
GO
PRINT N'Creating [Bitcoin].[GetLastProcessedTransactionId]...';


GO
CREATE PROCEDURE [Bitcoin].[GetLastProcessedTransactionId]
	@TransactionId VARCHAR(64) OUTPUT

AS

SELECT TOP 1 @TransactionId = TransactionId FROM Bitcoin.[Transaction]
	ORDER BY CreatedAt DESC;
GO
PRINT N'Creating [dbo].[DestroyEverything]...';


GO
CREATE PROCEDURE [dbo].[DestroyEverything]

AS

IF '$(TargetEnv)' <> 'Dev' AND '$(TargetEnv)' <> 'Beta'
BEGIN
	RAISERROR('Cannot destroy data unless target environment is Dev or Beta.', 16, 1);
	RETURN
END

EXEC Banking.DestroyEverything

DELETE FROM dbo.[User]
DELETE FROM dbo.[Currency]
GO
PRINT N'Creating [dbo].[RethrowError]...';


GO
CREATE PROCEDURE dbo.RethrowError AS
        IF ERROR_NUMBER() IS NULL
                RETURN;

        DECLARE 
                @ErrorMessage    NVARCHAR(4000),
                @ErrorNumber     INT,
                @ErrorSeverity   INT,
                @ErrorState      INT,
                @ErrorLine       INT,
                @ErrorProcedure  NVARCHAR(200);

        SELECT 
                @ErrorNumber = ERROR_NUMBER(),
                @ErrorSeverity = ERROR_SEVERITY(),
                @ErrorState = ERROR_STATE(),
                @ErrorLine = ERROR_LINE(),
                @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

        SELECT @ErrorMessage = 
                N'Error %d, Level %d, State %d, Procedure %s, Line %d, ' + 
                        'Message: '+ ERROR_MESSAGE();

        RAISERROR 
                (
                @ErrorMessage, 
                @ErrorSeverity, 
                @ErrorState,               
                @ErrorNumber,    -- parameter: original error number.
                @ErrorSeverity,  -- parameter: original error severity.
                @ErrorState,     -- parameter: original error state.
                @ErrorProcedure, -- parameter: original error procedure name.
                @ErrorLine       -- parameter: original error line number.
                );
GO
PRINT N'Creating [Banking].[CreateCurrentAccount]...';


GO
CREATE PROCEDURE Banking.[CreateCurrentAccount]
	@UserId int,
	@CurrencyId varchar(100),
	@DisplayName nvarchar(150),
	@AccountId int output
AS

IF @UserId IS NULL RAISERROR(51003, 16, 1, '@UserId');
IF @CurrencyId IS NULL RAISERROR(51003, 16, 1, '@CurrencyId');
IF @AccountId IS NOT NULL RAISERROR(51004, 16, 1, '@AccountId');

BEGIN TRAN

BEGIN TRY
	INSERT INTO Banking.Account (CurrencyId, [Type], [DisplayName]) VALUES (@CurrencyId, 'Current', @DisplayName);
	SELECT @AccountId = CAST(SCOPE_IDENTITY() as int)

	IF @@ROWCOUNT <> 1 OR @AccountId IS NULL
		RAISERROR(51010, 16, 1);

	INSERT INTO Banking.AccountUser (AccountId, UserId, CanDeposit, CanWithdraw)
	VALUES (@AccountId, @UserId, 1, 1);

	IF @@ROWCOUNT <> 1
		RAISERROR(51010, 16, 1);

	COMMIT TRAN
	RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	
	exec dbo.RethrowError
END CATCH
GO
PRINT N'Creating [Banking].[GetOrCreateUserCurrentAccount]...';


GO
CREATE PROCEDURE [Banking].[GetOrCreateUserCurrentAccount]
(
	@UserId int,
	@CurrencyId varchar(10),
	@DisplayName NVARCHAR(150),
	@AccountId int output
)

AS

select @AccountId = A.AccountId
from Banking.Account A
inner join Banking.AccountUser AU on A.AccountId = AU.AccountId and AU.UserId = @UserId
where A.CurrencyId = @CurrencyId and A.[Type] = 'Current'

if @AccountId is null
	exec [Banking].[CreateCurrentAccount] @UserId, @CurrencyId, @DisplayName, @AccountId output

if @AccountId is null
	raiserror('Failed to create current account.', 16, 1)

return 0
GO
PRINT N'Creating [dbo].[CreateTestData]...';


GO
CREATE PROCEDURE [dbo].[CreateTestData]

AS

PRINT 'Creating test data...'

IF '$(TargetEnv)' <> 'Dev' AND '$(TargetEnv)' <> 'Beta'
BEGIN
	RAISERROR('Cannot create test data unless target environment is Dev or Beta.', 16, 1);
	RETURN
END

insert into dbo.Currency (CurrencyId) values ('USD')

insert into dbo.Currency (CurrencyId) values ('BTC')
insert into dbo.Currency (CurrencyId) values ('NOK')
insert into dbo.Currency (CurrencyId) values ('GBP')

insert into Banking.[Account] ([Type], CurrencyId, AllowNegative, DisplayName)
	values ('IncomingMoneybookersUSD', 'USD', 1, 'Incoming Moneybookers (USD)')
	
declare @UserCount int = round((100 - 50) * rand() + 50, 0)
print 'Creating ' + cast(@UserCount as nvarchar) + ' users.'

declare @UserN int = 0

while @UserN < @UserCount 
begin
	select @UserN += 1

	insert into dbo.[User] (PasswordHash, PasswordSalt, Email) values (left(cast(newid() as varchar(100)),20), left(cast(newid() as varchar(100)),20),
		left(cast(newid() as varchar(100)),5) + '@' + left(cast(newid() as varchar(100)),5)
	
	)
	
	declare @UserId int = null
	select @UserId = cast(@@IDENTITY as int) -- Must use @@IDENTITY because of the trigger (which doesn't set SCOPE_IDENTITY()).
	declare @CurrentAccountCount int = round((5 - 1 - 1) * rand() + 1, 0) 
	declare @CurrentAccountN int = 0
	
	declare @TransactionCount int = round((100 - 0) * rand() + 0, 0)
	declare @TransactionN int = 0
	
	while @TransactionN < @TransactionCount
	begin
		select @TransactionN += 1
		
		if rand() < 0.2
		begin
			-- Deposit money
			declare @SourceAccountId int = (select AccountId from Banking.Account
				where [Type] = 'IncomingMoneybookersUSD')
				
			declare @DestAccountId int = null
			exec [Banking].[GetOrCreateUserCurrentAccount] @UserId, 'USD', null, @DestAccountId output
			
			declare @Amount decimal(18, 8) = round(rand() * 100000, 8)
			declare @TransferId bigint = null
			
			exec Banking.CreateTransfer @SourceAccountId, @DestAccountId, 'Deposit with Moneybookers',
				@Amount, @TransferId output
		end
	end
end
GO
PRINT N'Creating [Auth].[ActiveSession]...';


GO
CREATE VIEW [Auth].[ActiveSession]
	WITH SCHEMABINDING
	AS SELECT [SessionId], [ExpiresAt], [UserId] FROM [Auth].[Session] WHERE ExpiresAt > GETUTCDATE();
GO
PRINT N'Creating [Banking].[AccountHoldView]...';


GO
CREATE VIEW [Banking].[AccountHoldView]
	WITH SCHEMABINDING
	AS SELECT AccountHoldId, AccountId, Amount, ExpiresAt, Reason
	FROM Banking.AccountHold
	WHERE ExpiresAt > GETUTCDATE();
GO
PRINT N'Creating [Banking].[AccountWithBalance]...';


GO
CREATE VIEW Banking.AccountWithBalance
WITH SCHEMABINDING
AS

SELECT
	AccountId,
	DisplayName,
	CurrencyId,
	[Type],
	AllowNegative,
		(SELECT ISNULL(SUM(TD.Amount), 0) FROM Banking.[Transfer] TD WHERE TD.DestAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(TS.Amount), 0) FROM Banking.[Transfer] TS WHERE TS.SourceAccountId = A.AccountId) -
		(SELECT ISNULL(SUM(Amount), 0) FROM Banking.AccountHoldView AHV WHERE AHV.AccountId = A.AccountId)
	Available
FROM
	Banking.[Account] A;
GO
PRINT N'Creating [Auth].[DeleteSession]...';


GO
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
GO
PRINT N'Creating [Auth].[VerifySession]...';


GO
CREATE PROCEDURE [Auth].[VerifySession]
(
	@SessionId UNIQUEIDENTIFIER,
	@UserId INT OUTPUT
)

AS

IF @UserId IS NOT NULL RAISERROR(51003, 16, 1, '@UserId');
IF @SessionId IS NULL RAISERROR(51003, 16, 1, '@SessionId');

SELECT @UserId = UserId 
FROM Auth.[ActiveSession]
WHERE SessionId = @SessionId;

IF @UserId IS NULL
BEGIN
	-- The specified session does not exist or is expired.
	RAISERROR(51011, 16, 1);
END

RETURN 0;
GO
PRINT N'Creating [Banking].[CreateAccountHold]...';


GO
CREATE PROCEDURE [Banking].[CreateAccountHold]
	@AccountId INT,
	@Amount DECIMAL(18, 8),
	@Reason NVARCHAR(150),
	@ExpiresAt DATETIME,
	@AccountHoldId INT OUTPUT

AS

BEGIN TRY
	IF @AccountId IS NULL RAISERROR(51003, 16, 1, '@AccountId');
	IF @Amount IS NULL RAISERROR(51003, 16, 1, '@Amount');
	IF @Reason IS NULL RAISERROR(51003, 16, 1, '@Reason');
	IF @AccountHoldId IS NOT NULL RAISERROR(51004, 16, 1, '@AccountHoldId');

	BEGIN TRAN

	DECLARE @Available DECIMAL(18, 8)
	SELECT @Available = Available FROM Banking.AccountWithBalance WHERE AccountId = @AccountId;

	IF @Available < @Amount
		RAISERROR(51013, 16, 1);

	INSERT INTO Banking.AccountHold (AccountId, Amount, ExpiresAt, Reason)
		VALUES (@AccountId, @Amount, @ExpiresAt, @Reason);

	SELECT @AccountHoldId = CONVERT(INT, SCOPE_IDENTITY());

	COMMIT TRAN

	RETURN 0
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN

	RETURN ERROR_NUMBER();
END CATCH
GO
PRINT N'Creating [Banking].[CreateTransfer]...';


GO
-- Returns:
-- 100: @SourceAccountId is null
-- 101: @DestAccountId is null
-- 102: @Amount is null
-- 103: @Description is null
-- 104: @TransferId must be null
-- 105: Source account would get illegal negative balance.
-- 106: Failed to insert transfer.
-- 107: Amount is <= 0.
-- 108: Accounts have different currencies.
CREATE PROCEDURE Banking.CreateTransfer
(
	@SourceAccountId int,
	@DestAccountId int,
	@Description nvarchar(250),
	@Amount decimal(18, 8),
	@TransferId bigint output
)

AS

if @SourceAccountId is null RAISERROR(51003, 16, 1, '@SourceAccountId');
if @DestAccountId is null RAISERROR(51003, 16, 1, '@DestAccountId');
if @Amount is null RAISERROR(51003, 16, 1, '@Amount');
if @Description is null RAISERROR(51003, 16, 1, '@Description');
if @TransferId is not null RAISERROR(51004, 16, 1, '@TransferId');
IF @SourceAccountId = @DestAccountId RAISERROR(51007, 16, 1);
if @Amount <= 0 RAISERROR(51005, 16, 1);

BEGIN TRAN
BEGIN TRY

-- Make sure the source account has enough funds.
DECLARE @SourceAccountBeforeBalance decimal(18, 8)
DECLARE @SourceAccountAllowsNegativeBalance bit
SELECT @SourceAccountBeforeBalance = Available, @SourceAccountAllowsNegativeBalance = AllowNegative FROM Banking.AccountWithBalance
	WHERE AccountId = @SourceAccountId
		
IF @SourceAccountAllowsNegativeBalance = 0 AND @SourceAccountBeforeBalance < @Amount
	RAISERROR(51006, 16, 1);

-- Make sure the accounts have the same currency.
DECLARE @SourceAccountCurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @SourceAccountId);

IF @SourceAccountCurrencyId IS NULL
	RAISERROR(51001, 16, 1);

DECLARE @DestAccountCurrencyId VARCHAR(10) = (SELECT CurrencyId FROM Banking.Account WHERE AccountId = @DestAccountId);

IF @DestAccountCurrencyId IS NULL
	RAISERROR(51002, 16, 1);

IF @SourceAccountCurrencyId <> @DestAccountCurrencyId
	RAISERROR(51008, 16, 1);
	
INSERT INTO Banking.[Transfer]
(
	SourceAccountId, 
	DestAccountId, 
	Amount,
	[Description]
) VALUES (
	@SourceAccountId,
	@DestAccountId,
	@Amount,
	@Description
)
	
IF @@ROWCOUNT <> 1
	RAISERROR(51010, 16, 1);

SELECT @TransferId = CONVERT(BIGINT, SCOPE_IDENTITY());

COMMIT TRAN

RETURN 0
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	EXEC dbo.RethrowError
END CATCH
GO
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- Security
EXEC sp_addrolemember N'BankServiceRole', N'ServiceUser'
GRANT SELECT ON [Banking].[AccountWithBalance] TO [BankServiceRole]
GRANT SELECT ON [Banking].[AccountUser] TO [BankServiceRole]
GRANT SELECT ON [Banking].[Account] TO [BankServiceRole]
GRANT SELECT ON [Banking].[Transfer] TO [BankServiceRole]
GRANT INSERT ON [dbo].[User] TO [BankServiceRole]
GRANT SELECT ON [dbo].[User] TO [BankServiceRole]
GRANT UPDATE ON [dbo].[User] ([PasswordHash]) TO [BankServiceRole]
GRANT SELECT ON [dbo].[Currency] TO [BankServiceRole]
GRANT EXECUTE ON [Banking].[CreateTransfer] TO [BankServiceRole]
GRANT EXECUTE ON [Banking].[CreateCurrentAccount] TO [BankServiceRole]
GRANT EXECUTE ON [Banking].[EditCurrentAccount] TO [BankServiceRole]
GRANT EXECUTE ON [Auth].[CreateSession] TO [BankServiceRole];
GRANT EXECUTE ON [Auth].VerifySession TO [BankServiceRole];
GRANT EXECUTE ON [Auth].DeleteSession TO [BankServiceRole];
--GRANT EXECUTE ON [dbo].RethrowError TO [BankServiceRole];

-- Errors
--USE master
EXEC master..sp_addmessage 51001, 16, N'The specified source account does not exist.', @replace = 'replace';
EXEC master..sp_addmessage 51002, 16, N'The specified destination account does not exist.', @replace = 'replace';
EXEC master..sp_addmessage 51003, 16, N'The parameter %s must not be null.', @replace = 'replace';
EXEC master..sp_addmessage 51004, 16, N'The parameter %s must be null.', @replace = 'replace';
EXEC master..sp_addmessage 51005, 16, N'Transfer amount must be > 0.', @replace = 'replace';
EXEC master..sp_addmessage 51006, 16, N'Transfer may not be set balance below zero when source account AllowNegative is false.', @replace = 'replace';
EXEC master..sp_addmessage 51007, 16, N'Source and destination account may not be the same for a transfer.', @replace = 'replace';
EXEC master..sp_addmessage 51008, 16, N'Source and destination account must have the same currency.', @replace = 'replace';
EXEC master..sp_addmessage 51009, 16, N'Authentication failed.', @replace = 'replace';
EXEC master..sp_addmessage 51010, 16, N'Failed to insert.', @replace = 'replace';
EXEC master..sp_addmessage 51011, 16, N'The specified session does not exist.', @replace = 'replace';
EXEC master..sp_addmessage 51012, 16, N'The specified account does not exist.', @replace = 'replace';
EXEC master..sp_addmessage 51013, 16, N'The specified account does not have the necessary available balance to create the hold.', @replace = 'replace';
EXEC master..sp_addmessage 51014, 16, N'The specified account hold does not exist.', @replace = 'replace';

USE Olive;

SET NOCOUNT ON


INSERT INTO dbo.[User] (Email,PasswordHash,PasswordSalt) Values ('andreas@opuno.com', 'a', 'b');

IF '$(TargetEnv)' = 'Dev'
BEGIN
    --exec dbo.DestroyEverything
    exec dbo.CreateTestData

    -- Easy test password: "a"
    update dbo.[User] set PasswordHash = 'eJsSKba6/GAXbyZ5AhoRSgUkwojQLOYevHxVPrY8UCKZe0e9sH+YL3F7DfaYdsnKHIpGxIsIfCl/KJfZRDA0dg==',
    PasswordSalt = 'GIWdAnVlZAIt8REiQArFVWs+nJJb9f+5Br61pxLUpA9H5T0vCq7th2l+TPHl6WOGHqi+7GbxRc0r8tOdMf5Qrg==';
END

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [Auth].[Session] WITH CHECK CHECK CONSTRAINT [FK_Session_User];

ALTER TABLE [Banking].[Account] WITH CHECK CHECK CONSTRAINT [FK_Account_Currency];

ALTER TABLE [Banking].[AccountHold] WITH CHECK CHECK CONSTRAINT [FK_AccountHold_Account];

ALTER TABLE [Banking].[AccountUser] WITH CHECK CHECK CONSTRAINT [FK_AccountUser_Account];

ALTER TABLE [Banking].[AccountUser] WITH CHECK CHECK CONSTRAINT [FK_AccountUser_User];

ALTER TABLE [Banking].[Transfer] WITH CHECK CHECK CONSTRAINT [FK_Transfer_DestAccount];

ALTER TABLE [Banking].[Transfer] WITH CHECK CHECK CONSTRAINT [FK_Transfer_SourceAccount];

ALTER TABLE [Bitcoin].[Transaction] WITH CHECK CHECK CONSTRAINT [FK_Transaction_Account];

ALTER TABLE [Bitcoin].[Transaction] WITH CHECK CHECK CONSTRAINT [FK_Transaction_AccountHold];

ALTER TABLE [Banking].[Account] WITH CHECK CHECK CONSTRAINT [CK_Account_Type];

ALTER TABLE [Banking].[Transfer] WITH CHECK CHECK CONSTRAINT [CK_Transfer_Amount];

ALTER TABLE [Banking].[Transfer] WITH CHECK CHECK CONSTRAINT [CK_Transfer_DifferentAccounts];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
