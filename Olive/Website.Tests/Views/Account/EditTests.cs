// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EditTests.cs" company="Olive">
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
//   Defines the EditTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Tests.Views.Account
{
    using NUnit.Framework;

    using Olive.Website.ViewModels.Account;
    using Olive.Website.Views.Account;

    using PrecompiledMvcViews.Testing;

    /// <summary>
    /// The edit tests.
    /// </summary>
    [TestFixture]
    public class EditTests
    {
        /// <summary>
        /// The with view model renders without exceptions 1.
        /// </summary>
        [Test]
        public void WithViewModelRendersWithoutExceptions1()
        {
            var view = new Edit();

            var viewModel = new EditViewModel { AccountId = 123, DisplayName = null };

            var html = view.RenderAsHtml(viewModel);
        }

        /// <summary>
        /// The with view model renders without exceptions 2.
        /// </summary>
        [Test]
        public void WithViewModelRendersWithoutExceptions2()
        {
            var view = new Edit();

            var viewModel = new EditViewModel { AccountId = 123, DisplayName = "Quite Unique" };

            var html = view.RenderAsHtml(viewModel);
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//form"), "Form missing");
            Assert.IsNotNull(
                html.DocumentNode.SelectSingleNode("//input[@type='text' and @name='DisplayName']"), 
                "DisplayName textbox missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='submit']"), "Submit button missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//a[@href='/Account']"), "Cancel link missing");
        }

        /// <summary>
        /// The without view model renders without exceptions.
        /// </summary>
        [Test]
        public void WithoutViewModelRendersWithoutExceptions()
        {
            var view = new Edit();
            var viewModel = new EditViewModel { AccountId = 612345 };

            var html = view.RenderAsHtml(viewModel);

            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//form"), "Form missing");
            Assert.IsNotNull(
                html.DocumentNode.SelectSingleNode("//input[@type='text' and @name='DisplayName' and @value='']"), 
                "DisplayName textbox missing.");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//input[@type='submit']"), "Submit button missing");
            Assert.IsNotNull(html.DocumentNode.SelectSingleNode("//a[@href='/Account']"), "Cancel link missing");
        }
    }
}