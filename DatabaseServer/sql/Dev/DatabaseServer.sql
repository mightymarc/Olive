/*
Deployment script for master
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar TargetEnvironment "Dev"
:setvar DatabaseName "master"
:setvar DefaultDataPath "c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "c:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
USE [master]
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
USE [$(DatabaseName)]
GO
PRINT N'Creating [OLIVE\andreasbrekken]...';


GO
CREATE LOGIN [OLIVE\andreasbrekken]
    FROM WINDOWS WITH DEFAULT_DATABASE = [master], DEFAULT_LANGUAGE = [us_english];


GO
PRINT N'Creating <unnamed>...';


GO
EXECUTE sp_addsrvrolemember @loginame = N'OLIVE\andreasbrekken', @rolename = N'sysadmin';


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

IF '$(DatabaseName)' = 'OliveTest'
BEGIN
	-- Test data
	INSERT INTO dbo.Currency (ShortName) VALUES ('BTC')

	INSERT INTO dbo.Currency (ShortName) VALUES ('USD')

	INSERT INTO dbo.Currency (ShortName) VALUES ('NOK')
	INSERT INTO dbo.Currency (ShortName) VALUES ('GBP')
	INSERT INTO dbo.Currency (ShortName) VALUES ('EUR')

	INSERT INTO Banking.Account (DisplayName, AllowNegative, CurrencyId, [Type])
	SELECT 'Incoming MoneyBookers (' + c.ShortName + ')', 1, c.CurrencyId, 'IncomingMoneybookersUSD'
	FROM dbo.Currency C
	WHERE C.ShortName = 'USD';
END;

GO
