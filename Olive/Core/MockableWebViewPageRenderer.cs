// -----------------------------------------------------------------------
// <copyright file="WebViewPageExtensionsEx.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.WebPages;

    using HtmlAgilityPack;

    using Moq;

    public class MockWebViewPageRenderer
    {
        public virtual HttpContextBase CreateHttpContext()
        {
            var context = new Mock<HttpContextBase> { CallBase = true };

            var request = new Mock<HttpRequestBase>() { CallBase = true };
            request.Setup(r => r.IsLocal).Returns(false);
            request.Setup(r => r.ApplicationPath).Returns("/");
            request.Setup(r => r.ServerVariables).Returns(new NameValueCollection());
            request.Setup(r => r.RawUrl).Returns(string.Empty);
            context.SetupGet(c => c.Request).Returns(request.Object);

            var response = new Mock<HttpResponseBase>() { CallBase = true };
            response.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            context.SetupGet(c => c.Response).Returns(response.Object);

            var hashTable = new Hashtable();
            context.SetupGet(c => c.Items).Returns(hashTable);

            return context.Object;
        }

        public virtual IView CreateView(string viewName = null)
        {
            var view = new Mock<IView>();
            view.Setup(v => v.Render(It.IsAny<ViewContext>(), It.IsAny<TextWriter>())).Callback<ViewContext, TextWriter>
                ((vc, tv) => tv.WriteLine("/* " + viewName + "*/"));

            return view.Object;
        }

        public virtual IViewEngine CreateViewEngine()
        {
            var viewEngine = new Mock<IViewEngine>();

            viewEngine.Setup(c => c.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns<ControllerContext, string, bool>((c, vn, uc) =>  new ViewEngineResult(this.CreateView(vn), viewEngine.Object));

            viewEngine.Setup(c => c.FindView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns<ControllerContext, string, string, bool>((c, vn, mn, uc) =>  new ViewEngineResult(this.CreateView(vn), viewEngine.Object));

            ////viewEngine.Setup(c => c.ReleaseView(It.IsAny<ControllerContext>(), It.IsAny<IView>()));

            return viewEngine.Object;
        }

        public virtual RouteData CreateRouteData()
        {
            return new RouteData();
        }

        public virtual ControllerBase CreateController()
        {
            return new Mock<ControllerBase>().Object;
        }

        public virtual TempDataDictionary CreateTempData()
        {
            return new TempDataDictionary();
        }

        public string RenderString<TModel>(WebViewPage<TModel> view, TModel model = default(TModel))
        {
            var httpContext = this.CreateHttpContext();
            var routeData = this.CreateRouteData();

            ////var requestContext = new RequestContext(httpContext, routeData);
            var controllerContext = new ControllerContext(httpContext, routeData, this.CreateController());

            view.ViewContext = new ViewContext(controllerContext, this.CreateView(), view.ViewData, this.CreateTempData(), new StringWriter());
            view.InitHelpers();

            view.ViewData.Model = model;

            view.ExecutePageHierarchy(
                new WebPageContext(view.ViewContext.HttpContext, page: null, model: null),
                view.ViewContext.Writer);

            return view.ViewContext.Writer.ToString();
        }

        public HtmlDocument RenderHtml<TModel>(WebViewPage<TModel> view, TModel model = default(TModel))
        {
            var raw = RenderString<TModel>(view, model);
            var html = new HtmlDocument();
            html.LoadHtml(raw);
            return html;
        }

        public static string RenderAsString<TModel>(WebViewPage<TModel> view, TModel model = default(TModel))
        {
            var instance = new MockWebViewPageRenderer();
            return RenderAsString(view, model);
        }
    }
}
