EXECUTE sp_addsrvrolemember @loginame = N'NT AUTHORITY\SYSTEM', @rolename = N'sysadmin';


GO
EXECUTE sp_addsrvrolemember @loginame = N'OLIVE\andreasbrekken', @rolename = N'sysadmin';

