﻿namespace Olive.DataAccess.Tests
{
    using System;
    using System.Linq;

    using Olive.DataAccess;

    using Xunit;

    public class AccountTests
    {
        private readonly Random random = new Random();

        [Fact]
        public void EmptyConstructor()
        {
            var account = new Account();
        }

        [Fact]
        public void AccountId_GetSet()
        {
            var account = new Account();

            var accountId = this.random.Next(1, 1000000000);
            account.AccountId = accountId;
            Assert.Equal(accountId, account.AccountId);

            accountId = this.random.Next(1, 1000000000);
            account.AccountId = accountId;
            Assert.Equal(accountId, account.AccountId);
        }

        [Fact]
        public void Users_NotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var account = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (account == null)
                {
                    Assert.True(false, "There are no accounts in the store.");
                }

                Assert.NotNull(account.Users);
            }
        }
    }
}
