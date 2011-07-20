// -----------------------------------------------------------------------
// <copyright file="RealClientTests.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Bitcoin.Tests
{
    using System.Linq;
    using System.Net;

    using NUnit.Framework;

    using Olive.Bitcoin;

    [TestFixture]
    public class RealClientTests
    {
        private RpcClient rpcClient;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            // Make sure that the rpcClient is running on the test network.
            this.rpcClient = new RpcClient { Hostname = "127.0.0.1", PortNumber = 8502, Credential = new NetworkCredential("user", "password") };

            /*if (!this.rpcClient.GetIsOnTestNetwork())
            {
                Assert.Inconclusive("Integration tests must run on the test network.");
            }*/
        }

        [Test]
        public void GetAccountsTest()
        {
            var accounts = this.rpcClient.GetAcccounts();

            Assert.IsNotNull(accounts.SingleOrDefault(x => x.Name == string.Empty));
        }

        [Test]
        public void GetTransactionsTest()
        {
            var transactions = this.rpcClient.GetTransactions();

            Assert.IsNotNull(transactions.FirstOrDefault());
        }
    }
}
