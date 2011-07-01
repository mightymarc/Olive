using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Conditions;

namespace Olive.DataAccess.Tests
{
    [TestClass()]
    public class DbTest1 : DatabaseTestClass
    {

        public DbTest1()
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
        public void Banking_AccountWithBalance_Balance_MatchesAcountTransferSums()
        {
            DatabaseTestActions testActions = this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData;
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
        public void Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount()
        {
            DatabaseTestActions testActions = this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData;
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
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbTest1));
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction
            // 
            Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction, "Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction");
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
            // Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData
            // 
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData.PosttestAction = null;
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData.PretestAction = null;
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData.TestAction = Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction;
            // 
            // Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData
            // 
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData.PosttestAction = null;
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData.PretestAction = null;
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData.TestAction = Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction;
            // 
            // Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction
            // 
            Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction, "Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction");
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

        private DatabaseTestActions Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData;
        private DatabaseTestActions Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData;
    }
}
