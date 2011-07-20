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
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using log4net;

    using Microsoft.Practices.Unity;

    using Olive.Bitcoin.BitcoinSync.Properties;
    using Olive.Services;

    public class IncomingTransactionProcessor
    {
        private const string ReceivedTransferCategory = "receive";

        private Properties.BitcoinSyncSettings settings = new BitcoinSyncSettings();

        [Dependency]
        public ILog Logger { get; set; }

        [Dependency]
        public IBitcoinService BitcoinService { get; set; }

        [Dependency]
        public IRpcClient RpClient { get; set; }

        public virtual void Process()
        {
            this.Logger.Debug("Looking up last processed transaction...");

            var lastTransactionId = this.BitcoinService.GetLastProcessedTransactionId();

            this.Logger.DebugFormat("Last processed transaction id is {0}.", lastTransactionId);
            this.Logger.DebugFormat("Looking for transactions that occured after the last processed transaction.");

            var newTransactions = this.GetTransactionsAfter(lastTransactionId);

            this.Logger.InfoFormat("Found new {0} transaction(s).", newTransactions.Count);

            foreach (var newTransaction in newTransactions.Where(t => t.Category == ReceivedTransferCategory))
            {
                this.Process(newTransaction);
            }
        }

        public virtual void Process(Transaction transaction)
        {
            if (transaction.Account == string.Empty)
            {
                return;
            }

            this.Logger.InfoFormat("Procsssing transaction #{0}.", transaction.TransactionId);

            if (this.BitcoinService.TransactionIsProcessed(transaction.TransactionId))
            {
                this.Logger.WarnFormat("Will not process transaction #{0} because the service claims it has already been processed.", transaction.TransactionId);
                return;
            }

            if (!(transaction.Amount > 0))
            {
                throw new Exception("The amount for a received transaction must be > 0");
            }

            int accountId;

            if (!int.TryParse(transaction.Account, NumberStyles.Integer, CultureInfo.InvariantCulture, out accountId))
            {
                this.Logger.WarnFormat(
                    "Will not process transaction #{0} because it maps to somethign that's not a user id ({1}).",
                    transaction.TransactionId,
                    transaction.Account);
                return;
            }

            this.BitcoinService.CreditTransactionWithHold(accountId, transaction.TransactionId, transaction.Amount, this.settings.Currency);
            this.RpClient.Move(transaction.Account, this.settings.Currency, transaction.Amount);
            this.BitcoinService.ReleaseTransactionHold(transaction.TransactionId);
        }

        private List<Transaction> GetTransactionsAfter(string transactionId)
        {
            const int TransactionCountStep = 1000;
            var transactionCount = 1000;

            Func<Transaction, bool> transactionFilter = t => t.Account != string.Empty && t.Category == "receive";

            if (transactionId == null)
            {
                while (true)
                {
                    this.Logger.DebugFormat("Looking for the most recent {0} transactions.", transactionCount);

                    var transactions = this.RpClient.GetTransactions(null, transactionCount);

                    if (transactions.Count == transactionCount)
                    {
                        transactionCount += TransactionCountStep;
                        continue;
                    }

                    return transactions.Where(transactionFilter).ToList();
                }
            }
            else
            {
                while (true)
                {
                    var transactions = this.RpClient.GetTransactions(null, transactionCount).Where(transactionFilter).ToList();

                    if (transactions.Count == 0)
                    {
                        return new List<Transaction>();
                    }

                    var lastTransaction = transactions.SingleOrDefault(t => t.TransactionId == transactionId);

                    if (lastTransaction == null)
                    {
                        // Transaction to look for was not found, go further back in time.
                        transactionCount += TransactionCountStep;
                        continue;
                    }

                    var newTransactions = transactions.Where(t => t.Time > lastTransaction.Time);

                    return newTransactions.ToList();
                }
            }
        }
    }
}
