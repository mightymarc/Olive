// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockOliveContext.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the MockOliveContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Data;
    using System.Data.Entity;

    using Olive.Services.Tests;

    public class MockOliveContext : IOliveContext, IDisposable
    {
        public MockOliveContext()
        {
            // Set up your collections
            this.Users = new MockDbSet<User> {
                    new User
                        {
                            Email = "andreas@opuno.com", 
                            PasswordHash =
                                "eJsSKba6/GAXbyZ5AhoRSgUkwojQLOYevHxVPrY8UCKZe0e9sH+YL3F7DfaYdsnKHIpGxIsIfCl/KJfZRDA0dg==", 
                            PasswordSalt =
                                "GIWdAnVlZAIt8REiQArFVWs+nJJb9f+5Br61pxLUpA9H5T0vCq7th2l+TPHl6WOGHqi+7GbxRc0r8tOdMf5Qrg=="
                        }
                };
        }

        public IDbSet<Account> Accounts
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<AccountWithBalance> AccountsWithBalance
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Currency> Currencies
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Session> Sessions
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<Transfer> Transfers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<User> Users { get; set; }

        public int CreateCurrentAccount(int userId, string currencyId, string displayName)
        {
            throw new NotImplementedException();
        }

        public Guid CreateSession(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Guid CreateSession(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public long CreateTransfer(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        public long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IDbCommand GetCreateSessionCommand(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public IDbCommand GetCreateTransferCommand(
            int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            throw new NotImplementedException();
        }

        public string GetUserSaltFromEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IDbCommand GetVerifySessionCommand(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
        }

        public IDbSet<T> Set<T>() where T : class
        {
            foreach (var property in typeof(MockOliveContext).GetProperties())
            {
                if (property.PropertyType == typeof(IDbSet<T>))
                {
                    return property.GetValue(this, null) as IDbSet<T>;
                }
            }

            throw new Exception("Type collection not found.");
        }

        public int VerifySession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public void EditCurrentAccount(int accountId, string displayName)
        {
            throw new NotImplementedException();
        }

        public int VerifySession(IDbCommand command)
        {
            throw new NotImplementedException();
        }
    }
}