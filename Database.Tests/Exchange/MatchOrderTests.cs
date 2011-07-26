using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Conditions;

namespace Olive.Database.Tests.Exchange
{
    using System.Transactions;

    [TestClass()]
    public class MatchOrderTests : DatabaseTestClass
    {

        public MatchOrderTests()
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
        public void SuccessTest1()
        {
            using (new TransactionScope())
            {
                DatabaseTestActions testActions = this.SuccessTest1Data;
                // Execute the pre-test script
                // 
                System.Diagnostics.Trace.WriteLineIf(
                    (testActions.PretestAction != null), "Executing pre-test script...");
                ExecutionResult[] pretestResults = TestService.Execute(
                    this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                ExecutionResult[] testResults = TestService.Execute(
                    this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf(
                    (testActions.PosttestAction != null), "Executing post-test script...");
                ExecutionResult[] posttestResults = TestService.Execute(
                    this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction SuccessTest1_PretestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchOrderTests));
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction SuccessTest1_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction SuccessTest1_PosttestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            this.SuccessTest1Data = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            SuccessTest1_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            SuccessTest1_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            SuccessTest1_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // SuccessTest1_PretestAction
            // 
            SuccessTest1_PretestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(SuccessTest1_PretestAction, "SuccessTest1_PretestAction");
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
            // SuccessTest1_TestAction
            // 
            SuccessTest1_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(SuccessTest1_TestAction, "SuccessTest1_TestAction");
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
            // SuccessTest1_PosttestAction
            // 
            SuccessTest1_PosttestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(SuccessTest1_PosttestAction, "SuccessTest1_PosttestAction");
            // 
            // SuccessTest1Data
            // 
            this.SuccessTest1Data.PosttestAction = SuccessTest1_PosttestAction;
            this.SuccessTest1Data.PretestAction = SuccessTest1_PretestAction;
            this.SuccessTest1Data.TestAction = SuccessTest1_TestAction;
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

        private DatabaseTestActions SuccessTest1Data;
    }
}
