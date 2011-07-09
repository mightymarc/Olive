// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockOliveContext.cs" company="Olive">
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
//   Defines the MockOliveContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Data;
    using System.Data.Entity;

    using Olive.Services.Tests;

    /// <summary>
    /// The mock olive context.
    /// </summary>
    public class MockOliveContext : IOliveContext, IDisposable
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MockOliveContext"/> class.
        /// </summary>
        public MockOliveContext()
        {
            // Set up your collections
            this.Users = new MockDbSet<User> {
                    new User
                        {
                            Email = "andreas@opuno.com", 
                            PasswordHash =
                                "eJsSKba6/GAXbyZ5AhoRSgUkwojQLOYevHxVPrY8UCKZe0e9sH+YL3F7DfaYdsnKHIpGxIsIfCl/KJfZRDA0dg==", 
                            PasswordSalt =
                                "GIWdAnVlZAIt8REiQArFVWs+nJJb9f+5Br61pxLUpA9H5T0vCq7th2l+TPHl6WOGHqi+7GbxRc0r8tOdMf5Qrg=="
                        }
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets Accounts.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbSet<Account> Accounts
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets AccountsWithBalance.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbSet<AccountWithBalance> AccountsWithBalance
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets Currencies.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbSet<Currency> Currencies
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets Sessions.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbSet<Session> Sessions
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets Transfers.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbSet<Transfer> Transfers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        public IDbSet<User> Users { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The create current account.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="currencyId">
        /// The currency id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <returns>
        /// The create current account.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public int CreateCurrentAccount(int userId, string currencyId, string displayName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create session.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="passwordHash">
        /// The password hash.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Guid CreateSession(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create session.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Guid CreateSession(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create transfer.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public long CreateTransfer(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create transfer.
        /// </summary>
        /// <param name="sourceAccountId">
        /// The source account id.
        /// </param>
        /// <param name="destAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The create transfer.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// The edit current account.
        /// </summary>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <param name="displayName">
        /// The display name.
        /// </param>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void EditCurrentAccount(int accountId, string displayName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get create session command.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="passwordHash">
        /// The password hash.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbCommand GetCreateSessionCommand(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get create transfer command.
        /// </summary>
        /// <param name="sourceAccountId">
        /// The source account id.
        /// </param>
        /// <param name="destAccountId">
        /// The dest account id.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbCommand GetCreateTransferCommand(
            int sourceAccountId, int destAccountId, string description, decimal amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get user salt from email.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The get user salt from email.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public string GetUserSaltFromEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get verify session command.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public IDbCommand GetVerifySessionCommand(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        public void SaveChanges()
        {
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public IDbSet<T> Set<T>() where T : class
        {
            foreach (var property in typeof(MockOliveContext).GetProperties())
            {
                if (property.PropertyType == typeof(IDbSet<T>))
                {
                    return property.GetValue(this, null) as IDbSet<T>;
                }
            }

            throw new Exception("Type collection not found.");
        }

        /// <summary>
        /// The verify session.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// The verify session.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public int VerifySession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The verify session.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        /// <returns>
        /// The verify session.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public int VerifySession(IDbCommand command)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}