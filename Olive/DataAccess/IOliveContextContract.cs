namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IOliveContext))]
    public abstract class IOliveContextContract : IOliveContext
    {
        public void Dispose()
        {
        }

        public IDbSet<Account> Accounts
        {
            get
            {
                return default(IDbSet<Account>);
            }
            set
            {
                return;
            }
        }

        public IDbSet<AccountWithBalance> AccountsWithBalance
        {
            get
            {
                return default(IDbSet<AccountWithBalance>);
            }
            set
            {
                return;
            }
        }

        public IDbSet<Currency> Currencies
        {
            get
            {
                return default(IDbSet<Currency>);
            }
            set
            {
                return;
            }
        }

        public IDbSet<Session> Sessions
        {
            get
            {

                return default(IDbSet<Session>);
            }
            set
            {
                return;
            }
        }

        public IDbSet<Transfer> Transfers
        {
            get
            {
                return default(IDbSet<Transfer>);
            }
            set
            {
                return;
            }
        }

        public IDbSet<User> Users
        {
            get
            {
                return default(IDbSet<User>);
            }
            set
            {
                return;
            }
        }

        public Guid CreateSession(string email, string passwordHash)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(passwordHash), "passwordHash");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(email), "email");
            return Guid.Empty;
        }

        public int CreateCurrentAccount(int userId, string currencyId, string displayName)
        {
            Contract.Requires<ArgumentException>(userId < 0, "userId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(currencyId), "currencyId");
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        public long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            Contract.Requires<ArgumentException>(sourceAccountId > 0, "sourceAccount");
            Contract.Requires<ArgumentException>(destAccountId > 0, "destAccountId");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(description), "description");
            Contract.Requires<ArgumentException>(amount > 0, "amount");
            Contract.Ensures(Contract.Result<long>() > 0);

            return default(long);
        }

        public int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            return default(int);
        }

        public void SaveChanges()
        {
            return;
        }
    }
}