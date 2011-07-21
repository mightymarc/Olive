// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OliveContext.cs" company="Olive">
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
//   Defines the OliveContext data access class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess.Domain;

    /// <summary>
    /// The entity framework database context.
    /// </summary>
    public partial class OliveContext : DbContext, IOliveContext
    {
        /// <summary>
        ///   The connection used to execute stored procedures.
        /// </summary>
        private IDbConnection commandConnection;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "OliveContext" /> class.
        /// </summary>
        [InjectionConstructor]
        public OliveContext()
#if Dev
            : base("OliveLocal")
#else
            : base("Olive")
#endif
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OliveContext"/> class.
        /// </summary>
        /// <param name="nameOrConnectionString">
        /// The name or connection string.
        /// </param>
        public OliveContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        ///   Gets or sets Accounts.
        /// </summary>
        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<AccountHold> AccountHolds { get; set; }

        public IDbSet<BitcoinTransaction> BitcoinTransactions { get; set; }

        /// <summary>
        ///   Gets or sets AccountsWithBalance.
        /// </summary>
        public IDbSet<AccountWithBalance> AccountsWithBalance { get; set; }

        /// <summary>
        ///   Gets or sets the connection used to execute stored procedures.
        /// </summary>
        /// <value>
        ///   The command connection.
        /// </value>
        public virtual IDbConnection CommandConnection
        {
            get
            {
                return this.commandConnection ?? (this.commandConnection = this.Database.Connection);
            }

            set
            {
                this.commandConnection = value;
            }
        }

        /// <summary>
        ///   Gets or sets Currencies.
        /// </summary>
        public IDbSet<Currency> Currencies { get; set; }

        /// <summary>
        ///   Gets or sets Sessions.
        /// </summary>
        public IDbSet<Session> Sessions { get; set; }

        /// <summary>
        ///   Gets or sets Transfers.
        /// </summary>
        public IDbSet<Transfer> Transfers { get; set; }

        /// <summary>
        ///   Gets or sets Users.
        /// </summary>
        public IDbSet<User> Users { get; set; }

        public IDbSet<BitcoinAccountReceiveAddress> BitcoinAccountReceiveAddresses { get; set; }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        ///   before the model has been locked down and used to initialize the context.  The default
        ///   implementation of this method does nothing, but it can be overridden in a derived class
        ///   such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">
        /// The builder that defines the model for the context being created.
        /// </param>
        [SuppressMessage("Microsoft.Contracts", "Requires", Justification = "Warning from framework class.")]
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Can be done using reflection, see http://stackoverflow.com/questions/5718976/ef-4-1-rtm-entitytypeconfiguration
            modelBuilder.Configurations.Add(new EntityConfigurations.AccountConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.AccountUserConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.AccountWithBalanceConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.BitcoinAccountReceiveAddressConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.BitcoinTransactionConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.CurrencyConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.SessionConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.TransferConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.UserConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.RoleConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.BitcoinWithdrawAccountConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.AccountHoldConfiguration());
        }
    }
}