// -----------------------------------------------------------------------
// <copyright file="CryptoTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using Olive;

    [TestFixture]
    public class CryptoTests
    {
        [Test]
        public void CreateSaltHasCorrectLength()
        {
            Assert.Inconclusive("Is this predictable?");
        }

        [Test]
        public void CreateSaltDoesNotRepeat()
        {
            var crypto = new Crypto();

            var salt1 = crypto.CreateSalt();
            var salt2 = crypto.CreateSalt();

            Assert.AreNotEqual(salt1, salt2);
        }

        [Test]
        public void GenerateHashMatchesPrecalculated()
        {
            var crypto = new Crypto();

            Assert.AreEqual("+mohhbPgqahe9B/7Z+88H7b3SYD46/lw5OcuNT7ZU31ZMIPCAd/W5D4cinqsK8jbsRnH37fUuPExEROVvXDpfw==", crypto.GenerateHash("password", "salt"));
        }
    }
}
