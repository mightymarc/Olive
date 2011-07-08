// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="GlobalSuppressions.cs">
//   
// </copyright>
// <summary>
//   GlobalSuppressions.cs
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", 
        Scope = "member", Target = "Olive.DataAccess.Account.#Users")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", 
        Scope = "member", Target = "Olive.DataAccess.Account.#OutgoingTransfers")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", 
        Scope = "member", Target = "Olive.DataAccess.Account.#IncomingTransfers")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", 
        "CA2100:Review SQL queries for security vulnerabilities", Scope = "member", 
        Target = "Olive.DataAccess.DbConnectionExtensions.#CreateCommand(System.Data.Common.DbConnection,System.String)"
        )]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", 
        MessageId = "Db", Scope = "type", Target = "Olive.DataAccess.DbConnectionExtensions")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", 
        MessageId = "dest", Scope = "member", 
        Target = "Olive.DataAccess.OliveContext.#CreateTransfer(System.Int32,System.Int32,System.String,System.Decimal)"
        )]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", 
        MessageId = "Moneybookers", Scope = "member", Target = "Olive.DataAccess.AccountType.#IncomingMoneybookersUsd")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", 
        MessageId = "Dest", Scope = "member", Target = "Olive.DataAccess.Transfer.#DestAccountId")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", 
        MessageId = "Dest", Scope = "member", Target = "Olive.DataAccess.Transfer.#DestAccount")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", 
        MessageId = "0", Scope = "member", 
        Target = "Olive.DataAccess.OliveContext.#OnModelCreating(System.Data.Entity.DbModelBuilder)")]
[assembly:
    System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", 
        Scope = "member", Target = "Olive.DataAccess.User.#AccountAccess")]