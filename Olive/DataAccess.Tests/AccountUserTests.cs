// -----------------------------------------------------------------------
// <copyright file="AccountUserTests.cs" company="Microsoft">
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

    public class AccountUserTests
    {
        private static readonly Random Random = new Random();

        private static void AssertIsDefault<T>(T target)
        {
            Assert.Equal(default(T), target);
        }

        [Fact]
        public void EmptyConstructor()
        {
            var target = new AccountUser();
            AssertIsDefault(target.Account);
            AssertIsDefault(target.AccountId);
            AssertIsDefault(target.CanDeposit);
            AssertIsDefault(target.CanWithdraw);
            AssertIsDefault(target.User);
            AssertIsDefault(target.UserId);
        }

        [Fact]
        public void AccountId_GetSet()
        {
            var target = new AccountUser();
            var expected = Random.Next(1, 100000);

            target.AccountId = expected;

            Assert.Equal(expected, target.AccountId);
        }

        [Fact]
        public void UserId_GetSet()
        {
            var target = new AccountUser();
            var expected = Random.Next(1, 100000);

            target.UserId = expected;

            Assert.Equal(expected, target.UserId);
        }
    }
}
