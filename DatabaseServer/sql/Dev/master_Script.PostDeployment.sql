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
