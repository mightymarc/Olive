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

namespace Olive.Website.Views.Exchange.DisplayTemplates
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
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
    using Olive.Services;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.1.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Exchange/DisplayTemplates/MarketAndPrices.cshtml")]
    public class MarketAndPrices : System.Web.Mvc.WebViewPage<Tuple<GetMarketResponse, GetMarketPricesResponse>>
    {
        public MarketAndPrices()
        {
        }
        public override void Execute()
        {


WriteLiteral("<h3>");


            
            #line 3 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
Write(Model.Item1.FromCurrencyId);

            
            #line default
            #line hidden
WriteLiteral(" to ");


            
            #line 3 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
                              Write(Model.Item1.ToCurrencyId);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n\r\n<h4>Best prices</h4>\r\n<p><a href=\"/Exchange/CreateOrder?marketId=");


            
            #line 6 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
                                      Write(Model.Item1.MarketId);

            
            #line default
            #line hidden
WriteLiteral("\">Create order</a></p>\r\n\r\n<ul>\r\n");


            
            #line 9 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
     foreach (var price in Model.Item2.Prices)
    {

            
            #line default
            #line hidden
WriteLiteral("        <li>\r\n            <span>");


            
            #line 12 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
             Write(price.Price);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n            <span>(");


            
            #line 13 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
              Write(price.Volume);

            
            #line default
            #line hidden
WriteLiteral(")</span>\r\n            <a href=\"/Exchange/CreateOrder?marketId=");


            
            #line 14 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
                                               Write(Model.Item1.MarketId);

            
            #line default
            #line hidden
WriteLiteral("&price=");


            
            #line 14 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
                                                                           Write(price.Price);

            
            #line default
            #line hidden
WriteLiteral("\">Create order</a>\r\n        </li>\r\n");


            
            #line 16 "C:\Users\Andy\AppData\Local\Git\Olive\Website\Views\Exchange\DisplayTemplates\MarketAndPrices.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</ul>\r\n");


        }
    }
}
#pragma warning restore 1591
