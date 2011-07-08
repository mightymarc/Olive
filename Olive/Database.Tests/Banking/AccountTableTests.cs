// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountTableTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the AccountTableTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests.Banking
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccountTableTests : DatabaseTestClass
    {
        private DatabaseTestActions AllowNegativeEnforcedData;

        public AccountTableTests()
        {
            this.InitializeComponent();
        }

        [TestMethod]
        public void AllowNegativeEnforced()
        {
            DatabaseTestActions testActions = this.AllowNegativeEnforcedData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.CleanupTest();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.InitializeTest();
        }

        /// <summary>
        ///   Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DatabaseTestAction AllowNegativeEnforced_TestAction;
            var resources = new ComponentResourceManager(typeof(AccountTableTests));
            ScalarValueCondition scalarValueCondition1;
            this.AllowNegativeEnforcedData = new DatabaseTestActions();
            AllowNegativeEnforced_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();

            // AllowNegativeEnforced_TestAction
            AllowNegativeEnforced_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(AllowNegativeEnforced_TestAction, "AllowNegativeEnforced_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // AllowNegativeEnforcedData
            this.AllowNegativeEnforcedData.PosttestAction = null;
            this.AllowNegativeEnforcedData.PretestAction = null;
            this.AllowNegativeEnforcedData.TestAction = AllowNegativeEnforced_TestAction;
        }

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
    }
}