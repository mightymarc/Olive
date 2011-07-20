// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncomingTransactionProcessorTests.cs" company="Microsoft">
//   Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    
//    1. Definitions
//    
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    
//    A "contribution" is the original software, or any additions or changes to the software.
//    
//    A "contributor" is any person that distributes its contribution under this license.
//    
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    
//    2. Grant of Rights
//    
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    
//    3. Conditions and Limitations
//    
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BitcoinSync.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Text;

    using log4net;

    using Microsoft.Practices.Unity;

    using Moq;

    using NUnit.Framework;

    using Olive;
    using Olive.Bitcoin;
    using Olive.Bitcoin.BitcoinSync;
    using Olive.Services;

    [TestFixture]
    public class IncomingTransactionProcessorTests
    {
        /// <summary>
        /// Simulates that two transactions have come in to the Bitcoin server:
        ///   Receive 50 BTC for user 1000
        ///   Receive 75 BTC for user 1001
        /// The expected behavior is that the processor calls Process(Transaction) twice.
        /// </summary>
        [Test]
        public void SuccessfulProcessTest1()
        {
            // Arrange
            var container = new UnityContainer();

            var appender = new log4net.Appender.ConsoleAppender { Layout = new log4net.Layout.SimpleLayout() };
            log4net.Config.BasicConfigurator.Configure(appender);

            var logger = LogManager.GetLogger(typeof(IncomingTransactionProcessorTests));

            container.RegisterInstance(logger);

            var sessionId = Guid.NewGuid();

            var mockService = new Mock<IClientService>(MockBehavior.Strict);
            mockService.Setup(s => s.GetLastProcessedTransactionId(sessionId)).Returns(default(string));
            container.RegisterInstance(mockService.Object);

            var mockRpcClient = new Mock<IRpcClient>(MockBehavior.Strict);

            var transactions = new List<Transaction>
                {
                    new Transaction
                        {
                            Account = "100",
                            Address = "address",
                            Amount = 50m,
                            Category = "receive",
                            Confirmations = 1,
                            Time = DateTime.UtcNow.AddMinutes(-11),
                            TransactionId = "1000"
                        },
                    new Transaction
                        {
                            Account = "101",
                            Address = "address",
                            Amount = 75m,
                            Category = "receive",
                            Confirmations = 1,
                            Time = DateTime.UtcNow.AddMinutes(-10),
                            TransactionId = "1001"
                        },
                };

            mockRpcClient.Setup(r => r.GetTransactions(null, 1000, 0)).Returns(transactions.Take(1).ToList());
            ////mockRpcClient.Setup(r => r.GetTransactions(null, 12, 0)).Returns(transactions.Take(2).ToList());
            ////mockRpcClient.Setup(r => r.GetTransactions(null, 14, 0)).Returns(transactions.Take(2).ToList());
            container.RegisterInstance(mockRpcClient.Object);

            var processor = new Mock<IncomingTransactionProcessor>() { CallBase = true };
            processor.Setup(p => p.Process(sessionId, It.IsAny<Transaction>()));
            container.BuildUp(processor.Object);

            // Act
            processor.Object.Process(sessionId);
            
            // Assert
            mockService.Verify(s => s.GetLastProcessedTransactionId(sessionId), Times.Once());
            mockRpcClient.Verify(r => r.GetTransactions(null, 1000, 0), Times.Once());
            ////mockRpcClient.Verify(r => r.GetTransactions(null, 12, 0), Times.Once());
            ////mockRpcClient.Verify(r => r.GetTransactions(null, 14, 0), Times.Once());
            processor.Verify(p => p.Process(sessionId, It.IsAny<Transaction>()), Times.Exactly(1));
        }

        [Test]
        public void SuccessfulProcessTransaction1()
        {
            var accountId = UnitTestHelper.Random.Next(1, int.MaxValue);

            var transaction = new Transaction
                {
                    Account = accountId.ToString(CultureInfo.InvariantCulture),
                    Address = UnitTestHelper.Random.Next(1, int.MaxValue).ToString(CultureInfo.InvariantCulture),
                    Amount = (decimal)(UnitTestHelper.Random.NextDouble() * 10000) / 100m,
                    Category = "receive",
                    Confirmations = UnitTestHelper.Random.Next(1, 250),
                    Time = DateTime.UtcNow.AddDays(-UnitTestHelper.Random.Next(1, 100)),
                    TransactionId = UnitTestHelper.Random.Next(1, int.MaxValue).ToString(CultureInfo.InvariantCulture)
                };

            var container = new UnityContainer();

            var logger = LogManager.GetLogger(typeof(IncomingTransactionProcessorTests));
            container.RegisterInstance(logger);

            var sessionId = Guid.NewGuid();

            var mockService = new Mock<IClientService>(MockBehavior.Strict);
            mockService.Setup(s => s.TransactionIsProcessed(sessionId, transaction.TransactionId)).Returns(false);
            mockService.Setup(s => s.CreditTransactionWithHold(sessionId, accountId, transaction.TransactionId, transaction.Amount, "EXU"));
            mockService.Setup(s => s.ReleaseTransactionHold(sessionId, transaction.TransactionId));
            container.RegisterInstance(mockService.Object);

            var mockRpcClient = new Mock<IRpcClient>(MockBehavior.Strict);

            mockRpcClient.Setup(r => r.Move(transaction.Account, "EXU", transaction.Amount, 1, null));
            container.RegisterInstance(mockRpcClient.Object);

            var processor = new Mock<IncomingTransactionProcessor>() { CallBase = true };
            container.BuildUp(processor.Object);

            // Act
            processor.Object.Process(sessionId, transaction);

            // Assert
            mockService.Verify(s => s.TransactionIsProcessed(sessionId, transaction.TransactionId), Times.Once());
            mockService.Verify(s => s.CreditTransactionWithHold(sessionId, accountId, transaction.TransactionId, transaction.Amount, "EXU"), Times.Once());
            mockService.Verify(s => s.ReleaseTransactionHold(sessionId, transaction.TransactionId), Times.Once());
            mockRpcClient.Verify(r => r.Move(transaction.Account, "EXU", transaction.Amount, 1, null), Times.Once());
        }

        /// <summary>
        /// This test creates a new account for a fictional user and sends a small amount
        /// from the "Free" account to it. The BitcoinSync should pick this up and process it.
        /// </summary>
        [Test]
        public void IntegrationWithBitcoinDaemonTest()
        {
            var accountId = UnitTestHelper.Random.Next(1, int.MaxValue);

            var container = new UnityContainer();

            var logger = LogManager.GetLogger(typeof(IncomingTransactionProcessorTests));
            container.RegisterInstance(logger);

            var mockService = new Mock<IClientService>(MockBehavior.Strict);
            container.RegisterInstance(mockService.Object);

            var mockRpcClient = new Mock<RpcClient> { CallBase = true };
            mockRpcClient.Object.Hostname = "127.0.0.1";
            mockRpcClient.Object.PortNumber = 8502;
            mockRpcClient.Object.Credential = new NetworkCredential("user", "password");

            container.RegisterInstance<IRpcClient>(mockRpcClient.Object);

            var processor = new Mock<IncomingTransactionProcessor> { CallBase = true };
            container.BuildUp(processor.Object);

            var amount = 0.00001m * UnitTestHelper.Random.Next(1, 100);

            // Locate the most recent transaction before this point
            // TODO: Magic number
            var lastTransaction =
                mockRpcClient.Object.GetTransactions(null, 100, 0).OrderBy(x => x.Time).Where(x => x.Category == "receive").LastOrDefault();

            var lastTransactionId = lastTransaction == null ? null : lastTransaction.TransactionId;

            Console.WriteLine("Last transaction (actual) before test is " + lastTransactionId ?? "<none>");

            // Create a new address for the account
            var accountAddress = mockRpcClient.Object.GetNewAddress(accountId.ToString(CultureInfo.InvariantCulture));

            Console.WriteLine("New address for account " + accountId + " is " + accountAddress);

            // Send the amount
            var transactionId = mockRpcClient.Object.Send("Free", accountAddress, amount);

            Console.WriteLine("Sent " + amount + " BTC from 'Free' to " + accountAddress + " with transaction id #" + transactionId);

            var sessionId = Guid.NewGuid();

            mockService.Setup(s => s.GetLastProcessedTransactionId(sessionId)).Returns(lastTransactionId);
            mockService.Setup(s => s.TransactionIsProcessed(sessionId, transactionId)).Returns(false);
            mockService.Setup(s => s.CreditTransactionWithHold(sessionId, accountId, transactionId, amount, "EXU"));
            mockService.Setup(s => s.ReleaseTransactionHold(sessionId, transactionId));

            Console.WriteLine("Processing transaction log. Mocking will prevent anything but #" + transactionId + " from being processed.");

            // Act
            processor.Object.Process(sessionId);

            // Assert
            mockService.Verify(s => s.GetLastProcessedTransactionId(sessionId));
            mockService.Verify(s => s.TransactionIsProcessed(sessionId, transactionId));
            mockService.Verify(s => s.CreditTransactionWithHold(sessionId, accountId, transactionId, amount, "EXU"));
            mockService.Verify(s => s.ReleaseTransactionHold(sessionId, transactionId));
        }
    }
}
