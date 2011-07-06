// -----------------------------------------------------------------------
// <copyright file="OliveContextTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    public class OliveContextTests : TestBase
    {
        [Test]
        public void AccountsWithBalancesEntitiesCurrencyNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.AccountsWithBalance.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The AccountWithBalance view is empty.");
                }

                Assert.NotNull(target.Currency);
            }
        }

        [Test]
        public void AccountsEntitiesUsersNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Account table is empty.");
                }

                Assert.NotNull(target.Users);
            }
        }

        [Test]
        public void AccountsEntitiesCurrencyNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Account table is empty.");
                }

                Assert.NotNull(target.Currency);
            }
        }

        [Test]
        public void UsersEntitiesAccountAccessNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.Users.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The User table is empty.");
                }

                Assert.NotNull(target.AccountAccess);
            }
        }

        [Test]
        public void TransfersEntitiesSourceAccountNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.Transfers.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Transfer table is empty.");
                }

                Assert.NotNull(target.SourceAccount);
            }
        }

        [Test]
        public void TransfersEntitiesDestAccountNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.Transfers.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Transfer table is empty.");
                }

                Assert.NotNull(target.SourceAccount);
            }
        }


        [Test]
        public void SessionsEntitiesDestAccountNotNull()
        {
            using (var context = this.GetDbaContext())
            {
                var target = context.Sessions.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Session table is empty.");
                }

                Assert.NotNull(target.User);
            }
        }

        private static readonly Random Random = new Random();

        [Test]
        public void CreateTransfer_ReturnsCorrectTransfer()
        {
            using (var context = this.GetDbaContext())
            {
                var sourceAccountQuery = from a in context.Accounts
                               where a.AccountType == AccountType.IncomingMoneybookersUsd
                               select a;

                var sourceAccount = sourceAccountQuery.FirstOrDefault();

                if (sourceAccount == null)
                {
                    Assert.Inconclusive("Unable to find a source account.");
                }

                var destAccountQuery = from a in context.Accounts
                                       where a.AccountType == AccountType.Current && a.CurrencyId == sourceAccount.CurrencyId &&
                                       a.AccountId != sourceAccount.AccountId
                                       orderby Guid.NewGuid()
                                       select a;

                var destAccount = destAccountQuery.FirstOrDefault();

                if (destAccount == null)
                {
                    Assert.Inconclusive("Unable to find a destination account.");
                }

                var amount = ((decimal)Random.Next(1, 10000000)) / 100000;
                const string Description = "The transfer description";

                var transferId = context.CreateTransfer(
                    sourceAccount.AccountId, destAccount.AccountId, Description, amount);

                var transfer = context.Transfers.Find(transferId);

                Assert.NotNull(transfer);
                Assert.AreEqual(amount, transfer.Amount);
                Assert.AreEqual(Description, transfer.Description);
                Assert.AreEqual(sourceAccount, transfer.SourceAccount);
                Assert.AreEqual(destAccount, transfer.DestAccount);
                Assert.AreEqual(transfer.TransferId, transferId);
                Assert.True((transfer.CreatedAt - DateTime.UtcNow).Duration().TotalMinutes < 5);
            }
        }
    }
}
