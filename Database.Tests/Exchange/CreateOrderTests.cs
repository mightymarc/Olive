namespace Olive.Database.Tests.Exchange
{
    using System.ComponentModel;
    using System.Transactions;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CreateOrderTests : DatabaseTestClass
    {
        private DatabaseTestActions CreateOrderTest1Data;

        public CreateOrderTests()
        {
            this.InitializeComponent();
        }

        [TestMethod]
        public void CreateOrderTest1()
        {
            using (new TransactionScope())
            {
                var testActions = this.CreateOrderTest1Data;

                // Execute the pre-test script
                System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
                var pretestResults = TestService.Execute(
                    this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

                // Execute the test script
                System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
                ExecutionResult[] testResults = TestService.Execute(
                    this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

                // Execute the post-test script
                System.Diagnostics.Trace.WriteLineIf(
                    testActions.PosttestAction != null, "Executing post-test script...");
                ExecutionResult[] posttestResults = TestService.Execute(
                    this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
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

        /// <summary>
        ///   Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction CreateOrderTest1_PretestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateOrderTests));
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction CreateOrderTest1_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction CreateOrderTest1_PosttestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            this.CreateOrderTest1Data = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            CreateOrderTest1_PretestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            CreateOrderTest1_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            CreateOrderTest1_PosttestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            scalarValueCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.ScalarValueCondition();
            // 
            // CreateOrderTest1_PretestAction
            // 
            CreateOrderTest1_PretestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(CreateOrderTest1_PretestAction, "CreateOrderTest1_PretestAction");
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
            // CreateOrderTest1_TestAction
            // 
            CreateOrderTest1_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(CreateOrderTest1_TestAction, "CreateOrderTest1_TestAction");
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
            // CreateOrderTest1_PosttestAction
            // 
            CreateOrderTest1_PosttestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(CreateOrderTest1_PosttestAction, "CreateOrderTest1_PosttestAction");
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
            // CreateOrderTest1Data
            // 
            this.CreateOrderTest1Data.PosttestAction = CreateOrderTest1_PosttestAction;
            this.CreateOrderTest1Data.PretestAction = CreateOrderTest1_PretestAction;
            this.CreateOrderTest1Data.TestAction = CreateOrderTest1_TestAction;
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