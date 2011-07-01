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
        public void AccountsWithBalance_AccountsReturnedHaveCurrencySet()
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
        public void AccountsWithBalance_AccountsReturnedHaveUsersSet()
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
        public void Accounts_AccountsReturnedHaveUsersSet()
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
        public void Accounts_AccountsReturnedHaveCurrencySet()
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
    }
}
