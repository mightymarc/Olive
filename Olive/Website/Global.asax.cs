// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the MvcApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;

    using Olive.DataAccess;
    using Olive.Services;
    using Olive.Website.Helpers;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", 
                // Route name
                "{controller}/{action}/{id}", 
                // URL with parameters
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
            container.RegisterType<IOliveContext, OliveContext>();
            container.RegisterType<ICurrencyCache, CurrencyCache>();
            return container;
        }
    }

    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnityDependencyResolver" /> class.
        /// </summary>
        /// <param name = "container">The container.</param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        ///   Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name = "serviceType">The type of the requested service or object.</param>
        /// <returns>
        ///   The requested service or object.
        /// </returns>
        public object GetService(Type serviceType)
        {
            try
            {
                var x = this.container.Resolve(serviceType);
                return x;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///   Resolves multiply registered services.
        /// </summary>
        /// <param name = "serviceType">The type of the requested services.</param>
        /// <returns>
        ///   The requested services.
        /// </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}