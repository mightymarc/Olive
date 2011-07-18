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
