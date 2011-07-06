// -----------------------------------------------------------------------
// <copyright file="MockOliveContext.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Text;


    public partial class MockOliveContext : IOliveContext, IDisposable
    {
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

        public Guid CreateSession(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public string GetUserSaltFromEmail(string email)
        {
            throw new NotImplementedException();
        }

        public long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            throw new NotImplementedException();
        }

        public int VerifySession(Guid sessionId)
        {
            throw new NotImplementedException();
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

        public void SaveChanges()
        {
        }

        public MockOliveContext()
        {
            // Set up your collections
            this.Users = new MockDbSet<User>
            {
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
