CREATE PROCEDURE [dbo].[DestroyEverything]

AS

EXEC Banking.DestroyEverything

DELETE FROM dbo.[User]
DELETE FROM dbo.[Currency]
