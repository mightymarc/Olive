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

IF '$(TargetEnv)' <> 'Dev'
BEGIN
    CREATE USER [OLIVE\OliveService]
        FOR LOGIN [OLIVE\OliveService]
        WITH DEFAULT_SCHEMA = dbo;

    CREATE LOGIN [OLIVE\OliveService]
        FROM WINDOWS
        WITH DEFAULT_DATABASE=[Olive];

    EXECUTE sp_addsrvrolemember @loginame = N'NT AUTHORITY\SYSTEM', @rolename = N'sysadmin';
    EXECUTE sp_addsrvrolemember @loginame = N'OLIVE\andreasbrekken', @rolename = N'sysadmin';
END
