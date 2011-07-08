﻿using System;
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
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction VerifyFailsOnExpiredSession_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction VerifyFailsOnExpiredSession_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction VerifyFailsOnExpiredSession_PosttestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            this.SuccessData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.UnknownSessionIdData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.VerifyFailsOnExpiredSessionData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            Success_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            UnknownSessionId_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            VerifyFailsOnExpiredSession_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            VerifyFailsOnExpiredSession_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            VerifyFailsOnExpiredSession_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
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
            // UnknownSessionId_TestAction
            // 
            UnknownSessionId_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(UnknownSessionId_TestAction, "UnknownSessionId_TestAction");
            // 
            // VerifyFailsOnExpiredSession_TestAction
            // 
            VerifyFailsOnExpiredSession_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(VerifyFailsOnExpiredSession_TestAction, "VerifyFailsOnExpiredSession_TestAction");
            // 
            // SuccessData
            // 
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = null;
            this.SuccessData.TestAction = Success_TestAction;
            // 
            // UnknownSessionIdData
            // 
            this.UnknownSessionIdData.PosttestAction = null;
            this.UnknownSessionIdData.PretestAction = null;
            this.UnknownSessionIdData.TestAction = UnknownSessionId_TestAction;
            // 
            // VerifyFailsOnExpiredSessionData
            // 
            this.VerifyFailsOnExpiredSessionData.PosttestAction = VerifyFailsOnExpiredSession_PosttestAction;
            this.VerifyFailsOnExpiredSessionData.PretestAction = VerifyFailsOnExpiredSession_PretestAction;
            this.VerifyFailsOnExpiredSessionData.TestAction = VerifyFailsOnExpiredSession_TestAction;
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
            // VerifyFailsOnExpiredSession_PretestAction
            // 
            resources.ApplyResources(VerifyFailsOnExpiredSession_PretestAction, "VerifyFailsOnExpiredSession_PretestAction");
            // 
            // VerifyFailsOnExpiredSession_PosttestAction
            // 
            resources.ApplyResources(VerifyFailsOnExpiredSession_PosttestAction, "VerifyFailsOnExpiredSession_PosttestAction");
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

        private DatabaseTestActions SuccessData;
        private DatabaseTestActions UnknownSessionIdData;
        private DatabaseTestActions VerifyFailsOnExpiredSessionData;
    }
}