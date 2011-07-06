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
    public class VerifySessionTests : DatabaseTestClass
    {

        public VerifySessionTests()
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
        public void UnknownSessionId()
        {
            DatabaseTestActions testActions = this.UnknownSessionIdData;
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
        public void VerifyFailsOnExpiredSession()
        {
            DatabaseTestActions testActions = this.VerifyFailsOnExpiredSessionData;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifySessionTests));
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction UnknownSessionId_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction VerifyFailsOnExpiredSession_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            this.SuccessData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.UnknownSessionIdData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.VerifyFailsOnExpiredSessionData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            Success_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            UnknownSessionId_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            VerifyFailsOnExpiredSession_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // SuccessData
            // 
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = null;
            this.SuccessData.TestAction = Success_TestAction;
            // 
            // Success_TestAction
            // 
            Success_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");
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
            // UnknownSessionIdData
            // 
            this.UnknownSessionIdData.PosttestAction = null;
            this.UnknownSessionIdData.PretestAction = null;
            this.UnknownSessionIdData.TestAction = UnknownSessionId_TestAction;
            // 
            // UnknownSessionId_TestAction
            // 
            UnknownSessionId_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(UnknownSessionId_TestAction, "UnknownSessionId_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // VerifyFailsOnExpiredSessionData
            // 
            this.VerifyFailsOnExpiredSessionData.PosttestAction = null;
            this.VerifyFailsOnExpiredSessionData.PretestAction = null;
            this.VerifyFailsOnExpiredSessionData.TestAction = VerifyFailsOnExpiredSession_TestAction;
            // 
            // VerifyFailsOnExpiredSession_TestAction
            // 
            VerifyFailsOnExpiredSession_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(VerifyFailsOnExpiredSession_TestAction, "VerifyFailsOnExpiredSession_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
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
        private DatabaseTestActions UnknownSessionIdData;
        private DatabaseTestActions VerifyFailsOnExpiredSessionData;
    }
}
