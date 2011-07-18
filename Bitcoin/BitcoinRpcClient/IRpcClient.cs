namespace Olive.Bitcoin
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    // TODO: Write code contract
    [ContractClass(typeof(IRpcClientContract))]
    public interface IRpcClient
    {
        string GetNewAddress(string accountId);

        List<Transaction> GetTransactions(string accountId = null, int count = 10, int skip = 0);

        void Move(string fromAccountId, string toAccountId, decimal amount, int minConfirmations = 1, string comment = null);

        string Send(
            string fromAccountId,
            string toAddress,
            decimal amount,
            int minConfirmations = 1,
            string comment = null,
            string commentTo = null);
    }
}