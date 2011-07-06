CREATE PROCEDURE [Banking].[GetOrCreateUserCurrentAccount]
(
	@UserId int,
	@CurrencyId varchar(10),
	@AccountId int output
)

AS

select @AccountId = A.AccountId
from Banking.Account A
inner join Banking.AccountUser AU on A.AccountId = AU.AccountId and AU.UserId = @UserId
where A.CurrencyId = @CurrencyId and A.[Type] = 'Current'

if @AccountId is null
	exec [Banking].[CreateCurrentAccount] @UserId, @CurrencyId, @AccountId output

if @AccountId is null
	raiserror('Failed to create current account.', 16, 1)

return 0
