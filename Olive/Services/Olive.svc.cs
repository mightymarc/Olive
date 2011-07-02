// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Olive.svc.cs" company="Olive">
//   [Copyright]
// </copyright>
// <summary>
//   Defines the Olive type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    using Olive.DataAccess;

    public class WebService : IWebService
    {
        private OliveContext GetContext()
        {
            
            return new OliveContext(@"server=.\SQLEXPRESS;database=OliveTest;user=ServiceUser;password=temp;");
        }

        public Guid CreateSession(int userId, string password)
        {
            using (var context = this.GetContext())
            {
                var salt = context.Users.Find(userId).PasswordSalt;
                var hash = Crypto.GenerateHash(password, salt);
                return context.CreateSession(userId, hash);
            }
        }

        public int CreateUser(string password)
        {
            using (var context = this.GetContext())
            {
                var salt = Crypto.CreateSalt(64);
                var hash = Crypto.GenerateHash(password, salt);

                var user = new User { PasswordHash = hash, PasswordSalt = salt };
                context.Users.Add(user);
                context.SaveChanges();

                return user.UserId;
            }
        }

        public AccountOverview GetAccounts(Guid sessionId)
        {
            Contract.Requires<ArgumentNullException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<List<AccountWithBalance>>() != null);

            var userId = this.VerifySession(sessionId);

            using (var context = this.GetContext())
            {
                var query = from account in context.Users.Find(userId).AccountAccess.Select(x => x.Account)
                            let accountWithBalance = context.AccountsWithBalance.Find(account.AccountId)
                            select
                                new AccountOverviewAccount
                                    {
                                        AccountId = account.AccountId,
                                        Balance = accountWithBalance.Balance,
                                        CurrencyShortName = accountWithBalance.Currency.ShortName,
                                        DisplayName = account.DisplayName
                                    };

                var result = new AccountOverview();
                result.AddRange(query);
                return result;
            }
        }

        private int VerifySession(Guid sessionId)
        {
            Contract.Requires<ArgumentNullException>(sessionId != Guid.Empty, "sessionId");
            Contract.Ensures(Contract.Result<int>() > 0);

            using (var context = this.GetContext())
            {
                return context.VerifySession(sessionId);
            }
        }
    }
}
