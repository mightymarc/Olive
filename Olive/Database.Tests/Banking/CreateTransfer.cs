// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateTransfer.cs" company="Olive">
//   Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    
//    1. Definitions
//    
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    
//    A "contribution" is the original software, or any additions or changes to the software.
//    
//    A "contributor" is any person that distributes its contribution under this license.
//    
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    
//    2. Grant of Rights
//    
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    
//    3. Conditions and Limitations
//    
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
// </copyright>
// <summary>
//   Defines the CreateTransfer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.Banking
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The create transfer.
    /// </summary>
    [TestClass]
    public class CreateTransfer : DatabaseTestClass
    {
        /// <summary>
        ///   The fails with different account currencies data.
        /// </summary>
        private DatabaseTestActions FailsWithDifferentAccountCurrenciesData;

        /// <summary>
        ///   The fails with negative amount data.
        /// </summary>
        private DatabaseTestActions FailsWithNegativeAmountData;

        /// <summary>
        ///   The fails with null description data.
        /// </summary>
        private DatabaseTestActions FailsWithNullDescriptionData;

        /// <summary>
        ///   The fails with null source account id data.
        /// </summary>
        private DatabaseTestActions FailsWithNullSourceAccountIdData;

        /// <summary>
        ///   The fails with unknown dest account data.
        /// </summary>
        private DatabaseTestActions FailsWithUnknownDestAccountData;

        /// <summary>
        ///   The fails with unknown source account data.
        /// </summary>
        private DatabaseTestActions FailsWithUnknownSourceAccountData;

        /// <summary>
        ///   The success data.
        /// </summary>
        private DatabaseTestActions SuccessData;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "CreateTransfer" /> class.
        /// </summary>
        public CreateTransfer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The fails with different account currencies.
        /// </summary>
        [TestMethod]
        public void FailsWithDifferentAccountCurrencies()
        {
            DatabaseTestActions testActions = this.FailsWithDifferentAccountCurrenciesData;

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

        /// <summary>
        /// The fails with negative amount.
        /// </summary>
        [TestMethod]
        public void FailsWithNegativeAmount()
        {
            DatabaseTestActions testActions = this.FailsWithNegativeAmountData;

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

        /// <summary>
        /// The fails with null description.
        /// </summary>
        [TestMethod]
        public void FailsWithNullDescription()
        {
            DatabaseTestActions testActions = this.FailsWithNullDescriptionData;

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

        /// <summary>
        /// The fails with null source account id.
        /// </summary>
        [TestMethod]
        public void FailsWithNullSourceAccountId()
        {
            DatabaseTestActions testActions = this.FailsWithNullSourceAccountIdData;

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

        /// <summary>
        /// The fails with unknown dest account.
        /// </summary>
        [TestMethod]
        public void FailsWithUnknownDestAccount()
        {
            DatabaseTestActions testActions = this.FailsWithUnknownDestAccountData;

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

        /// <summary>
        /// The fails with unknown source account.
        /// </summary>
        [TestMethod]
        public void FailsWithUnknownSourceAccount()
        {
            DatabaseTestActions testActions = this.FailsWithUnknownSourceAccountData;

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

        /// <summary>
        /// The success.
        /// </summary>
        [TestMethod]
        public void Success()
        {
            DatabaseTestActions testActions = this.SuccessData;

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

        /// <summary>
        /// The test cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            this.CleanupTest();
        }

        /// <summary>
        /// The test initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this.InitializeTest();
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DatabaseTestAction FailsWithNegativeAmount_TestAction;
            var resources = new ComponentResourceManager(typeof(CreateTransfer));
            ScalarValueCondition scalarValueCondition2;
            DatabaseTestAction Success_TestAction;
            ScalarValueCondition scalarValueCondition6;
            DatabaseTestAction FailsWithUnknownSourceAccount_TestAction;
            ScalarValueCondition scalarValueCondition5;
            DatabaseTestAction FailsWithUnknownDestAccount_TestAction;
            ScalarValueCondition scalarValueCondition4;
            DatabaseTestAction FailsWithDifferentAccountCurrencies_TestAction;
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction FailsWithNullDescription_TestAction;
            ScalarValueCondition scalarValueCondition3;
            DatabaseTestAction FailsWithDifferentAccountCurrencies_PretestAction;
            DatabaseTestAction FailsWithNegativeAmount_PretestAction;
            DatabaseTestAction FailsWithNullDescription_PretestAction;
            DatabaseTestAction FailsWithUnknownDestAccount_PretestAction;
            DatabaseTestAction FailsWithUnknownSourceAccount_PretestAction;
            DatabaseTestAction Success_PretestAction;
            DatabaseTestAction FailsWithUnknownSourceAccount_PosttestAction;
            DatabaseTestAction FailsWithUnknownDestAccount_PosttestAction;
            DatabaseTestAction FailsWithNullSourceAccountId_TestAction;
            ScalarValueCondition scalarValueCondition7;
            DatabaseTestAction FailsWithDifferentAccountCurrencies_PosttestAction;
            this.FailsWithNegativeAmountData = new DatabaseTestActions();
            this.SuccessData = new DatabaseTestActions();
            this.FailsWithUnknownSourceAccountData = new DatabaseTestActions();
            this.FailsWithUnknownDestAccountData = new DatabaseTestActions();
            this.FailsWithDifferentAccountCurrenciesData = new DatabaseTestActions();
            this.FailsWithNullDescriptionData = new DatabaseTestActions();
            this.FailsWithNullSourceAccountIdData = new DatabaseTestActions();
            FailsWithNegativeAmount_TestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();
            Success_TestAction = new DatabaseTestAction();
            scalarValueCondition6 = new ScalarValueCondition();
            FailsWithUnknownSourceAccount_TestAction = new DatabaseTestAction();
            scalarValueCondition5 = new ScalarValueCondition();
            FailsWithUnknownDestAccount_TestAction = new DatabaseTestAction();
            scalarValueCondition4 = new ScalarValueCondition();
            FailsWithDifferentAccountCurrencies_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            FailsWithNullDescription_TestAction = new DatabaseTestAction();
            scalarValueCondition3 = new ScalarValueCondition();
            FailsWithDifferentAccountCurrencies_PretestAction = new DatabaseTestAction();
            FailsWithNegativeAmount_PretestAction = new DatabaseTestAction();
            FailsWithNullDescription_PretestAction = new DatabaseTestAction();
            FailsWithUnknownDestAccount_PretestAction = new DatabaseTestAction();
            FailsWithUnknownSourceAccount_PretestAction = new DatabaseTestAction();
            Success_PretestAction = new DatabaseTestAction();
            FailsWithUnknownSourceAccount_PosttestAction = new DatabaseTestAction();
            FailsWithUnknownDestAccount_PosttestAction = new DatabaseTestAction();
            FailsWithNullSourceAccountId_TestAction = new DatabaseTestAction();
            scalarValueCondition7 = new ScalarValueCondition();
            FailsWithDifferentAccountCurrencies_PosttestAction = new DatabaseTestAction();

            // FailsWithNegativeAmount_TestAction
            FailsWithNegativeAmount_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(FailsWithNegativeAmount_TestAction, "FailsWithNegativeAmount_TestAction");

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;

            // Success_TestAction
            Success_TestAction.Conditions.Add(scalarValueCondition6);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");

            // scalarValueCondition6
            scalarValueCondition6.ColumnNumber = 1;
            scalarValueCondition6.Enabled = true;
            scalarValueCondition6.ExpectedValue = "0";
            scalarValueCondition6.Name = "scalarValueCondition6";
            scalarValueCondition6.NullExpected = false;
            scalarValueCondition6.ResultSet = 1;
            scalarValueCondition6.RowNumber = 1;

            // FailsWithUnknownSourceAccount_TestAction
            FailsWithUnknownSourceAccount_TestAction.Conditions.Add(scalarValueCondition5);
            resources.ApplyResources(
                FailsWithUnknownSourceAccount_TestAction, "FailsWithUnknownSourceAccount_TestAction");

            // scalarValueCondition5
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "0";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;

            // FailsWithUnknownDestAccount_TestAction
            FailsWithUnknownDestAccount_TestAction.Conditions.Add(scalarValueCondition4);
            resources.ApplyResources(FailsWithUnknownDestAccount_TestAction, "FailsWithUnknownDestAccount_TestAction");

            // scalarValueCondition4
            scalarValueCondition4.ColumnNumber = 1;
            scalarValueCondition4.Enabled = true;
            scalarValueCondition4.ExpectedValue = "0";
            scalarValueCondition4.Name = "scalarValueCondition4";
            scalarValueCondition4.NullExpected = false;
            scalarValueCondition4.ResultSet = 1;
            scalarValueCondition4.RowNumber = 1;

            // FailsWithDifferentAccountCurrencies_TestAction
            FailsWithDifferentAccountCurrencies_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(
                FailsWithDifferentAccountCurrencies_TestAction, "FailsWithDifferentAccountCurrencies_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // FailsWithNullDescription_TestAction
            FailsWithNullDescription_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(FailsWithNullDescription_TestAction, "FailsWithNullDescription_TestAction");

            // scalarValueCondition3
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;

            // FailsWithDifferentAccountCurrencies_PretestAction
            resources.ApplyResources(
                FailsWithDifferentAccountCurrencies_PretestAction, "FailsWithDifferentAccountCurrencies_PretestAction");

            // FailsWithNegativeAmount_PretestAction
            resources.ApplyResources(FailsWithNegativeAmount_PretestAction, "FailsWithNegativeAmount_PretestAction");

            // FailsWithNullDescription_PretestAction
            resources.ApplyResources(FailsWithNullDescription_PretestAction, "FailsWithNullDescription_PretestAction");

            // FailsWithUnknownDestAccount_PretestAction
            resources.ApplyResources(
                FailsWithUnknownDestAccount_PretestAction, "FailsWithUnknownDestAccount_PretestAction");

            // FailsWithUnknownSourceAccount_PretestAction
            resources.ApplyResources(
                FailsWithUnknownSourceAccount_PretestAction, "FailsWithUnknownSourceAccount_PretestAction");

            // Success_PretestAction
            resources.ApplyResources(Success_PretestAction, "Success_PretestAction");

            // FailsWithUnknownSourceAccount_PosttestAction
            resources.ApplyResources(
                FailsWithUnknownSourceAccount_PosttestAction, "FailsWithUnknownSourceAccount_PosttestAction");

            // FailsWithUnknownDestAccount_PosttestAction
            resources.ApplyResources(
                FailsWithUnknownDestAccount_PosttestAction, "FailsWithUnknownDestAccount_PosttestAction");

            // FailsWithNullSourceAccountId_TestAction
            FailsWithNullSourceAccountId_TestAction.Conditions.Add(scalarValueCondition7);
            resources.ApplyResources(FailsWithNullSourceAccountId_TestAction, "FailsWithNullSourceAccountId_TestAction");

            // scalarValueCondition7
            scalarValueCondition7.ColumnNumber = 1;
            scalarValueCondition7.Enabled = true;
            scalarValueCondition7.ExpectedValue = "0";
            scalarValueCondition7.Name = "scalarValueCondition7";
            scalarValueCondition7.NullExpected = false;
            scalarValueCondition7.ResultSet = 1;
            scalarValueCondition7.RowNumber = 1;

            // FailsWithDifferentAccountCurrencies_PosttestAction
            resources.ApplyResources(
                FailsWithDifferentAccountCurrencies_PosttestAction, "FailsWithDifferentAccountCurrencies_PosttestAction");

            // FailsWithNegativeAmountData
            this.FailsWithNegativeAmountData.PosttestAction = null;
            this.FailsWithNegativeAmountData.PretestAction = FailsWithNegativeAmount_PretestAction;
            this.FailsWithNegativeAmountData.TestAction = FailsWithNegativeAmount_TestAction;

            // SuccessData
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = Success_PretestAction;
            this.SuccessData.TestAction = Success_TestAction;

            // FailsWithUnknownSourceAccountData
            this.FailsWithUnknownSourceAccountData.PosttestAction = FailsWithUnknownSourceAccount_PosttestAction;
            this.FailsWithUnknownSourceAccountData.PretestAction = FailsWithUnknownSourceAccount_PretestAction;
            this.FailsWithUnknownSourceAccountData.TestAction = FailsWithUnknownSourceAccount_TestAction;

            // FailsWithUnknownDestAccountData
            this.FailsWithUnknownDestAccountData.PosttestAction = FailsWithUnknownDestAccount_PosttestAction;
            this.FailsWithUnknownDestAccountData.PretestAction = FailsWithUnknownDestAccount_PretestAction;
            this.FailsWithUnknownDestAccountData.TestAction = FailsWithUnknownDestAccount_TestAction;

            // FailsWithDifferentAccountCurrenciesData
            this.FailsWithDifferentAccountCurrenciesData.PosttestAction =
                FailsWithDifferentAccountCurrencies_PosttestAction;
            this.FailsWithDifferentAccountCurrenciesData.PretestAction =
                FailsWithDifferentAccountCurrencies_PretestAction;
            this.FailsWithDifferentAccountCurrenciesData.TestAction = FailsWithDifferentAccountCurrencies_TestAction;

            // FailsWithNullDescriptionData
            this.FailsWithNullDescriptionData.PosttestAction = null;
            this.FailsWithNullDescriptionData.PretestAction = FailsWithNullDescription_PretestAction;
            this.FailsWithNullDescriptionData.TestAction = FailsWithNullDescription_TestAction;

            // FailsWithNullSourceAccountIdData
            this.FailsWithNullSourceAccountIdData.PosttestAction = null;
            this.FailsWithNullSourceAccountIdData.PretestAction = null;
            this.FailsWithNullSourceAccountIdData.TestAction = FailsWithNullSourceAccountId_TestAction;
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