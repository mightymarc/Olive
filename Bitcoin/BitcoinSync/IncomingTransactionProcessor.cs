// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IncomingTransactionProcessor.cs" company="Olive">
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
//   Defines the IncomingTransactionProcessor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Bitcoin.BitcoinSync
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using log4net;

    using Microsoft.Practices.Unity;

    using Olive.Services;

    public class IncomingTransactionProcessor
    {
        private const string ReceivedTransferCategory = "receive";

        private const string IncomingBitcoinBTCAccount = "IncomingBitcoinBTC";

        [Dependency]
        public ILog Logger { get; set; }

        [Dependency]
        public IBitcoinService BitcoinService { get; set; }

        [Dependency]
        public IRpcClient RpClient { get; set; }

        public void Process()
        {
            this.Logger.Debug("Looking up last processed transaction...");

            var lastTransactionId = this.BitcoinService.GetLastProcessedTransationId();

            this.Logger.DebugFormat("Last processed transaction id is {0}.", lastTransactionId);
            this.Logger.DebugFormat("Looking for transactions that occured after the last processed transaction.");

            var newTransactions = this.GetTransationsAfter(lastTransactionId);

            this.Logger.InfoFormat("Found new {0} transaction(s).", newTransactions.Count);

            foreach (var newTransaction in newTransactions.Where(t => t.Category == ReceivedTransferCategory))
            {
                this.Process(newTransaction);
            }
        }

        private void Process(Transaction transaction)
        {
            this.Logger.InfoFormat("Procsssing transaction #{0}.", transaction.TransactionId);

            if (this.BitcoinService.TransactionIsProcessed(transaction.TransactionId))
            {
                throw new InvalidOperationException("The specified transaction has already been processed.");
            }

            if (!(transaction.Amount > 0))
            {
                throw new Exception("The amount for a received transaction must be > 0");
            }

            var accountId = int.Parse(transaction.Account, CultureInfo.InvariantCulture);

            this.BitcoinService.CreditTransactionWithHold(accountId, transaction.TransactionId, transaction.Amount);

            // TODO: If an exception occurs, reverse the transaction and release the hold.
            this.RpClient.Move(transaction.Account, IncomingBitcoinBTCAccount, transaction.Amount);

            this.BitcoinService.ReleaseTransactionHold(transaction.TransactionId);
        }

        private List<Transaction> GetTransationsAfter(string transactionId)
        {
            const int transactionCountStep = 2;
            var transactionCount = 10;

            if (transactionId == null)
            {
                List<Transaction> prevTransactions = null;

                while (true)
                {
                    var transactions = this.RpClient.GetTransactions(null, transactionCount);

                    if (prevTransactions == null)
                    {
                        prevTransactions = transactions;
                        transactionCount += transactionCountStep;
                        continue;
                    }

                    // Are there any older entries in transactions than in prevTransactions?
                    var oldestPrevTransaction = prevTransactions.Min(t => t.Time);

                    if (!transactions.Any(t => t.Time < oldestPrevTransaction))
                    {
                        return transactions;
                    }

                    // There are older transactions, keep going backwards.
                    prevTransactions = transactions;
                    transactionCount += transactionCountStep;
                }
            }
            else
            {
                while (true)
                {
                    var transactions = this.RpClient.GetTransactions(null, transactionCount);
                    var lastTransaction = transactions.SingleOrDefault(t => t.TransactionId == transactionId);

                    if (lastTransaction == null)
                    {
                        // Transaction to look for was not found, go further back in time.
                        transactionCount += transactionCountStep;
                        continue;
                    }

                    var newTransactions = transactions.Where(t => t.Time > lastTransaction.Time);

                    return newTransactions.ToList();
                }
            }
        }
    }
}
