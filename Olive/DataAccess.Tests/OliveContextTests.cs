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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OliveContextTests
    {
        [TestMethod]
        public void AccountsWithBalancesEntitiesCurrencyNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.AccountsWithBalance.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The AccountWithBalance view is empty.");
                }

                Assert.IsNotNull(target.Currency);
            }
        }

        [TestMethod]
        public void AccountsWithBalancesEntitiesUsersNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.AccountsWithBalance.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The AccountWithBalance view is empty.");
                }

                Assert.IsNotNull(target.Users);
            }
        }

        [TestMethod]
        public void AccountsEntitiesUsersNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Account table is empty.");
                }

                Assert.IsNotNull(target.Users);
            }
        }

        [TestMethod]
        public void AccountsEntitiesCurrencyNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Account table is empty.");
                }

                Assert.IsNotNull(target.Currency);
            }
        }

        [TestMethod]
        public void UsersEntitiesAccountAccessNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Users.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The User table is empty.");
                }

                Assert.IsNotNull(target.AccountAccess);
            }
        }

        [TestMethod]
        public void TransfersEntitiesSourceAccountNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Transfers.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Transfer table is empty.");
                }

                Assert.IsNotNull(target.SourceAccount);
            }
        }

        [TestMethod]
        public void TransfersEntitiesDestAccountNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Transfers.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Transfer table is empty.");
                }

                Assert.IsNotNull(target.SourceAccount);
            }
        }


        [TestMethod]
        public void Sessions_EntitiesDestAccountNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Sessions.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.Inconclusive("The Session table is empty.");
                }

                Assert.IsNotNull(target.User);
            }
        }

        private static readonly Random Random = new Random();

        [TestMethod]
        public void CreateTransfer_ReturnsCorrectTransfer()
        {
            using (var context = OliveContextFactory.GetDbaContext())
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

                Assert.IsNotNull(transfer);
                Assert.AreEqual(amount, transfer.Amount);
                Assert.AreEqual(Description, transfer.Description);
                Assert.AreEqual(sourceAccount, transfer.SourceAccount);
                Assert.AreEqual(destAccount, transfer.DestAccount);
                Assert.AreEqual(transfer.TransferId, transferId);
                Assert.IsTrue((transfer.CreatedAt - DateTime.UtcNow).Duration().TotalMinutes < 5);
            }
        }
    }
}
