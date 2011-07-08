CREATE PROCEDURE [dbo].[CreateTestData]

AS

insert into dbo.Currency (CurrencyId) values ('USD')

insert into dbo.Currency (CurrencyId) values ('BTC')
insert into dbo.Currency (CurrencyId) values ('NOK')
insert into dbo.Currency (CurrencyId) values ('GBP')

insert into Banking.[Account] ([Type], CurrencyId, AllowNegative, DisplayName)
	values ('IncomingMoneybookersUSD', 'USD', 1, 'Incoming Moneybookers (USD)')
	
declare @UserCount int = round((100 - 50) * rand() + 50, 0)
print 'Creating ' + cast(@UserCount as nvarchar) + ' users.'

declare @UserN int = 0

while @UserN < @UserCount 
begin
	select @UserN += 1

	insert into dbo.[User] (PasswordHash, PasswordSalt, Email) values (left(cast(newid() as varchar(100)),20), left(cast(newid() as varchar(100)),20),
		left(cast(newid() as varchar(100)),5) + '@' + left(cast(newid() as varchar(100)),5)
	
	)
	
	declare @UserId int = null
	select @UserId = cast(@@IDENTITY as int) -- Must use @@IDENTITY because of the trigger (which doesn't set SCOPE_IDENTITY()).
	declare @CurrentAccountCount int = round((5 - 1 - 1) * rand() + 1, 0) 
	declare @CurrentAccountN int = 0
	
	declare @TransactionCount int = round((100 - 0) * rand() + 0, 0)
	declare @TransactionN int = 0
	
	while @TransactionN < @TransactionCount
	begin
		select @TransactionN += 1
		
		if rand() < 0.2
		begin
			-- Deposit money
			declare @SourceAccountId int = (select AccountId from Banking.Account
				where [Type] = 'IncomingMoneybookersUSD')
				
			declare @DestAccountId int = null
			exec [Banking].[GetOrCreateUserCurrentAccount] @UserId, 'USD', null, @DestAccountId output
			
			declare @Amount decimal(18, 8) = round(rand() * 100000, 8)
			declare @TransferId bigint = null
			
			exec Banking.CreateTransfer @SourceAccountId, @DestAccountId, 'Deposit with Moneybookers',
				@Amount, @TransferId output
		end
	end
end

