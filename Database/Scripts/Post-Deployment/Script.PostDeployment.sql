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

IF '$(TargetEnv)' <> 'Dev'
BEGIN
	CREATE USER [OLIVE\OliveService] FOR LOGIN [OLIVE\OliveService]
	EXEC sp_addrolemember N'ClientService', N'OLIVE\OliveService'
END

IF '$(TargetEnv)' = 'Dev'
BEGIN
	CREATE USER [ServiceUser] FOR LOGIN [ServiceUser];
	EXEC sp_addrolemember N'ClientService', N'ServiceUser';

	INSERT INTO dbo.Currency (CurrencyId) VALUES ('EXU');
	INSERT INTO dbo.Currency (CurrencyId) VALUES ('CAP');
	INSERT INTO dbo.Currency (CurrencyId) VALUES ('BTT');

	-- Settings
	INSERT INTO dbo.[Setting] (SettingId, Value)
	SELECT 'Exchange match fee ratio', '0.01'
	UNION ALL
	SELECT 'Fee K value', '2';

	INSERT INTO [Auth].[User] (Email, PasswordHash, PasswordSalt, ParentUserId)
		VALUES ('bank@exu.me', 'hash', 'salt', NULL);
	DECLARE @BankUserId INT = CONVERT(INT, SCOPE_IDENTITY());

	INSERT INTO [Auth].[User] (Email, PasswordHash, PasswordSalt, ParentUserId)
		VALUES ('andreas@opuno.com', 'gV01I0aXqNLdcX21eiZIF8GYqax5l8fbMvZ8GpNkNUk/LK+iksEwGwRJgfy1BKrXF44SIdF4EQSqXF0Xqs9yaQ==', 'hlOjDseGhycET+M7N85tESzM8tGjzp1x/p/ZflyXsVt7ksJ+mvQ2x+3jD8s6UscjdOYdCH2vReKy5ghK2noYIw==', 1);
	DECLARE @TestUserId INT = CONVERT(INT, SCOPE_IDENTITY());

	DECLARE @ID INT;

	-- EXU faucet
	INSERT INTO Banking.Account (CurrencyId, [Type], AllowNegative) VALUES ('EXU', 'Special', 1);
	DECLARE @ExuFaucetId INT = CONVERT(INT, SCOPE_IDENTITY());
	INSERT INTO Banking.SpecialAccount (AccountId, Name) VALUES (@ExuFaucetId, 'Free EXU');

	-- CAP faucet
	INSERT INTO Banking.Account (CurrencyId, [Type], AllowNegative) VALUES ('CAP', 'Special', 1);
	DECLARE @CapFaucetId INT = CONVERT(INT, SCOPE_IDENTITY());
	INSERT INTO Banking.SpecialAccount (AccountId, Name) VALUES (@CapFaucetId, 'Free CAP');

	DECLARE @TestUserExuAccountId INT;
	EXEC Banking.CreateCurrentAccount @TestUserId, 'EXU', 'Primary', @TestUserExuAccountId OUTPUT;

	DECLARE @TestUserCapAccountId INT;
	EXEC Banking.CreateCurrentAccount @TestUserId, 'CAP', 'Primary', @TestUserCapAccountId OUTPUT;

	DECLARE @TestUserExuAccount2Id INT;
	EXEC Banking.CreateCurrentAccount @TestUserId, 'EXU', 'Primary', @TestUserExuAccount2Id OUTPUT;

	DECLARE @TestUserCapAccount2Id INT;
	EXEC Banking.CreateCurrentAccount @TestUserId, 'CAP', 'Primary', @TestUserCapAccount2Id OUTPUT;

	-- Give test user some EXU money.
	DECLARE @TID BIGINT = NULL;
	EXEC Banking.CreateTransfer @ExuFaucetId, @TestUserExuAccountId, 'From faucet', 'From faucet', 100, @TID OUTPUT;

	-- Give test user some CAP money.
	SELECT @TID = NULL;
	EXEC Banking.CreateTransfer @CapFaucetId, @TestUserCapAccount2Id, 'From faucet', 'From faucet', 100, @TID OUTPUT;

	-- Markets
	INSERT INTO Exchange.Market (FromCurrencyId, ToCurrencyId)
		VALUES ('CAP', 'EXU');
	INSERT INTO Exchange.Market (FromCurrencyId, ToCurrencyId)
		VALUES ('EXU', 'CAP');

	INSERT INTO Exchange.Market (FromCurrencyId, ToCurrencyId)
		VALUES ('BTT', 'EXU');
	INSERT INTO Exchange.Market (FromCurrencyId, ToCurrencyId)
		VALUES ('EXU', 'BTT');

	INSERT INTO Exchange.Market (FromCurrencyId, ToCurrencyId)
		VALUES ('CAP', 'BTT');
	INSERT INTO Exchange.Market (FromCurrencyId, ToCurrencyId)
		VALUES ('BTT', 'CAP');
END

-- Errors
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
EXEC master..sp_addmessage 51015, 16, N'Cannot increase volume of order #%d.', @replace = 'replace';
EXEC master..sp_addmessage 51016, 16, N'The specified order does not exist. (#%d).', @replace = 'replace';
EXEC master..sp_addmessage 51017, 16, N'Invalid operation: %s', @replace = 'replace';
EXEC master..sp_addmessage 51018, 16, N'Failed to update: %s.', @replace = 'replace';
EXEC master..sp_addmessage 51019, 16, N'Failed to delete: %s.', @replace = 'replace';

SET NOCOUNT ON
