namespace Olive.DataAccess
{
    using System;
    using System.Data.Entity;

    public interface IOliveContext : IDisposable
    {
        /// <summary>
        /// Gets or sets Accounts.
        /// </summary>
        IDbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets AccountsWithBalance.
        /// </summary>
        IDbSet<AccountWithBalance> AccountsWithBalance { get; set; }

        /// <summary>
        /// Gets or sets Currencies.
        /// </summary>
        IDbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets Sessions.
        /// </summary>
        IDbSet<Session> Sessions { get; set; }

        /// <summary>
        /// Gets or sets Transfers.
        /// </summary>
        IDbSet<Transfer> Transfers { get; set; }

        /// <summary>
        /// Gets or sets Users.
        /// </summary>
        IDbSet<User> Users { get; set; }

        /// <summary>
        /// Creates the session.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <returns>The session identifier.</returns>
        Guid CreateSession(string email, string passwordHash);

        /// <summary>
        /// Creates the transfer.
        /// </summary>
        /// <param name="sourceAccountId">The source account id.</param>
        /// <param name="destAccountId">The dest account id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>The identifier for the transfer that was created.</returns>
        long CreateTransfer(int sourceAccountId, int destAccountId, string description, decimal amount);

        /// <summary>
        /// Verifies that specified session exists and is not expired.
        /// </summary>
        /// <param name="sessionId">The session id.</param>
        /// <returns>
        /// The user id of the user that owns the session.
        /// </returns>
        int VerifySession(Guid sessionId);

        void SaveChanges();
    }
}