// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Transfer.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the Transfer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests.Banking
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Transfer : DatabaseTestClass
    {
        private DatabaseTestActions Transfer_SourceAndDestAccountsHaveSameCurrencyData;

        public Transfer()
        {
            this.InitializeComponent();
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

        [TestMethod]
        public void Transfer_SourceAndDestAccountsHaveSameCurrency()
        {
            DatabaseTestActions testActions = this.Transfer_SourceAndDestAccountsHaveSameCurrencyData;

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

        /// <summary>
        ///   Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DatabaseTestAction Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction;
            var resources = new ComponentResourceManager(typeof(Transfer));
            ScalarValueCondition scalarValueCondition1;
            this.Transfer_SourceAndDestAccountsHaveSameCurrencyData = new DatabaseTestActions();
            Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();

            // Transfer_SourceAndDestAccountsHaveSameCurrencyData
            this.Transfer_SourceAndDestAccountsHaveSameCurrencyData.PosttestAction = null;
            this.Transfer_SourceAndDestAccountsHaveSameCurrencyData.PretestAction = null;
            this.Transfer_SourceAndDestAccountsHaveSameCurrencyData.TestAction =
                Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction;

            // Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction
            Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(
                Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction, 
                "Transfer_SourceAndDestAccountsHaveSameCurrency_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
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