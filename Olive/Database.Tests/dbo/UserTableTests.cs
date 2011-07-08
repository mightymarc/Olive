// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserTableTests.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the UserTableTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.dbo
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserTableTests : DatabaseTestClass
    {
        private DatabaseTestActions AllowedToUpdatePasswordHashData;

        private DatabaseTestActions InsertSuccessData;

        private DatabaseTestActions InsertWithDuplicateEmailFailsData;

        private DatabaseTestActions InsertWithNullEmailFailsData;

        private DatabaseTestActions InsertWithNullPasswordHashFailsData;

        private DatabaseTestActions InsertWithNullPasswordSaltFailsData;

        private DatabaseTestActions NotAllowedToDeleteData;

        private DatabaseTestActions NotAllowedToUpdateEmailData;

        private DatabaseTestActions NotAllowedToUpdatePasswordSaltData;

        public UserTableTests()
        {
            this.InitializeComponent();
        }

        [TestMethod]
        public void AllowedToUpdatePasswordHash()
        {
            DatabaseTestActions testActions = this.AllowedToUpdatePasswordHashData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void InsertSuccess()
        {
            DatabaseTestActions testActions = this.InsertSuccessData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void InsertWithDuplicateEmailFails()
        {
            DatabaseTestActions testActions = this.InsertWithDuplicateEmailFailsData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void InsertWithNullEmailFails()
        {
            DatabaseTestActions testActions = this.InsertWithNullEmailFailsData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void InsertWithNullPasswordHashFails()
        {
            DatabaseTestActions testActions = this.InsertWithNullPasswordHashFailsData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void InsertWithNullPasswordSaltFails()
        {
            DatabaseTestActions testActions = this.InsertWithNullPasswordSaltFailsData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void NotAllowedToDelete()
        {
            DatabaseTestActions testActions = this.NotAllowedToDeleteData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void NotAllowedToUpdateEmail()
        {
            DatabaseTestActions testActions = this.NotAllowedToUpdateEmailData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
        }

        [TestMethod]
        public void NotAllowedToUpdatePasswordSalt()
        {
            DatabaseTestActions testActions = this.NotAllowedToUpdatePasswordSaltData;

            // Execute the pre-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
            ExecutionResult[] pretestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);

            // Execute the test script
            System.Diagnostics.Trace.WriteLineIf(testActions.TestAction != null, "Executing test script...");
            ExecutionResult[] testResults = TestService.Execute(
                this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);

            // Execute the post-test script
            System.Diagnostics.Trace.WriteLineIf(testActions.PosttestAction != null, "Executing post-test script...");
            ExecutionResult[] posttestResults = TestService.Execute(
                this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
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
            DatabaseTestAction InsertSuccess_TestAction;
            var resources = new ComponentResourceManager(typeof(UserTableTests));
            ScalarValueCondition scalarValueCondition3;
            DatabaseTestAction InsertWithNullEmailFails_TestAction;
            ScalarValueCondition scalarValueCondition5;
            DatabaseTestAction InsertWithNullPasswordHashFails_TestAction;
            ScalarValueCondition scalarValueCondition6;
            DatabaseTestAction InsertWithNullPasswordSaltFails_TestAction;
            DatabaseTestAction InsertWithDuplicateEmailFails_TestAction;
            ScalarValueCondition scalarValueCondition4;
            DatabaseTestAction NotAllowedToDelete_TestAction;
            DatabaseTestAction NotAllowedToUpdateEmail_TestAction;
            DatabaseTestAction AllowedToUpdatePasswordHash_TestAction;
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction NotAllowedToUpdatePasswordSalt_TestAction;
            DatabaseTestAction AllowedToUpdatePasswordHash_PretestAction;
            DatabaseTestAction AllowedToUpdatePasswordHash_PosttestAction;
            ScalarValueCondition scalarValueCondition2;
            DatabaseTestAction InsertWithDuplicateEmailFails_PretestAction;
            ScalarValueCondition scalarValueCondition7;
            ScalarValueCondition scalarValueCondition8;
            ScalarValueCondition scalarValueCondition9;
            ScalarValueCondition scalarValueCondition10;
            this.InsertSuccessData = new DatabaseTestActions();
            this.InsertWithNullEmailFailsData = new DatabaseTestActions();
            this.InsertWithNullPasswordHashFailsData = new DatabaseTestActions();
            this.InsertWithNullPasswordSaltFailsData = new DatabaseTestActions();
            this.InsertWithDuplicateEmailFailsData = new DatabaseTestActions();
            this.NotAllowedToDeleteData = new DatabaseTestActions();
            this.NotAllowedToUpdateEmailData = new DatabaseTestActions();
            this.AllowedToUpdatePasswordHashData = new DatabaseTestActions();
            this.NotAllowedToUpdatePasswordSaltData = new DatabaseTestActions();
            InsertSuccess_TestAction = new DatabaseTestAction();
            scalarValueCondition3 = new ScalarValueCondition();
            InsertWithNullEmailFails_TestAction = new DatabaseTestAction();
            scalarValueCondition5 = new ScalarValueCondition();
            InsertWithNullPasswordHashFails_TestAction = new DatabaseTestAction();
            scalarValueCondition6 = new ScalarValueCondition();
            InsertWithNullPasswordSaltFails_TestAction = new DatabaseTestAction();
            InsertWithDuplicateEmailFails_TestAction = new DatabaseTestAction();
            scalarValueCondition4 = new ScalarValueCondition();
            NotAllowedToDelete_TestAction = new DatabaseTestAction();
            NotAllowedToUpdateEmail_TestAction = new DatabaseTestAction();
            AllowedToUpdatePasswordHash_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            NotAllowedToUpdatePasswordSalt_TestAction = new DatabaseTestAction();
            AllowedToUpdatePasswordHash_PretestAction = new DatabaseTestAction();
            AllowedToUpdatePasswordHash_PosttestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();
            InsertWithDuplicateEmailFails_PretestAction = new DatabaseTestAction();
            scalarValueCondition7 = new ScalarValueCondition();
            scalarValueCondition8 = new ScalarValueCondition();
            scalarValueCondition9 = new ScalarValueCondition();
            scalarValueCondition10 = new ScalarValueCondition();

            // InsertSuccess_TestAction
            InsertSuccess_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(InsertSuccess_TestAction, "InsertSuccess_TestAction");

            // scalarValueCondition3
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;

            // InsertWithNullEmailFails_TestAction
            InsertWithNullEmailFails_TestAction.Conditions.Add(scalarValueCondition5);
            resources.ApplyResources(InsertWithNullEmailFails_TestAction, "InsertWithNullEmailFails_TestAction");

            // scalarValueCondition5
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "0";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;

            // InsertWithNullPasswordHashFails_TestAction
            InsertWithNullPasswordHashFails_TestAction.Conditions.Add(scalarValueCondition6);
            resources.ApplyResources(
                InsertWithNullPasswordHashFails_TestAction, "InsertWithNullPasswordHashFails_TestAction");

            // scalarValueCondition6
            scalarValueCondition6.ColumnNumber = 1;
            scalarValueCondition6.Enabled = true;
            scalarValueCondition6.ExpectedValue = "0";
            scalarValueCondition6.Name = "scalarValueCondition6";
            scalarValueCondition6.NullExpected = false;
            scalarValueCondition6.ResultSet = 1;
            scalarValueCondition6.RowNumber = 1;

            // InsertWithNullPasswordSaltFails_TestAction
            InsertWithNullPasswordSaltFails_TestAction.Conditions.Add(scalarValueCondition7);
            resources.ApplyResources(
                InsertWithNullPasswordSaltFails_TestAction, "InsertWithNullPasswordSaltFails_TestAction");

            // InsertWithDuplicateEmailFails_TestAction
            InsertWithDuplicateEmailFails_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(
                InsertWithDuplicateEmailFails_TestAction, "InsertWithDuplicateEmailFails_TestAction");

            // scalarValueCondition4
            scalarValueCondition4.ColumnNumber = 1;
            scalarValueCondition4.Enabled = true;
            scalarValueCondition4.ExpectedValue = "0";
            scalarValueCondition4.Name = "scalarValueCondition4";
            scalarValueCondition4.NullExpected = false;
            scalarValueCondition4.ResultSet = 1;
            scalarValueCondition4.RowNumber = 1;

            // NotAllowedToDelete_TestAction
            NotAllowedToDelete_TestAction.Conditions.Add(scalarValueCondition8);
            resources.ApplyResources(NotAllowedToDelete_TestAction, "NotAllowedToDelete_TestAction");

            // NotAllowedToUpdateEmail_TestAction
            NotAllowedToUpdateEmail_TestAction.Conditions.Add(scalarValueCondition9);
            resources.ApplyResources(NotAllowedToUpdateEmail_TestAction, "NotAllowedToUpdateEmail_TestAction");

            // AllowedToUpdatePasswordHash_TestAction
            AllowedToUpdatePasswordHash_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(AllowedToUpdatePasswordHash_TestAction, "AllowedToUpdatePasswordHash_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // NotAllowedToUpdatePasswordSalt_TestAction
            NotAllowedToUpdatePasswordSalt_TestAction.Conditions.Add(scalarValueCondition10);
            resources.ApplyResources(
                NotAllowedToUpdatePasswordSalt_TestAction, "NotAllowedToUpdatePasswordSalt_TestAction");

            // AllowedToUpdatePasswordHash_PretestAction
            resources.ApplyResources(
                AllowedToUpdatePasswordHash_PretestAction, "AllowedToUpdatePasswordHash_PretestAction");

            // AllowedToUpdatePasswordHash_PosttestAction
            AllowedToUpdatePasswordHash_PosttestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(
                AllowedToUpdatePasswordHash_PosttestAction, "AllowedToUpdatePasswordHash_PosttestAction");

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;

            // InsertWithDuplicateEmailFails_PretestAction
            resources.ApplyResources(
                InsertWithDuplicateEmailFails_PretestAction, "InsertWithDuplicateEmailFails_PretestAction");

            // InsertSuccessData
            this.InsertSuccessData.PosttestAction = null;
            this.InsertSuccessData.PretestAction = null;
            this.InsertSuccessData.TestAction = InsertSuccess_TestAction;

            // InsertWithNullEmailFailsData
            this.InsertWithNullEmailFailsData.PosttestAction = null;
            this.InsertWithNullEmailFailsData.PretestAction = null;
            this.InsertWithNullEmailFailsData.TestAction = InsertWithNullEmailFails_TestAction;

            // InsertWithNullPasswordHashFailsData
            this.InsertWithNullPasswordHashFailsData.PosttestAction = null;
            this.InsertWithNullPasswordHashFailsData.PretestAction = null;
            this.InsertWithNullPasswordHashFailsData.TestAction = InsertWithNullPasswordHashFails_TestAction;

            // InsertWithNullPasswordSaltFailsData
            this.InsertWithNullPasswordSaltFailsData.PosttestAction = null;
            this.InsertWithNullPasswordSaltFailsData.PretestAction = null;
            this.InsertWithNullPasswordSaltFailsData.TestAction = InsertWithNullPasswordSaltFails_TestAction;

            // InsertWithDuplicateEmailFailsData
            this.InsertWithDuplicateEmailFailsData.PosttestAction = null;
            this.InsertWithDuplicateEmailFailsData.PretestAction = InsertWithDuplicateEmailFails_PretestAction;
            this.InsertWithDuplicateEmailFailsData.TestAction = InsertWithDuplicateEmailFails_TestAction;

            // NotAllowedToDeleteData
            this.NotAllowedToDeleteData.PosttestAction = null;
            this.NotAllowedToDeleteData.PretestAction = null;
            this.NotAllowedToDeleteData.TestAction = NotAllowedToDelete_TestAction;

            // NotAllowedToUpdateEmailData
            this.NotAllowedToUpdateEmailData.PosttestAction = null;
            this.NotAllowedToUpdateEmailData.PretestAction = null;
            this.NotAllowedToUpdateEmailData.TestAction = NotAllowedToUpdateEmail_TestAction;

            // AllowedToUpdatePasswordHashData
            this.AllowedToUpdatePasswordHashData.PosttestAction = AllowedToUpdatePasswordHash_PosttestAction;
            this.AllowedToUpdatePasswordHashData.PretestAction = AllowedToUpdatePasswordHash_PretestAction;
            this.AllowedToUpdatePasswordHashData.TestAction = AllowedToUpdatePasswordHash_TestAction;

            // NotAllowedToUpdatePasswordSaltData
            this.NotAllowedToUpdatePasswordSaltData.PosttestAction = null;
            this.NotAllowedToUpdatePasswordSaltData.PretestAction = null;
            this.NotAllowedToUpdatePasswordSaltData.TestAction = NotAllowedToUpdatePasswordSalt_TestAction;

            // scalarValueCondition7
            scalarValueCondition7.ColumnNumber = 1;
            scalarValueCondition7.Enabled = true;
            scalarValueCondition7.ExpectedValue = "0";
            scalarValueCondition7.Name = "scalarValueCondition7";
            scalarValueCondition7.NullExpected = false;
            scalarValueCondition7.ResultSet = 1;
            scalarValueCondition7.RowNumber = 1;

            // scalarValueCondition8
            scalarValueCondition8.ColumnNumber = 1;
            scalarValueCondition8.Enabled = true;
            scalarValueCondition8.ExpectedValue = "0";
            scalarValueCondition8.Name = "scalarValueCondition8";
            scalarValueCondition8.NullExpected = false;
            scalarValueCondition8.ResultSet = 1;
            scalarValueCondition8.RowNumber = 1;

            // scalarValueCondition9
            scalarValueCondition9.ColumnNumber = 1;
            scalarValueCondition9.Enabled = true;
            scalarValueCondition9.ExpectedValue = "0";
            scalarValueCondition9.Name = "scalarValueCondition9";
            scalarValueCondition9.NullExpected = false;
            scalarValueCondition9.ResultSet = 1;
            scalarValueCondition9.RowNumber = 1;

            // scalarValueCondition10
            scalarValueCondition10.ColumnNumber = 1;
            scalarValueCondition10.Enabled = true;
            scalarValueCondition10.ExpectedValue = "0";
            scalarValueCondition10.Name = "scalarValueCondition10";
            scalarValueCondition10.NullExpected = false;
            scalarValueCondition10.ResultSet = 1;
            scalarValueCondition10.RowNumber = 1;
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