namespace Olive.Bitcoin
{
    using System.Collections.Generic;

    // TODO: Write code contract
    public interface IRpcClient
    {
        string GetNewAddress(string accountId);

        List<Transaction> GetTransactions(string accountId = null, int count = 10, int skip = 0);

        void Move(string fromAccountId, string toAccountId, decimal amount);
    }
}