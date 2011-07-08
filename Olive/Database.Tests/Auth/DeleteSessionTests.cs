// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteSessionTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the DeleteSessionTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.Auth
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DeleteSessionTests : DatabaseTestClass
    {
        private DatabaseTestActions FailsOnExpiredSessionData;

        private DatabaseTestActions FailsOnUnknownSessionIdData;

        private DatabaseTestActions SuccessData;

        public DeleteSessionTests()
        {
            this.InitializeComponent();
        }

        [TestMethod]
        public void FailsOnExpiredSession()
        {
            DatabaseTestActions testActions = this.FailsOnExpiredSessionData;

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
        public void FailsOnUnknownSessionId()
        {
            DatabaseTestActions testActions = this.FailsOnUnknownSessionIdData;

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
        public void Success()
        {
            DatabaseTestActions testActions = this.SuccessData;

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
            DatabaseTestAction Success_TestAction;
            var resources = new ComponentResourceManager(typeof(DeleteSessionTests));
            ScalarValueCondition scalarValueCondition3;
            DatabaseTestAction FailsOnExpiredSession_TestAction;
            DatabaseTestAction FailsOnUnknownSessionId_TestAction;
            ScalarValueCondition scalarValueCondition2;
            DatabaseTestAction FailsOnExpiredSession_PretestAction;
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction FailsOnExpiredSession_PosttestAction;
            this.SuccessData = new DatabaseTestActions();
            this.FailsOnExpiredSessionData = new DatabaseTestActions();
            this.FailsOnUnknownSessionIdData = new DatabaseTestActions();
            Success_TestAction = new DatabaseTestAction();
            scalarValueCondition3 = new ScalarValueCondition();
            FailsOnExpiredSession_TestAction = new DatabaseTestAction();
            FailsOnUnknownSessionId_TestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();
            FailsOnExpiredSession_PretestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            FailsOnExpiredSession_PosttestAction = new DatabaseTestAction();

            // Success_TestAction
            Success_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");

            // scalarValueCondition3
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;

            // FailsOnExpiredSession_TestAction
            resources.ApplyResources(FailsOnExpiredSession_TestAction, "FailsOnExpiredSession_TestAction");

            // FailsOnUnknownSessionId_TestAction
            FailsOnUnknownSessionId_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(FailsOnUnknownSessionId_TestAction, "FailsOnUnknownSessionId_TestAction");

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;

            // FailsOnExpiredSession_PretestAction
            FailsOnExpiredSession_PretestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(FailsOnExpiredSession_PretestAction, "FailsOnExpiredSession_PretestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // FailsOnExpiredSession_PosttestAction
            resources.ApplyResources(FailsOnExpiredSession_PosttestAction, "FailsOnExpiredSession_PosttestAction");

            // SuccessData
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = null;
            this.SuccessData.TestAction = Success_TestAction;

            // FailsOnExpiredSessionData
            this.FailsOnExpiredSessionData.PosttestAction = FailsOnExpiredSession_PosttestAction;
            this.FailsOnExpiredSessionData.PretestAction = FailsOnExpiredSession_PretestAction;
            this.FailsOnExpiredSessionData.TestAction = FailsOnExpiredSession_TestAction;

            // FailsOnUnknownSessionIdData
            this.FailsOnUnknownSessionIdData.PosttestAction = null;
            this.FailsOnUnknownSessionIdData.PretestAction = null;
            this.FailsOnUnknownSessionIdData.TestAction = FailsOnUnknownSessionId_TestAction;
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