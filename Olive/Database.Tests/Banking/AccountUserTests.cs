// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountUserTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the AccountUserTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.Banking
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccountUserTests : DatabaseTestClass
    {
        private DatabaseTestActions AllowedToSelectData;

        private DatabaseTestActions NotAllowedToDeleteData;

        private DatabaseTestActions NotAllowedToUpdateData;

        public AccountUserTests()
        {
            this.InitializeComponent();
        }

        [TestMethod]
        public void AllowedToSelect()
        {
            DatabaseTestActions testActions = this.AllowedToSelectData;

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

        [TestMethod]
        public void NotAllowedToDelete()
        {
            DatabaseTestActions testActions = this.NotAllowedToDeleteData;

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

        [TestMethod]
        public void NotAllowedToUpdate()
        {
            DatabaseTestActions testActions = this.NotAllowedToUpdateData;

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
            DatabaseTestAction AllowedToSelect_TestAction;
            var resources = new ComponentResourceManager(typeof(AccountUserTests));
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction NotAllowedToDelete_TestAction;
            ScalarValueCondition scalarValueCondition2;
            DatabaseTestAction NotAllowedToUpdate_TestAction;
            ScalarValueCondition scalarValueCondition3;
            this.AllowedToSelectData = new DatabaseTestActions();
            this.NotAllowedToDeleteData = new DatabaseTestActions();
            this.NotAllowedToUpdateData = new DatabaseTestActions();
            AllowedToSelect_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            NotAllowedToDelete_TestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();
            NotAllowedToUpdate_TestAction = new DatabaseTestAction();
            scalarValueCondition3 = new ScalarValueCondition();

            // AllowedToSelectData
            this.AllowedToSelectData.PosttestAction = null;
            this.AllowedToSelectData.PretestAction = null;
            this.AllowedToSelectData.TestAction = AllowedToSelect_TestAction;

            // AllowedToSelect_TestAction
            AllowedToSelect_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(AllowedToSelect_TestAction, "AllowedToSelect_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // NotAllowedToDeleteData
            this.NotAllowedToDeleteData.PosttestAction = null;
            this.NotAllowedToDeleteData.PretestAction = null;
            this.NotAllowedToDeleteData.TestAction = NotAllowedToDelete_TestAction;

            // NotAllowedToDelete_TestAction
            NotAllowedToDelete_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(NotAllowedToDelete_TestAction, "NotAllowedToDelete_TestAction");

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;

            // NotAllowedToUpdateData
            this.NotAllowedToUpdateData.PosttestAction = null;
            this.NotAllowedToUpdateData.PretestAction = null;
            this.NotAllowedToUpdateData.TestAction = NotAllowedToUpdate_TestAction;

            // NotAllowedToUpdate_TestAction
            NotAllowedToUpdate_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(NotAllowedToUpdate_TestAction, "NotAllowedToUpdate_TestAction");

            // scalarValueCondition3
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
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