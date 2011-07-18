namespace Olive.Bitcoin
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IRpcClient))]
    public abstract class IRpcClientContract : IRpcClient
    {
        public string GetNewAddress(string accountId)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.Ensures(Contract.Result<string>().Length == 34);
            return default(string);
        }

        public List<Transaction> GetTransactions(string accountId, int count, int skip)
        {
            Contract.Requires<ArgumentException>(count > 0, "count");
            Contract.Requires<ArgumentException>(skip >= 0, "skip");
            Contract.Ensures(Contract.Result<List<Transaction>>() != null);
            return default(List<Transaction>);
        }

        public void Move(string fromAccountId, string toAccountId, decimal amount, int minConfirmations, string comment)
        {
            Contract.Requires<ArgumentException>(fromAccountId != toAccountId, "fromAccount != toAccount");
            Contract.Requires<ArgumentException>(amount > 0, "amount");
        }

        public string Send(string fromAccountId, string toAddress, decimal amount, int minConfirmations, string comment, string commentTo)
        {
            Contract.Requires<ArgumentException>(amount > 0, "amount");
            Contract.Requires<ArgumentException>(minConfirmations >= 1, "minConfirmations");
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.Ensures(Contract.Result<string>().Length == 64);
            return default(string);
        }
    }
}