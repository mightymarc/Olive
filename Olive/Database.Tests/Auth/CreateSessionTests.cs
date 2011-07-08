// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateSessionTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the CreateSessionTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.Auth
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CreateSessionTests : DatabaseTestClass
    {
        private DatabaseTestActions CorrectHashReturnsSessionIdData;

        private DatabaseTestActions IncorrectHashReturnsErrorCodeData;

        private DatabaseTestActions UnknownEmailReturnsErrorCodeData;

        public CreateSessionTests()
        {
            this.InitializeComponent();
        }

        [TestMethod]
        public void CorrectHashReturnsSessionId()
        {
            DatabaseTestActions testActions = this.CorrectHashReturnsSessionIdData;

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
        public void IncorrectHashReturnsErrorCode()
        {
            DatabaseTestActions testActions = this.IncorrectHashReturnsErrorCodeData;

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

        [TestMethod]
        public void UnknownEmailReturnsErrorCode()
        {
            DatabaseTestActions testActions = this.UnknownEmailReturnsErrorCodeData;

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
            DatabaseTestAction CorrectHashReturnsSessionId_TestAction;
            var resources = new ComponentResourceManager(typeof(CreateSessionTests));
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction IncorrectHashReturnsErrorCode_TestAction;
            ScalarValueCondition scalarValueCondition2;
            DatabaseTestAction UnknownEmailReturnsErrorCode_TestAction;
            ScalarValueCondition scalarValueCondition3;
            this.CorrectHashReturnsSessionIdData = new DatabaseTestActions();
            this.IncorrectHashReturnsErrorCodeData = new DatabaseTestActions();
            this.UnknownEmailReturnsErrorCodeData = new DatabaseTestActions();
            CorrectHashReturnsSessionId_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            IncorrectHashReturnsErrorCode_TestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();
            UnknownEmailReturnsErrorCode_TestAction = new DatabaseTestAction();
            scalarValueCondition3 = new ScalarValueCondition();

            // CorrectHashReturnsSessionId_TestAction
            CorrectHashReturnsSessionId_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(CorrectHashReturnsSessionId_TestAction, "CorrectHashReturnsSessionId_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // IncorrectHashReturnsErrorCode_TestAction
            IncorrectHashReturnsErrorCode_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(
                IncorrectHashReturnsErrorCode_TestAction, "IncorrectHashReturnsErrorCode_TestAction");

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;

            // UnknownEmailReturnsErrorCode_TestAction
            UnknownEmailReturnsErrorCode_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(UnknownEmailReturnsErrorCode_TestAction, "UnknownEmailReturnsErrorCode_TestAction");

            // scalarValueCondition3
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;

            // CorrectHashReturnsSessionIdData
            this.CorrectHashReturnsSessionIdData.PosttestAction = null;
            this.CorrectHashReturnsSessionIdData.PretestAction = null;
            this.CorrectHashReturnsSessionIdData.TestAction = CorrectHashReturnsSessionId_TestAction;

            // IncorrectHashReturnsErrorCodeData
            this.IncorrectHashReturnsErrorCodeData.PosttestAction = null;
            this.IncorrectHashReturnsErrorCodeData.PretestAction = null;
            this.IncorrectHashReturnsErrorCodeData.TestAction = IncorrectHashReturnsErrorCode_TestAction;

            // UnknownEmailReturnsErrorCodeData
            this.UnknownEmailReturnsErrorCodeData.PosttestAction = null;
            this.UnknownEmailReturnsErrorCodeData.PretestAction = null;
            this.UnknownEmailReturnsErrorCodeData.TestAction = UnknownEmailReturnsErrorCode_TestAction;
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