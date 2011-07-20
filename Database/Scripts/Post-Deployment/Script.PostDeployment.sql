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
    CREATE USER [OLIVE\OliveService]
    FOR LOGIN [OLIVE\OliveService];

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
END

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
