﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Olive.Website.Views.User
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/User/Register.cshtml")]
    public class Register : System.Web.Mvc.WebViewPage<Olive.Website.ViewModels.User.RegisterViewModel>
    {
        public Register()
        {
        }
        public override void Execute()
        {

WriteLiteral("\r\n");


            
            #line 3 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
  
    ViewBag.Title = "Register";


            
            #line default
            #line hidden
WriteLiteral("\r\n<h2>");


            
            #line 7 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n<p>\r\n    Use the form below to become a user. \r\n</p>\r\n\r\n");


            
            #line 12 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
 using (Html.BeginForm()) 
{
    
            
            #line default
            #line hidden
            
            #line 14 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
Write(Html.ValidationSummary(true, "User creation was unsuccessful. Please correct the errors and try again."));

            
            #line default
            #line hidden
            
            #line 14 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
                                                                                                             

            
            #line default
            #line hidden
WriteLiteral("    <div>\r\n        <fieldset>\r\n            <legend>Account Information</legend>\r\n" +
"            <p>\r\n               ");


            
            #line 19 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.LabelFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n               ");


            
            #line 20 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.TextBoxFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n               ");


            
            #line 21 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.ValidationMessageFor(m => m.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </p>\r\n            <p>\r\n               ");


            
            #line 24 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.LabelFor(m => m.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n               ");


            
            #line 25 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.PasswordFor(m => m.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n               ");


            
            #line 26 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.ValidationMessageFor(m => m.Password));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </p>\r\n            <p>\r\n               ");


            
            #line 29 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.LabelFor(m => m.ConfirmPassword));

            
            #line default
            #line hidden
WriteLiteral("\r\n               ");


            
            #line 30 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.PasswordFor(m => m.ConfirmPassword));

            
            #line default
            #line hidden
WriteLiteral("\r\n               ");


            
            #line 31 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
          Write(Html.ValidationMessageFor(m => m.ConfirmPassword));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </p>\r\n            <p>\r\n                <input type=\"submit\" value=\"" +
"Register\" />\r\n            </p>\r\n        </fieldset>\r\n    </div>\r\n");


            
            #line 38 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\User\Register.cshtml"
}
            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
