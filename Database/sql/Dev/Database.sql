/*
Deployment script for Olive
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar TargetEnv "Beta"
:setvar DatabaseName "Olive"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

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

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target OSQL1. The database for which this script was built, Olive, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'OSQL1')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'OSQL1',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]
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
PRINT N'Creating [Bitcoin].[SetAccountReceiveAddress]...';


GO
CREATE PROCEDURE [Bitcoin].[SetAccountReceiveAddress]
	@AccountId INT,
	@ReceiveAddress VARCHAR(34)
AS

BEGIN TRY
	IF @AccountId IS NULL RAISERROR(51003, 16, 1, '@AccountId');
	IF @ReceiveAddress IS NULL RAISERROR(51003, 16, 1, '@ReceiveAddress');

	DECLARE @CurrencyId VARCHAR(10);
	SELECT @CurrencyId = CurrencyId FROM Banking.Account
		WHERE AccountId = @AccountId;

	IF @CurrencyId <> 'EXU' AND @CurrencyId <> 'BTC'
		RAISERROR('Unexpected currency.', 16, 1);

	MERGE INTO Bitcoin.AccountReceiveAddress t
		USING (SELECT @AccountId, @ReceiveAddress) AS s (AccountId, ReceiveAddress)
		ON t.AccountId = s.AccountId
		WHEN MATCHED AND s.ReceiveAddress IS NULL THEN
			DELETE
		WHEN MATCHED THEN
			UPDATE SET ReceiveAddress = s.ReceiveAddress
		WHEN NOT MATCHED THEN
			INSERT (AccountId, ReceiveAddress)
				VALUES (s.AccountId, s.ReceiveAddress);

	RETURN 0;
END TRY
BEGIN CATCH
	RETURN ERROR_NUMBER();
END CATCH

RETURN 0
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
--EXEC sp_addrolemember N'BankServiceRole', N'ServiceUser'
GRANT SELECT ON [Banking].[AccountWithBalance] TO [OLIVE\OliveService]
GRANT SELECT ON [Bitcoin].[Transaction] TO [OLIVE\OliveService]
GRANT SELECT ON [Banking].[AccountUser] TO [OLIVE\OliveService]
GRANT SELECT ON [Banking].[Account] TO [OLIVE\OliveService]
GRANT SELECT ON [Banking].[Transfer] TO [OLIVE\OliveService]
GRANT INSERT ON [dbo].[User] TO [OLIVE\OliveService]
GRANT SELECT ON [dbo].[User] TO [OLIVE\OliveService]
GRANT UPDATE ON [dbo].[User] ([PasswordHash]) TO [OLIVE\OliveService]
GRANT SELECT ON [dbo].[Currency] TO [OLIVE\OliveService]
GRANT EXECUTE ON [Banking].[CreateTransfer] TO [OLIVE\OliveService]
GRANT EXECUTE ON [Banking].[ReleaseAccountHold] TO [OLIVE\OliveService]
GRANT EXECUTE ON [Banking].[CreateCurrentAccount] TO [OLIVE\OliveService]
GRANT EXECUTE ON [Banking].[EditCurrentAccount] TO [OLIVE\OliveService]
GRANT EXECUTE ON [Bitcoin].[CreateTransaction] TO [OLIVE\OliveService]
GRANT EXECUTE ON [Auth].[CreateSession] TO [OLIVE\OliveService];
GRANT EXECUTE ON [Auth].VerifySession TO [OLIVE\OliveService];
GRANT EXECUTE ON [Auth].DeleteSession TO [OLIVE\OliveService];
GRANT EXECUTE ON [Banking].[GetSpecialAccountId] TO [OLIVE\OliveService];
GRANT EXECUTE ON [Bitcoin].[GetLastProcessedTransactionId] TO [OLIVE\OliveService];
GRANT EXECUTE ON [Banking].[CreateAccountHold] TO [OLIVE\OliveService];
GRANT EXECUTE ON [Bitcoin].[GetAccountReceiveAddress] TO [OLIVE\OliveService];
GRANT EXECUTE ON [Bitcoin].[SetAccountReceiveAddress] TO [OLIVE\OliveService];

--GRANT EXECUTE ON [dbo].RethrowError TO [OLIVE\OliveService];

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


--INSERT INTO dbo.[User] (Email,PasswordHash,PasswordSalt) Values ('andreas@opuno.com', 'a', 'b');

--IF '$(TargetEnv)' = 'Dev'
--BEGIN
--    --exec dbo.DestroyEverything
--    --exec dbo.CreateTestData

--    -- Easy test password: "a"
--    --update dbo.[User] set PasswordHash = 'eJsSKba6/GAXbyZ5AhoRSgUkwojQLOYevHxVPrY8UCKZe0e9sH+YL3F7DfaYdsnKHIpGxIsIfCl/KJfZRDA0dg==',
--    --PasswordSalt = 'GIWdAnVlZAIt8REiQArFVWs+nJJb9f+5Br61pxLUpA9H5T0vCq7th2l+TPHl6WOGHqi+7GbxRc0r8tOdMf5Qrg==';
--END

GO
