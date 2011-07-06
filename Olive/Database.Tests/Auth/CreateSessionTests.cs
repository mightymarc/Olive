using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Conditions;

namespace Olive.Database.Tests.Auth
{
    [TestClass()]
    public class CreateSessionTests : DatabaseTestClass
    {

        public CreateSessionTests()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        [TestMethod()]
        public void CorrectHashReturnsSessionId()
        {
            DatabaseTestActions testActions = this.CorrectHashReturnsSessionIdData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            // Execute the test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            // Execute the post-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }
        [TestMethod()]
        public void IncorrectHashReturnsErrorCode()
        {
            DatabaseTestActions testActions = this.IncorrectHashReturnsErrorCodeData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            // Execute the test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            // Execute the post-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }
        [TestMethod()]
        public void UnknownUserIdReturnsErrorCode()
        {
            DatabaseTestActions testActions = this.UnknownUserIdReturnsErrorCodeData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            // Execute the test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            // Execute the post-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }



        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction CorrectHashReturnsSessionId_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSessionTests));
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction IncorrectHashReturnsErrorCode_TestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction UnknownUserIdReturnsErrorCode_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            this.CorrectHashReturnsSessionIdData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.IncorrectHashReturnsErrorCodeData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.UnknownUserIdReturnsErrorCodeData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            CorrectHashReturnsSessionId_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            IncorrectHashReturnsErrorCode_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            UnknownUserIdReturnsErrorCode_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            scalarValueCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // CorrectHashReturnsSessionId_TestAction
            // 
            CorrectHashReturnsSessionId_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(CorrectHashReturnsSessionId_TestAction, "CorrectHashReturnsSessionId_TestAction");
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
            // 
            // IncorrectHashReturnsErrorCode_TestAction
            // 
            IncorrectHashReturnsErrorCode_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(IncorrectHashReturnsErrorCode_TestAction, "IncorrectHashReturnsErrorCode_TestAction");
            // 
            // UnknownUserIdReturnsErrorCode_TestAction
            // 
            UnknownUserIdReturnsErrorCode_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(UnknownUserIdReturnsErrorCode_TestAction, "UnknownUserIdReturnsErrorCode_TestAction");
            // 
            // CorrectHashReturnsSessionIdData
            // 
            this.CorrectHashReturnsSessionIdData.PosttestAction = null;
            this.CorrectHashReturnsSessionIdData.PretestAction = null;
            this.CorrectHashReturnsSessionIdData.TestAction = CorrectHashReturnsSessionId_TestAction;
            // 
            // IncorrectHashReturnsErrorCodeData
            // 
            this.IncorrectHashReturnsErrorCodeData.PosttestAction = null;
            this.IncorrectHashReturnsErrorCodeData.PretestAction = null;
            this.IncorrectHashReturnsErrorCodeData.TestAction = IncorrectHashReturnsErrorCode_TestAction;
            // 
            // UnknownUserIdReturnsErrorCodeData
            // 
            this.UnknownUserIdReturnsErrorCodeData.PosttestAction = null;
            this.UnknownUserIdReturnsErrorCodeData.PretestAction = null;
            this.UnknownUserIdReturnsErrorCodeData.TestAction = UnknownUserIdReturnsErrorCode_TestAction;
            // 
            // scalarValueCondition2
            // 
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;
            // 
            // scalarValueCondition3
            // 
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        private DatabaseTestActions CorrectHashReturnsSessionIdData;
        private DatabaseTestActions IncorrectHashReturnsErrorCodeData;
        private DatabaseTestActions UnknownUserIdReturnsErrorCodeData;
    }
}
