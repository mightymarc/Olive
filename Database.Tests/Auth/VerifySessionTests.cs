// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerifySessionTests.cs" company="Olive">
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
//   Defines the VerifySessionTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.Auth
{
    using System.ComponentModel;
    using System.Transactions;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The verify session tests.
    /// </summary>
    [TestClass]
    public class VerifySessionTests : DatabaseTestClass
    {
        /// <summary>
        ///   The success data.
        /// </summary>
        private DatabaseTestActions SuccessData;

        /// <summary>
        ///   The unknown session id data.
        /// </summary>
        private DatabaseTestActions UnknownSessionIdData;

        /// <summary>
        ///   The verify fails on expired session data.
        /// </summary>
        private DatabaseTestActions VerifyFailsOnExpiredSessionData;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "VerifySessionTests" /> class.
        /// </summary>
        public VerifySessionTests()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The success.
        /// </summary>
        [TestMethod]
        public void Success()
        {
            using (new TransactionScope())
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
                System.Diagnostics.Trace.WriteLineIf(
                    testActions.PosttestAction != null, "Executing post-test script...");
                ExecutionResult[] posttestResults = TestService.Execute(
                    this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
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
        /// The unknown session id.
        /// </summary>
        [TestMethod]
        public void UnknownSessionId()
        {
            using (new TransactionScope())
            {
                DatabaseTestActions testActions = this.UnknownSessionIdData;

                // Execute the pre-test script
                System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
                ExecutionResult[] pretestResults = TestService.Execute(
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

        /// <summary>
        /// The verify fails on expired session.
        /// </summary>
        [TestMethod]
        public void VerifyFailsOnExpiredSession()
        {
            using (new TransactionScope())
            {
                DatabaseTestActions testActions = this.VerifyFailsOnExpiredSessionData;

                // Execute the pre-test script
                System.Diagnostics.Trace.WriteLineIf(testActions.PretestAction != null, "Executing pre-test script...");
                ExecutionResult[] pretestResults = TestService.Execute(
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

        /// <summary>
        /// Required method for Designer support - do not modify 
        ///   the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DatabaseTestAction Success_TestAction;
            var resources = new ComponentResourceManager(typeof(VerifySessionTests));
            ScalarValueCondition scalarValueCondition1;
            DatabaseTestAction UnknownSessionId_TestAction;
            DatabaseTestAction VerifyFailsOnExpiredSession_TestAction;
            ScalarValueCondition scalarValueCondition2;
            DatabaseTestAction VerifyFailsOnExpiredSession_PretestAction;
            DatabaseTestAction VerifyFailsOnExpiredSession_PosttestAction;
            ScalarValueCondition scalarValueCondition3;
            this.SuccessData = new DatabaseTestActions();
            this.UnknownSessionIdData = new DatabaseTestActions();
            this.VerifyFailsOnExpiredSessionData = new DatabaseTestActions();
            Success_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();
            UnknownSessionId_TestAction = new DatabaseTestAction();
            VerifyFailsOnExpiredSession_TestAction = new DatabaseTestAction();
            scalarValueCondition2 = new ScalarValueCondition();
            VerifyFailsOnExpiredSession_PretestAction = new DatabaseTestAction();
            VerifyFailsOnExpiredSession_PosttestAction = new DatabaseTestAction();
            scalarValueCondition3 = new ScalarValueCondition();

            // Success_TestAction
            Success_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(Success_TestAction, "Success_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;

            // UnknownSessionId_TestAction
            UnknownSessionId_TestAction.Conditions.Add(scalarValueCondition2);
            resources.ApplyResources(UnknownSessionId_TestAction, "UnknownSessionId_TestAction");

            // VerifyFailsOnExpiredSession_TestAction
            VerifyFailsOnExpiredSession_TestAction.Conditions.Add(scalarValueCondition3);
            resources.ApplyResources(VerifyFailsOnExpiredSession_TestAction, "VerifyFailsOnExpiredSession_TestAction");

            // SuccessData
            this.SuccessData.PosttestAction = null;
            this.SuccessData.PretestAction = null;
            this.SuccessData.TestAction = Success_TestAction;

            // UnknownSessionIdData
            this.UnknownSessionIdData.PosttestAction = null;
            this.UnknownSessionIdData.PretestAction = null;
            this.UnknownSessionIdData.TestAction = UnknownSessionId_TestAction;

            // VerifyFailsOnExpiredSessionData
            this.VerifyFailsOnExpiredSessionData.PosttestAction = VerifyFailsOnExpiredSession_PosttestAction;
            this.VerifyFailsOnExpiredSessionData.PretestAction = VerifyFailsOnExpiredSession_PretestAction;
            this.VerifyFailsOnExpiredSessionData.TestAction = VerifyFailsOnExpiredSession_TestAction;

            // scalarValueCondition2
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "0";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;

            // VerifyFailsOnExpiredSession_PretestAction
            resources.ApplyResources(
                VerifyFailsOnExpiredSession_PretestAction, "VerifyFailsOnExpiredSession_PretestAction");

            // VerifyFailsOnExpiredSession_PosttestAction
            resources.ApplyResources(
                VerifyFailsOnExpiredSession_PosttestAction, "VerifyFailsOnExpiredSession_PosttestAction");

            // scalarValueCondition3
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "0";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
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