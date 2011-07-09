// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountWithBalance.cs" company="Olive">
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
//   Defines the DbTest1 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The db test 1.
    /// </summary>
    [TestClass]
    public class DbTest1 : DatabaseTestClass
    {
        #region Constants and Fields

        /// <summary>
        /// The banking_ account with balance_ balance_ matches acount transfer sums data.
        /// </summary>
        private DatabaseTestActions Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData;

        /// <summary>
        /// The banking_ account with balance_ row count_ equals account table row count data.
        /// </summary>
        private DatabaseTestActions Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DbTest1"/> class.
        /// </summary>
        public DbTest1()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The banking_ account with balance_ balance_ matches acount transfer sums.
        /// </summary>
        [TestMethod]
        public void Banking_AccountWithBalance_Balance_MatchesAcountTransferSums()
        {
            DatabaseTestActions testActions = this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData;

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
        /// The banking_ account with balance_ row count_ equals account table row count.
        /// </summary>
        [TestMethod]
        public void Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount()
        {
            DatabaseTestActions testActions = this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData;

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

        #endregion

        #region Methods

        /// <summary>
        /// Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DatabaseTestAction Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction;
            var resources = new ComponentResourceManager(typeof(DbTest1));
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction;
            ScalarValueCondition scalarValueCondition2;
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData = new DatabaseTestActions();
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData = new DatabaseTestActions();
            Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();

            // Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction
            Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction.Conditions.Add(
                scalarValueCondition1);
            resources.ApplyResources(
                Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction, 
                "Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData.PosttestAction = null;
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData.PretestAction = null;
            this.Banking_AccountWithBalance_Balance_MatchesAcountTransferSumsData.TestAction =
                Banking_AccountWithBalance_Balance_MatchesAcountTransferSums_TestAction;

            // Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData.PosttestAction = null;
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData.PretestAction = null;
            this.Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCountData.TestAction =
                Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction;

            // Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction
            Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction.Conditions.Add(
                scalarValueCondition2);
            resources.ApplyResources(
                Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction, 
                "Banking_AccountWithBalance_RowCount_EqualsAccountTableRowCount_TestAction");

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;
        }

        #endregion

        // You can use the following additional attributes as you write your tests:
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
    }
}