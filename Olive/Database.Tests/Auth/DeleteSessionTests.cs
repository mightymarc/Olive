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
    public class DeleteSessionTests : DatabaseTestClass
    {

        public DeleteSessionTests()
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
        public void Success()
        {
            DatabaseTestActions testActions = this.SuccessData;
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
        public void FailsOnExpiredSession()
        {
            DatabaseTestActions testActions = this.FailsOnExpiredSessionData;
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
        public void FailsOnUnknownSessionId()
        {
            DatabaseTestActions testActions = this.FailsOnUnknownSessionIdData;
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
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction Success_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteSessionTests));
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsOnExpiredSession_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsOnUnknownSessionId_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsOnExpiredSession_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsOnExpiredSession_PosttestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            this.SuccessData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsOnExpiredSessionData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsOnUnknownSessionIdData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            Success_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsOnExpiredSession_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsOnUnknownSessionId_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsOnExpiredSession_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsOnExpiredSession_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // Success_TestAction
            // 
            Success_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // FailsOnExpiredSession_TestAction
            // 
            FailsOnExpiredSession_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(FailsOnExpiredSession_TestAction, "FailsOnExpiredSession_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // FailsOnUnknownSessionId_TestAction
            // 
            FailsOnUnknownSessionId_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(FailsOnUnknownSessionId_TestAction, "FailsOnUnknownSessionId_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // SuccessData
            // 
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = null;
            this.SuccessData.TestAction = Success_TestAction;
            // 
            // FailsOnExpiredSessionData
            // 
            this.FailsOnExpiredSessionData.PosttestAction = FailsOnExpiredSession_PosttestAction;
            this.FailsOnExpiredSessionData.PretestAction = FailsOnExpiredSession_PretestAction;
            this.FailsOnExpiredSessionData.TestAction = FailsOnExpiredSession_TestAction;
            // 
            // FailsOnUnknownSessionIdData
            // 
            this.FailsOnUnknownSessionIdData.PosttestAction = null;
            this.FailsOnUnknownSessionIdData.PretestAction = null;
            this.FailsOnUnknownSessionIdData.TestAction = FailsOnUnknownSessionId_TestAction;
            // 
            // FailsOnExpiredSession_PretestAction
            // 
            FailsOnExpiredSession_PretestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(FailsOnExpiredSession_PretestAction, "FailsOnExpiredSession_PretestAction");
            // 
            // FailsOnExpiredSession_PosttestAction
            // 
            resources.ApplyResources(FailsOnExpiredSession_PosttestAction, "FailsOnExpiredSession_PosttestAction");
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

        private DatabaseTestActions SuccessData;
        private DatabaseTestActions FailsOnExpiredSessionData;
        private DatabaseTestActions FailsOnUnknownSessionIdData;
    }
}
