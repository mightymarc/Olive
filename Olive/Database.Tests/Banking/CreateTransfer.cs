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






        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNegativeAmount_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTransfer));
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction Success_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownSourceAccount_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithUnknownDestAccount_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithDifferentAccountCurrencies_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition5;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction FailsWithNullDescription_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition6;
            this.FailsWithNegativeAmountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.SuccessData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithUnknownSourceAccountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithUnknownDestAccountData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithDifferentAccountCurrenciesData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.FailsWithNullDescriptionData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            FailsWithNegativeAmount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            Success_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsWithUnknownSourceAccount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsWithUnknownDestAccount_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsWithDifferentAccountCurrencies_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition5 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            FailsWithNullDescription_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition6 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // FailsWithNegativeAmountData
            // 
            this.FailsWithNegativeAmountData.PosttestAction = null;
            this.FailsWithNegativeAmountData.PretestAction = null;
            this.FailsWithNegativeAmountData.TestAction = FailsWithNegativeAmount_TestAction;
            // 
            // FailsWithNegativeAmount_TestAction
            // 
            FailsWithNegativeAmount_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(FailsWithNegativeAmount_TestAction, "FailsWithNegativeAmount_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // SuccessData
            // 
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = null;
            this.SuccessData.TestAction = Success_TestAction;
            // 
            // Success_TestAction
            // 
            Success_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // FailsWithUnknownSourceAccountData
            // 
            this.FailsWithUnknownSourceAccountData.PosttestAction = null;
            this.FailsWithUnknownSourceAccountData.PretestAction = null;
            this.FailsWithUnknownSourceAccountData.TestAction = FailsWithUnknownSourceAccount_TestAction;
            // 
            // FailsWithUnknownSourceAccount_TestAction
            // 
            FailsWithUnknownSourceAccount_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(FailsWithUnknownSourceAccount_TestAction, "FailsWithUnknownSourceAccount_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // FailsWithUnknownDestAccountData
            // 
            this.FailsWithUnknownDestAccountData.PosttestAction = null;
            this.FailsWithUnknownDestAccountData.PretestAction = null;
            this.FailsWithUnknownDestAccountData.TestAction = FailsWithUnknownDestAccount_TestAction;
            // 
            // FailsWithUnknownDestAccount_TestAction
            // 
            FailsWithUnknownDestAccount_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(FailsWithUnknownDestAccount_TestAction, "FailsWithUnknownDestAccount_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
            // 
            // FailsWithDifferentAccountCurrenciesData
            // 
            this.FailsWithDifferentAccountCurrenciesData.PosttestAction = null;
            this.FailsWithDifferentAccountCurrenciesData.PretestAction = null;
            this.FailsWithDifferentAccountCurrenciesData.TestAction = FailsWithDifferentAccountCurrencies_TestAction;
            // 
            // FailsWithDifferentAccountCurrencies_TestAction
            // 
            FailsWithDifferentAccountCurrencies_TestAction.Conditions.Add(inconclusiveCondition5);
            resources.ApplyResources(FailsWithDifferentAccountCurrencies_TestAction, "FailsWithDifferentAccountCurrencies_TestAction");
            // 
            // inconclusiveCondition5
            // 
            inconclusiveCondition5.Enabled = true;
            inconclusiveCondition5.Name = "inconclusiveCondition5";
            // 
            // FailsWithNullDescriptionData
            // 
            this.FailsWithNullDescriptionData.PosttestAction = null;
            this.FailsWithNullDescriptionData.PretestAction = null;
            this.FailsWithNullDescriptionData.TestAction = FailsWithNullDescription_TestAction;
            // 
            // FailsWithNullDescription_TestAction
            // 
            FailsWithNullDescription_TestAction.Conditions.Add(inconclusiveCondition6);
            resources.ApplyResources(FailsWithNullDescription_TestAction, "FailsWithNullDescription_TestAction");
            // 
            // inconclusiveCondition6
            // 
            inconclusiveCondition6.Enabled = true;
            inconclusiveCondition6.Name = "inconclusiveCondition6";
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
    }
}
