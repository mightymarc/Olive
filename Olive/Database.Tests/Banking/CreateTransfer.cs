using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Conditions;

namespace Olive.Database.Tests.Banking
{
    [TestClass()]
    public class CreateTransfer : DatabaseTestClass
    {

        public CreateTransfer()
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
        public void FailsWithNegativeAmount()
        {
            DatabaseTestActions testActions = this.FailsWithNegativeAmountData;
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
        public void FailsWithUnknownSourceAccount()
        {
            DatabaseTestActions testActions = this.FailsWithUnknownSourceAccountData;
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
        public void FailsWithUnknownDestAccount()
        {
            DatabaseTestActions testActions = this.FailsWithUnknownDestAccountData;
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
        public void FailsWithDifferentAccountCurrencies()
        {
            DatabaseTestActions testActions = this.FailsWithDifferentAccountCurrenciesData;
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
        public void FailsWithNullDescription()
        {
            DatabaseTestActions testActions = this.FailsWithNullDescriptionData;
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
        public void FailsWithNullSourceAccountId()
        {
            DatabaseTestActions testActions = this.FailsWithNullSourceAccountIdData;
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
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNegativeAmount_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTransfer));
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction Success_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition6;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownSourceAccount_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition5;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownDestAccount_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition4;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithDifferentAccountCurrencies_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNullDescription_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithDifferentAccountCurrencies_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNegativeAmount_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNullDescription_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownDestAccount_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownSourceAccount_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction Success_PretestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownSourceAccount_PosttestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownDestAccount_PosttestAction;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNullSourceAccountId_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition7;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithDifferentAccountCurrencies_PosttestAction;
            this.FailsWithNegativeAmountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.SuccessData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithUnknownSourceAccountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithUnknownDestAccountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithDifferentAccountCurrenciesData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithNullDescriptionData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithNullSourceAccountIdData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            FailsWithNegativeAmount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            Success_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition6 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            FailsWithUnknownSourceAccount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition5 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            FailsWithUnknownDestAccount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition4 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            FailsWithDifferentAccountCurrencies_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            FailsWithNullDescription_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            FailsWithDifferentAccountCurrencies_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithNegativeAmount_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithNullDescription_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithUnknownDestAccount_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithUnknownSourceAccount_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            Success_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithUnknownSourceAccount_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithUnknownDestAccount_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            FailsWithNullSourceAccountId_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition7 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            FailsWithDifferentAccountCurrencies_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            // 
            // FailsWithNegativeAmount_TestAction
            // 
            FailsWithNegativeAmount_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(FailsWithNegativeAmount_TestAction, "FailsWithNegativeAmount_TestAction");
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
            // Success_TestAction
            // 
            Success_TestAction.Conditions.Add(scalarValueCondition6);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");
            // 
            // scalarValueCondition6
            // 
            scalarValueCondition6.ColumnNumber = 1;
            scalarValueCondition6.Enabled = true;
            scalarValueCondition6.ExpectedValue = "0";
            scalarValueCondition6.Name = "scalarValueCondition6";
            scalarValueCondition6.NullExpected = false;
            scalarValueCondition6.ResultSet = 1;
            scalarValueCondition6.RowNumber = 1;
            // 
            // FailsWithUnknownSourceAccount_TestAction
            // 
            FailsWithUnknownSourceAccount_TestAction.Conditions.Add(scalarValueCondition5);
            resources.ApplyResources(FailsWithUnknownSourceAccount_TestAction, "FailsWithUnknownSourceAccount_TestAction");
            // 
            // scalarValueCondition5
            // 
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "0";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;
            // 
            // FailsWithUnknownDestAccount_TestAction
            // 
            FailsWithUnknownDestAccount_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(FailsWithUnknownDestAccount_TestAction, "FailsWithUnknownDestAccount_TestAction");
            // 
            // scalarValueCondition4
            // 
            scalarValueCondition4.ColumnNumber = 1;
            scalarValueCondition4.Enabled = true;
            scalarValueCondition4.ExpectedValue = "0";
            scalarValueCondition4.Name = "scalarValueCondition4";
            scalarValueCondition4.NullExpected = false;
            scalarValueCondition4.ResultSet = 1;
            scalarValueCondition4.RowNumber = 1;
            // 
            // FailsWithDifferentAccountCurrencies_TestAction
            // 
            FailsWithDifferentAccountCurrencies_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(FailsWithDifferentAccountCurrencies_TestAction, "FailsWithDifferentAccountCurrencies_TestAction");
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
            // FailsWithNullDescription_TestAction
            // 
            FailsWithNullDescription_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(FailsWithNullDescription_TestAction, "FailsWithNullDescription_TestAction");
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
            // 
            // FailsWithDifferentAccountCurrencies_PretestAction
            // 
            resources.ApplyResources(FailsWithDifferentAccountCurrencies_PretestAction, "FailsWithDifferentAccountCurrencies_PretestAction");
            // 
            // FailsWithNegativeAmount_PretestAction
            // 
            resources.ApplyResources(FailsWithNegativeAmount_PretestAction, "FailsWithNegativeAmount_PretestAction");
            // 
            // FailsWithNullDescription_PretestAction
            // 
            resources.ApplyResources(FailsWithNullDescription_PretestAction, "FailsWithNullDescription_PretestAction");
            // 
            // FailsWithUnknownDestAccount_PretestAction
            // 
            resources.ApplyResources(FailsWithUnknownDestAccount_PretestAction, "FailsWithUnknownDestAccount_PretestAction");
            // 
            // FailsWithUnknownSourceAccount_PretestAction
            // 
            resources.ApplyResources(FailsWithUnknownSourceAccount_PretestAction, "FailsWithUnknownSourceAccount_PretestAction");
            // 
            // Success_PretestAction
            // 
            resources.ApplyResources(Success_PretestAction, "Success_PretestAction");
            // 
            // FailsWithUnknownSourceAccount_PosttestAction
            // 
            resources.ApplyResources(FailsWithUnknownSourceAccount_PosttestAction, "FailsWithUnknownSourceAccount_PosttestAction");
            // 
            // FailsWithUnknownDestAccount_PosttestAction
            // 
            resources.ApplyResources(FailsWithUnknownDestAccount_PosttestAction, "FailsWithUnknownDestAccount_PosttestAction");
            // 
            // FailsWithNullSourceAccountId_TestAction
            // 
            FailsWithNullSourceAccountId_TestAction.Conditions.Add(scalarValueCondition7);
            resources.ApplyResources(FailsWithNullSourceAccountId_TestAction, "FailsWithNullSourceAccountId_TestAction");
            // 
            // scalarValueCondition7
            // 
            scalarValueCondition7.ColumnNumber = 1;
            scalarValueCondition7.Enabled = true;
            scalarValueCondition7.ExpectedValue = "0";
            scalarValueCondition7.Name = "scalarValueCondition7";
            scalarValueCondition7.NullExpected = false;
            scalarValueCondition7.ResultSet = 1;
            scalarValueCondition7.RowNumber = 1;
            // 
            // FailsWithDifferentAccountCurrencies_PosttestAction
            // 
            resources.ApplyResources(FailsWithDifferentAccountCurrencies_PosttestAction, "FailsWithDifferentAccountCurrencies_PosttestAction");
            // 
            // FailsWithNegativeAmountData
            // 
            this.FailsWithNegativeAmountData.PosttestAction = null;
            this.FailsWithNegativeAmountData.PretestAction = FailsWithNegativeAmount_PretestAction;
            this.FailsWithNegativeAmountData.TestAction = FailsWithNegativeAmount_TestAction;
            // 
            // SuccessData
            // 
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = Success_PretestAction;
            this.SuccessData.TestAction = Success_TestAction;
            // 
            // FailsWithUnknownSourceAccountData
            // 
            this.FailsWithUnknownSourceAccountData.PosttestAction = FailsWithUnknownSourceAccount_PosttestAction;
            this.FailsWithUnknownSourceAccountData.PretestAction = FailsWithUnknownSourceAccount_PretestAction;
            this.FailsWithUnknownSourceAccountData.TestAction = FailsWithUnknownSourceAccount_TestAction;
            // 
            // FailsWithUnknownDestAccountData
            // 
            this.FailsWithUnknownDestAccountData.PosttestAction = FailsWithUnknownDestAccount_PosttestAction;
            this.FailsWithUnknownDestAccountData.PretestAction = FailsWithUnknownDestAccount_PretestAction;
            this.FailsWithUnknownDestAccountData.TestAction = FailsWithUnknownDestAccount_TestAction;
            // 
            // FailsWithDifferentAccountCurrenciesData
            // 
            this.FailsWithDifferentAccountCurrenciesData.PosttestAction = FailsWithDifferentAccountCurrencies_PosttestAction;
            this.FailsWithDifferentAccountCurrenciesData.PretestAction = FailsWithDifferentAccountCurrencies_PretestAction;
            this.FailsWithDifferentAccountCurrenciesData.TestAction = FailsWithDifferentAccountCurrencies_TestAction;
            // 
            // FailsWithNullDescriptionData
            // 
            this.FailsWithNullDescriptionData.PosttestAction = null;
            this.FailsWithNullDescriptionData.PretestAction = FailsWithNullDescription_PretestAction;
            this.FailsWithNullDescriptionData.TestAction = FailsWithNullDescription_TestAction;
            // 
            // FailsWithNullSourceAccountIdData
            // 
            this.FailsWithNullSourceAccountIdData.PosttestAction = null;
            this.FailsWithNullSourceAccountIdData.PretestAction = null;
            this.FailsWithNullSourceAccountIdData.TestAction = FailsWithNullSourceAccountId_TestAction;
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

        private DatabaseTestActions FailsWithNegativeAmountData;
        private DatabaseTestActions SuccessData;
        private DatabaseTestActions FailsWithUnknownSourceAccountData;
        private DatabaseTestActions FailsWithUnknownDestAccountData;
        private DatabaseTestActions FailsWithDifferentAccountCurrenciesData;
        private DatabaseTestActions FailsWithNullDescriptionData;
        private DatabaseTestActions FailsWithNullSourceAccountIdData;
    }
}
