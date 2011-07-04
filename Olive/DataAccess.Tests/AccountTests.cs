// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountTests.cs" company="Olive">
//   [Copyright]
// </copyright>
// <summary>
//   Defines the AccountTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Linq;

    using NUnit.Framework;

    using Olive.DataAccess;

    public class AccountTests : TestBase
    {
        private readonly Random random = new Random();

        [Test]
        public void EmptyConstructor()
        {
            var account = new Account();
        }

        [Test]
        public void AccountId_GetSet()
        {
            var account = new Account();

            var accountId = this.random.Next(1, 1000000000);
            account.AccountId = accountId;
            Assert.AreEqual(accountId, account.AccountId);

            accountId = this.random.Next(1, 1000000000);
            account.AccountId = accountId;
            Assert.AreEqual(accountId, account.AccountId);
        }

        [Test]
        public void Users_NotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var account = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (account == null)
                {
                    Assert.Inconclusive("There are no accounts in the store.");
                }

                Assert.NotNull(account.Users);
            }
        }
    }
}
