// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountUserTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the AccountUserTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;

    using NUnit.Framework;

    public class AccountUserTests
    {
        private static readonly Random Random = new Random();

        [Test]
        public void AccountId_GetSet()
        {
            var target = new AccountUser();
            var expected = Random.Next(1, 100000);

            target.AccountId = expected;

            Assert.AreEqual(expected, target.AccountId);
        }

        [Test]
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

        [Test]
        public void UserId_GetSet()
        {
            var target = new AccountUser();
            var expected = Random.Next(1, 100000);

            target.UserId = expected;

            Assert.AreEqual(expected, target.UserId);
        }

        private static void AssertIsDefault<T>(T target)
        {
            Assert.AreEqual(default(T), target);
        }
    }
}