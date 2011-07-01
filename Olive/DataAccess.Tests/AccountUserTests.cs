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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccountUserTests
    {
        private static readonly Random Random = new Random();

        private static void AssertIsDefault<T>(T target)
        {
            Assert.AreEqual(default(T), target);
        }

        [TestMethod]
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

        [TestMethod]
        public void AccountId_GetSet()
        {
            var target = new AccountUser();
            var expected = Random.Next(1, 100000);

            target.AccountId = expected;

            Assert.AreEqual(expected, target.AccountId);
        }

        [TestMethod]
        public void UserId_GetSet()
        {
            var target = new AccountUser();
            var expected = Random.Next(1, 100000);

            target.UserId = expected;

            Assert.AreEqual(expected, target.UserId);
        }
    }
}
