﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Create.cs" company="Olive">
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
//   Defines the Create type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Olive.Website.Views.Account
{
    using System.Web.Mvc.Html;

    using Olive.Website.ViewModels.Account;

    /// <summary>
    /// The create.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/Create.cshtml")]
    public class Create : System.Web.Mvc.WebViewPage<CreateViewModel>
    {
        #region Public Methods

        /// <summary>
        /// The execute.
        /// </summary>
        public override void Execute()
        {
            this.WriteLiteral("\r\n");

#line 3 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Create.cshtml"
            using (this.Html.BeginForm())
            {
#line default
#line hidden

#line 5 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Create.cshtml"
                this.Write(this.Html.ValidationSummary(true, "Please correct the errors and try again."));

#line default
#line hidden

#line 5 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Create.cshtml"

#line default
#line hidden
                this.WriteLiteral("   <fieldset>\r\n       <div>\r\n           ");

#line 8 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Create.cshtml"
                this.Write(this.Html.EditorForModel());

#line default
#line hidden
                this.WriteLiteral(
                    "\r\n           <p>\r\n               <input type=\"submit\" value=\"Save\" />\r\n          "
                    + " </p>\r\n       </div>\r\n   </fieldset>\r\n");

#line 14 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Create.cshtml"
            }

#line default
#line hidden
        }

        #endregion
    }
}

#pragma warning restore 1591