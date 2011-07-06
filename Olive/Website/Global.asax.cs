namespace Olive.Website
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;
    using Olive.Services;
    using Olive.Website.Controllers;
    using Olive.Website.Helpers;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var container = this.GetUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));    
        }

        private IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IWebService, WebService>();
            container.RegisterType<ISiteSession, SiteSession>();
            container.RegisterType<ICrypto, Crypto>();
            container.RegisterInstance<IOliveContext>(new OliveContext());

////            container.RegisterType<UserController>();

            return container;
        }
    }

    public class UnityDependencyResolver : IDependencyResolver
    {
        #region Members

        private IUnityContainer _container;

        #endregion

        #region Ctor

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        #endregion

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            try
            {
                var x =  _container.Resolve(serviceType);
                //_container.BuildUp(x);
                return x;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                return new List<object>();
            }
        }

        #endregion
    }
}
