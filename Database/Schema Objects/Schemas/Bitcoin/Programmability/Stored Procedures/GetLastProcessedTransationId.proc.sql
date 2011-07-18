CREATE PROCEDURE [Bitcoin].[GetLastProcessedTransactionId]
	@TransactionId VARCHAR(64) OUTPUT

AS

SELECT TOP 1 @TransactionId = TransactionId FROM Bitcoin.[Transaction]
	ORDER BY CreatedAt DESC;
 