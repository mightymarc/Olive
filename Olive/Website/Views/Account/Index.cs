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

namespace Olive.Website.Views.Account
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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Account/Index.cshtml")]
    public class Index : System.Web.Mvc.WebViewPage<Olive.Website.ViewModels.Account.IndexViewModel>
    {
        public Index()
        {
        }
        public override void Execute()
        {

WriteLiteral("\r\n");


            
            #line 3 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
  
    ViewBag.Title = "Overview";


            
            #line default
            #line hidden
WriteLiteral("\r\n<h2>Overview</h2>\r\n\r\n<div>\r\n    <a href=\"/Account/Create\">Create account</a>\r\n<" +
"/div>\r\n\r\n<table>\r\n    <tr>\r\n        <th>#</th>\r\n        <th>Currency</th>\r\n     " +
"   <th>Balance</th>\r\n        <th>Name</th>\r\n    </tr>\r\n\r\n");


            
            #line 21 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
     foreach (var account in this.Model.Accounts)
    {

            
            #line default
            #line hidden
WriteLiteral("        <tr>\r\n            <td>");


            
            #line 24 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
           Write(account.AccountId);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            <td>");


            
            #line 25 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
           Write(account.CurrencyId);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            <td>");


            
            #line 26 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
           Write(account.Balance);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            <td>");


            
            #line 27 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
           Write(account.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n        </tr>\r\n");


            
            #line 29 "C:\Users\Andy\AppData\Local\TFS\Tests\Olive\Website\Views\Account\Index.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</table>\r\n");


        }
    }
}
#pragma warning restore 1591