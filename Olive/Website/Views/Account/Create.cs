﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Create.cs" company="Olive">
//   
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/Create.cshtml")]
    public class Create : System.Web.Mvc.WebViewPage<CreateViewModel>
    {
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
    }
}

#pragma warning restore 1591