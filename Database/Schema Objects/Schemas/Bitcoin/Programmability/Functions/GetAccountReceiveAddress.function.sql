CREATE FUNCTION [Bitcoin].[GetAccountReceiveAddress]
(
	@AccountId INT
)
RETURNS VARCHAR(34)
AS
BEGIN
	DECLARE @Result VARCHAR(34)
	SELECT @Result = ReceiveAddress
		FROM Bitcoin.AccountReceiveAddress
		WHERE AccountId = @AccountId;

	RETURN @Result;
END