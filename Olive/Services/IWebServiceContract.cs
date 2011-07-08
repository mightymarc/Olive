namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Text.RegularExpressions;

    using Olive.DataAccess;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
        Justification = "Contract for IWebService.")]
    [ContractClassFor(typeof(IWebService))]
    public abstract class IWebServiceContract : IWebService
    {
        public Guid CreateSession(string email, string password)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email), "email");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(password), "password");
            Contract.Requires<ArgumentException>(Regex.IsMatch(email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"));
            ////Contract.Requires<ArgumentException>(Regex.IsMatch(password, "^.{8,50}$"));
            Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);

            return default(Guid);
        }

        public void CreateUser(string email, string password)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(email), "email");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(password), "password");
            Contract.Requires<ArgumentException>(Regex.IsMatch(email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"));
            Contract.Requires<ArgumentException>(Regex.IsMatch(password, "^.{8,50}$"));
        }

        public List<GetAccountTransfersTransfer> GetAccountTransfers(Guid sessionId, int accountId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");
            Contract.Ensures(Contract.Result<List<GetAccountTransfersTransfer>>() != null);

            return default(List<GetAccountTransfersTransfer>);
        }

        public GetAccountAccount GetAccount(Guid sessionId, int accountId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");
            Contract.Ensures(Contract.Result<GetAccountAccount>() != null);

            return default(GetAccountAccount);
        }

        public AccountOverview GetAccounts(Guid sessionId)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<List<AccountWithBalance>>() != null);

            return default(AccountOverview);
        }

        public int CreateCurrentAccount(Guid sessionId, string currencyId, string displayName)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(currencyId), "currencyId");
            Contract.Ensures(Contract.Result<int>() > 0);

            return default(int);
        }

        public void EditAccount(Guid sessionId, int accountId, string displayName)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(accountId > 0, "accountId");

            return;
        }

        public List<string> GetCurrencies()
        {
            Contract.Ensures(Contract.Result<List<string>>() != null);
            return default(List<string>);
        }

        public long CreateTransfer(Guid sessionId, int sourceAccountId, int destAccountId, decimal amount, string description)
        {
            Contract.Requires<ArgumentException>(sessionId != Guid.Empty, "sessionId");
            Contract.Requires<ArgumentException>(sourceAccountId > 0, "sourceAccountId");
            Contract.Requires<ArgumentException>(destAccountId > 0, "destAccountId");
            Contract.Requires<ArgumentException>(amount > 0, "amount");
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(description), "description");
            return default(long);
        }
    }
}