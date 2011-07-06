using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Data.Schema.UnitTesting;
using Microsoft.Data.Schema.UnitTesting.Conditions;

namespace Olive.Database.Tests.dbo
{
    [TestClass()]
    public class UserTableTests : DatabaseTestClass
    {

        public UserTableTests()
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
        public void InsertSuccess()
        {
            DatabaseTestActions testActions = this.InsertSuccessData;
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
        public void InsertWithNullEmailFails()
        {
            DatabaseTestActions testActions = this.InsertWithNullEmailFailsData;
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
        public void InsertWithNullPasswordHashFails()
        {
            DatabaseTestActions testActions = this.InsertWithNullPasswordHashFailsData;
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
        public void InsertWithPasswordSaltFails()
        {
            DatabaseTestActions testActions = this.InsertWithPasswordSaltFailsData;
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
        public void InsertWithDuplicateEmailFails()
        {
            DatabaseTestActions testActions = this.InsertWithDuplicateEmailFailsData;
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
        public void NotAllowedToDelete()
        {
            DatabaseTestActions testActions = this.NotAllowedToDeleteData;
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
        public void NotAllowedToUpdateEmail()
        {
            DatabaseTestActions testActions = this.NotAllowedToUpdateEmailData;
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
        public void AllowedToUpdatePasswordHash()
        {
            DatabaseTestActions testActions = this.AllowedToUpdatePasswordHashData;
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
        public void NotAllowedToUpdatePasswordSalt()
        {
            DatabaseTestActions testActions = this.NotAllowedToUpdatePasswordSaltData;
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
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction InsertSuccess_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserTableTests));
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction InsertWithNullEmailFails_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction InsertWithNullPasswordHashFails_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction InsertWithPasswordSaltFails_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction InsertWithDuplicateEmailFails_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition5;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction NotAllowedToDelete_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition6;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction NotAllowedToUpdateEmail_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition7;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction AllowedToUpdatePasswordHash_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition8;
            Microsoft.Data.Schema.UnitTesting.DatabaseTestAction NotAllowedToUpdatePasswordSalt_TestAction;
            Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition9;
            this.InsertSuccessData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.InsertWithNullEmailFailsData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.InsertWithNullPasswordHashFailsData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.InsertWithPasswordSaltFailsData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.InsertWithDuplicateEmailFailsData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.NotAllowedToDeleteData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.NotAllowedToUpdateEmailData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.AllowedToUpdatePasswordHashData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            this.NotAllowedToUpdatePasswordSaltData = new Microsoft.Data.Schema.UnitTesting.DatabaseTestActions();
            InsertSuccess_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            InsertWithNullEmailFails_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            InsertWithNullPasswordHashFails_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            InsertWithPasswordSaltFails_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            InsertWithDuplicateEmailFails_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition5 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            NotAllowedToDelete_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition6 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            NotAllowedToUpdateEmail_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition7 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            AllowedToUpdatePasswordHash_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition8 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            NotAllowedToUpdatePasswordSalt_TestAction = new Microsoft.Data.Schema.UnitTesting.DatabaseTestAction();
            inconclusiveCondition9 = new Microsoft.Data.Schema.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // InsertSuccessData
            // 
            this.InsertSuccessData.PosttestAction = null;
            this.InsertSuccessData.PretestAction = null;
            this.InsertSuccessData.TestAction = InsertSuccess_TestAction;
            // 
            // InsertSuccess_TestAction
            // 
            InsertSuccess_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(InsertSuccess_TestAction, "InsertSuccess_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // InsertWithNullEmailFailsData
            // 
            this.InsertWithNullEmailFailsData.PosttestAction = null;
            this.InsertWithNullEmailFailsData.PretestAction = null;
            this.InsertWithNullEmailFailsData.TestAction = InsertWithNullEmailFails_TestAction;
            // 
            // InsertWithNullEmailFails_TestAction
            // 
            InsertWithNullEmailFails_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(InsertWithNullEmailFails_TestAction, "InsertWithNullEmailFails_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // InsertWithNullPasswordHashFailsData
            // 
            this.InsertWithNullPasswordHashFailsData.PosttestAction = null;
            this.InsertWithNullPasswordHashFailsData.PretestAction = null;
            this.InsertWithNullPasswordHashFailsData.TestAction = InsertWithNullPasswordHashFails_TestAction;
            // 
            // InsertWithNullPasswordHashFails_TestAction
            // 
            InsertWithNullPasswordHashFails_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(InsertWithNullPasswordHashFails_TestAction, "InsertWithNullPasswordHashFails_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // InsertWithPasswordSaltFailsData
            // 
            this.InsertWithPasswordSaltFailsData.PosttestAction = null;
            this.InsertWithPasswordSaltFailsData.PretestAction = null;
            this.InsertWithPasswordSaltFailsData.TestAction = InsertWithPasswordSaltFails_TestAction;
            // 
            // InsertWithPasswordSaltFails_TestAction
            // 
            InsertWithPasswordSaltFails_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(InsertWithPasswordSaltFails_TestAction, "InsertWithPasswordSaltFails_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
            // 
            // InsertWithDuplicateEmailFailsData
            // 
            this.InsertWithDuplicateEmailFailsData.PosttestAction = null;
            this.InsertWithDuplicateEmailFailsData.PretestAction = null;
            this.InsertWithDuplicateEmailFailsData.TestAction = InsertWithDuplicateEmailFails_TestAction;
            // 
            // InsertWithDuplicateEmailFails_TestAction
            // 
            InsertWithDuplicateEmailFails_TestAction.Conditions.Add(inconclusiveCondition5);
            resources.ApplyResources(InsertWithDuplicateEmailFails_TestAction, "InsertWithDuplicateEmailFails_TestAction");
            // 
            // inconclusiveCondition5
            // 
            inconclusiveCondition5.Enabled = true;
            inconclusiveCondition5.Name = "inconclusiveCondition5";
            // 
            // NotAllowedToDeleteData
            // 
            this.NotAllowedToDeleteData.PosttestAction = null;
            this.NotAllowedToDeleteData.PretestAction = null;
            this.NotAllowedToDeleteData.TestAction = NotAllowedToDelete_TestAction;
            // 
            // NotAllowedToDelete_TestAction
            // 
            NotAllowedToDelete_TestAction.Conditions.Add(inconclusiveCondition6);
            resources.ApplyResources(NotAllowedToDelete_TestAction, "NotAllowedToDelete_TestAction");
            // 
            // inconclusiveCondition6
            // 
            inconclusiveCondition6.Enabled = true;
            inconclusiveCondition6.Name = "inconclusiveCondition6";
            // 
            // NotAllowedToUpdateEmailData
            // 
            this.NotAllowedToUpdateEmailData.PosttestAction = null;
            this.NotAllowedToUpdateEmailData.PretestAction = null;
            this.NotAllowedToUpdateEmailData.TestAction = NotAllowedToUpdateEmail_TestAction;
            // 
            // NotAllowedToUpdateEmail_TestAction
            // 
            NotAllowedToUpdateEmail_TestAction.Conditions.Add(inconclusiveCondition7);
            resources.ApplyResources(NotAllowedToUpdateEmail_TestAction, "NotAllowedToUpdateEmail_TestAction");
            // 
            // inconclusiveCondition7
            // 
            inconclusiveCondition7.Enabled = true;
            inconclusiveCondition7.Name = "inconclusiveCondition7";
            // 
            // AllowedToUpdatePasswordHashData
            // 
            this.AllowedToUpdatePasswordHashData.PosttestAction = null;
            this.AllowedToUpdatePasswordHashData.PretestAction = null;
            this.AllowedToUpdatePasswordHashData.TestAction = AllowedToUpdatePasswordHash_TestAction;
            // 
            // AllowedToUpdatePasswordHash_TestAction
            // 
            AllowedToUpdatePasswordHash_TestAction.Conditions.Add(inconclusiveCondition8);
            resources.ApplyResources(AllowedToUpdatePasswordHash_TestAction, "AllowedToUpdatePasswordHash_TestAction");
            // 
            // inconclusiveCondition8
            // 
            inconclusiveCondition8.Enabled = true;
            inconclusiveCondition8.Name = "inconclusiveCondition8";
            // 
            // NotAllowedToUpdatePasswordSaltData
            // 
            this.NotAllowedToUpdatePasswordSaltData.PosttestAction = null;
            this.NotAllowedToUpdatePasswordSaltData.PretestAction = null;
            this.NotAllowedToUpdatePasswordSaltData.TestAction = NotAllowedToUpdatePasswordSalt_TestAction;
            // 
            // NotAllowedToUpdatePasswordSalt_TestAction
            // 
            NotAllowedToUpdatePasswordSalt_TestAction.Conditions.Add(inconclusiveCondition9);
            resources.ApplyResources(NotAllowedToUpdatePasswordSalt_TestAction, "NotAllowedToUpdatePasswordSalt_TestAction");
            // 
            // inconclusiveCondition9
            // 
            inconclusiveCondition9.Enabled = true;
            inconclusiveCondition9.Name = "inconclusiveCondition9";
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

        private DatabaseTestActions InsertSuccessData;
        private DatabaseTestActions InsertWithNullEmailFailsData;
        private DatabaseTestActions InsertWithNullPasswordHashFailsData;
        private DatabaseTestActions InsertWithPasswordSaltFailsData;
        private DatabaseTestActions InsertWithDuplicateEmailFailsData;
        private DatabaseTestActions NotAllowedToDeleteData;
        private DatabaseTestActions NotAllowedToUpdateEmailData;
        private DatabaseTestActions AllowedToUpdatePasswordHashData;
        private DatabaseTestActions NotAllowedToUpdatePasswordSaltData;
    }
}
