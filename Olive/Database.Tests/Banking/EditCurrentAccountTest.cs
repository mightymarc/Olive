// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditCurrentAccountTest.cs" company="Olive">
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
//   The edit current account test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Database.Tests.Banking
{
    using System.ComponentModel;

    using Microsoft.Data.Schema.UnitTesting;
    using Microsoft.Data.Schema.UnitTesting.Conditions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// The edit current account test.
    /// </summary>
    [TestClass]
    public class EditCurrentAccountTest : DatabaseTestClass
    {
        #region Constants and Fields

        /// <summary>
        /// The returns error code when account does not exist data.
        /// </summary>
        private DatabaseTestActions ReturnsErrorCodeWhenAccountDoesNotExistData;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditCurrentAccountTest"/> class.
        /// </summary>
        public EditCurrentAccountTest()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The returns error code when account does not exist.
        /// </summary>
        [TestMethod]
        public void ReturnsErrorCodeWhenAccountDoesNotExist()
        {
            DatabaseTestActions testActions = this.ReturnsErrorCodeWhenAccountDoesNotExistData;

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
            DatabaseTestAction ReturnsErrorCodeWhenAccountDoesNotExist_TestAction;
            var resources = new ComponentResourceManager(typeof(EditCurrentAccountTest));
            ScalarValueCondition scalarValueCondition1;
            this.ReturnsErrorCodeWhenAccountDoesNotExistData = new DatabaseTestActions();
            ReturnsErrorCodeWhenAccountDoesNotExist_TestAction = new DatabaseTestAction();
            scalarValueCondition1 = new ScalarValueCondition();

            // ReturnsErrorCodeWhenAccountDoesNotExistData
            this.ReturnsErrorCodeWhenAccountDoesNotExistData.PosttestAction = null;
            this.ReturnsErrorCodeWhenAccountDoesNotExistData.PretestAction = null;
            this.ReturnsErrorCodeWhenAccountDoesNotExistData.TestAction =
                ReturnsErrorCodeWhenAccountDoesNotExist_TestAction;

            // ReturnsErrorCodeWhenAccountDoesNotExist_TestAction
            ReturnsErrorCodeWhenAccountDoesNotExist_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(
                ReturnsErrorCodeWhenAccountDoesNotExist_TestAction, "ReturnsErrorCodeWhenAccountDoesNotExist_TestAction");

            // scalarValueCondition1
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = "0";
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
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