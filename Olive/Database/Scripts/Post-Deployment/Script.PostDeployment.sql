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
GRANT EXECUTE ON [Auth].[CreateSession] TO [BankServiceRole];
GRANT EXECUTE ON [Auth].VerifySession TO [BankServiceRole];


if '$(DatabaseName)' = 'OliveTest'
begin
    exec dbo.DestroyEverything
    exec dbo.CreateTestData
end

INSERT INTO dbo.[User] (Email,PasswordHash,PasswordSalt) Values ('andreas@opuno.com', 'a', 'b');

-- Easy test password: "a"
update dbo.[User] set PasswordHash = 'eJsSKba6/GAXbyZ5AhoRSgUkwojQLOYevHxVPrY8UCKZe0e9sH+YL3F7DfaYdsnKHIpGxIsIfCl/KJfZRDA0dg==',
PasswordSalt = 'GIWdAnVlZAIt8REiQArFVWs+nJJb9f+5Br61pxLUpA9H5T0vCq7th2l+TPHl6WOGHqi+7GbxRc0r8tOdMf5Qrg==';

