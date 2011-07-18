CREATE PROCEDURE [Banking].[DestroyEverything]

AS

IF '$(TargetEnv)' <> 'Dev' AND '$(TargetEnv)' <> 'Beta'
BEGIN
	RAISERROR('Cannot destroy Banking-schema data unless target environment is Dev or Beta.', 16, 1);
	RETURN
END

DELETE FROM Banking.[Transfer]
DELETE FROM Banking.[AccountUser]
DELETE FROM Banking.[Account]
