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
    using Xunit;

    public class OliveContextTests
    {
        [Fact]
        public void AccountsWithBalancesEntitiesCurrencyNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.AccountsWithBalance.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The AccountWithBalance view is empty.");
                }

                Assert.NotNull(target.Currency);
            }
        }

        [Fact]
        public void AccountsWithBalancesEntitiesUsersNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.AccountsWithBalance.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The AccountWithBalance view is empty.");
                }

                Assert.NotNull(target.Users);
            }
        }

        [Fact]
        public void AccountsEntitiesUsersNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The Account table is empty.");
                }

                Assert.NotNull(target.Users);
            }
        }

        [Fact]
        public void AccountsEntitiesCurrencyNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Accounts.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The Account table is empty.");
                }

                Assert.NotNull(target.Currency);
            }
        }

        [Fact]
        public void UsersEntitiesAccountAccessNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Users.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The User table is empty.");
                }

                Assert.NotNull(target.AccountAccess);
            }
        }

        [Fact]
        public void TransfersEntitiesSourceAccountNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Transfers.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The Transfer table is empty.");
                }

                Assert.NotNull(target.SourceAccount);
            }
        }

        [Fact]
        public void TransfersEntitiesDestAccountNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Transfers.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The Transfer table is empty.");
                }

                Assert.NotNull(target.SourceAccount);
            }
        }


        [Fact]
        public void Sessions_EntitiesDestAccountNotNull()
        {
            using (var context = OliveContextFactory.GetDbaContext())
            {
                var target = context.Sessions.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                if (target == null)
                {
                    Assert.True(false, "The Session table is empty.");
                }

                Assert.NotNull(target.User);
            }
        }

        private static readonly Random Random = new Random();

        [Fact]
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
                    Assert.True(false, "Unable to find a source account.");
                }

                var destAccountQuery = from a in context.Accounts
                                       where a.AccountType == AccountType.Current && a.CurrencyId == sourceAccount.CurrencyId &&
                                       a.AccountId != sourceAccount.AccountId
                                       orderby Guid.NewGuid()
                                       select a;

                var destAccount = destAccountQuery.FirstOrDefault();

                if (destAccount == null)
                {
                    Assert.True(false, "Unable to find a destination account.");
                }

                var amount = ((decimal)Random.Next(1, 10000000)) / 100000;
                const string Description = "The transfer description";

                var transferId = context.CreateTransfer(
                    sourceAccount.AccountId, destAccount.AccountId, Description, amount);

                var transfer = context.Transfers.Find(transferId);

                Assert.NotNull(transfer);
                Assert.Equal(amount, transfer.Amount);
                Assert.Equal(Description, transfer.Description);
                Assert.Equal(sourceAccount, transfer.SourceAccount);
                Assert.Equal(destAccount, transfer.DestAccount);
                Assert.Equal(transfer.TransferId, transferId);
                Assert.True((transfer.CreatedAt - DateTime.UtcNow).Duration().TotalMinutes < 5);
            }
        }
    }
}
