// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseSetup.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the DatabaseSetup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DatabaseSetup
    {
        [AssemblyInitialize]
        public static void InitializeAssembly(TestContext ctx)
        {
            // Setup the test database based on setting in the
            // configuration file
            DatabaseTestClass.TestService.DeployDatabaseProject();
            DatabaseTestClass.TestService.GenerateData();
        }
    }
}