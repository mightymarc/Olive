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

USE OliveTest

if '$(DatabaseName)' = 'OliveTest'
begin
    exec dbo.DestroyEverything
    exec dbo.CreateTestData
end

SET NOCOUNT ON


INSERT INTO dbo.[User] (Email,PasswordHash,PasswordSalt) Values ('andreas@opuno.com', 'a', 'b');

-- Easy test password: "a"
update dbo.[User] set PasswordHash = 'eJsSKba6/GAXbyZ5AhoRSgUkwojQLOYevHxVPrY8UCKZe0e9sH+YL3F7DfaYdsnKHIpGxIsIfCl/KJfZRDA0dg==',
PasswordSalt = 'GIWdAnVlZAIt8REiQArFVWs+nJJb9f+5Br61pxLUpA9H5T0vCq7th2l+TPHl6WOGHqi+7GbxRc0r8tOdMf5Qrg==';

