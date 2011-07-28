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
    using System.Transactions;

    [TestClass()]
    public class ChargeFeeTests : DatabaseTestClass
    {

        public ChargeFeeTests()
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
        public void DatabaseTest1()
        {
            using (new TransactionScope())
            {
                DatabaseTestActions testActions = this.DatabaseTest1Data;
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
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction DatabaseTest1_PretestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChargeFeeTests));
            this.DatabaseTest1Data = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            DatabaseTest1_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            // 
            // DatabaseTest1_PretestAction
            // 
            resources.ApplyResources(DatabaseTest1_PretestAction, "DatabaseTest1_PretestAction");
            // 
            // DatabaseTest1Data
            // 
            this.DatabaseTest1Data.PosttestAction = null;
            this.DatabaseTest1Data.PretestAction = DatabaseTest1_PretestAction;
            this.DatabaseTest1Data.TestAction = null;
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

        private DatabaseTestActions DatabaseTest1Data;
    }
}
