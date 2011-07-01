CREATE PROCEDURE [Banking].[DestroyEverything]

AS

DELETE FROM Banking.[Transfer]
DELETE FROM Banking.[AccountUser]
DELETE FROM Banking.[Account]
