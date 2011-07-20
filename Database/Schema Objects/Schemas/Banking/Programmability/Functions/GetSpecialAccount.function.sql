CREATE FUNCTION [Banking].[GetSpecialAccountId]
(
	@Name VARCHAR(150)
)
RETURNS INT
AS
BEGIN
	DECLARE @AccountId INT
	SELECT @AccountId = AccountId
	FROM Banking.SpecialAccount WHERE Name = @Name;

	RETURN @AccountId;
END