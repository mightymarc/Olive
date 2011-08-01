// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Olive">
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
//   Defines the MvcApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website
{
    using System;
    using System.Configuration;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    using Olive.DataAccess;
    using Olive.Services;
    using Olive.Website.Helpers;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// The mvc application.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication, IContainerAccessor
    {
        private static IUnityContainer container;

        public static IUnityContainer Container
        {
            get { return container; }
        }

        IUnityContainer IContainerAccessor.Container
        {
            get { return Container; }
        }


        /// <summary>
        /// The register global filters.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
            routes.MapRoute(
                "Account_Edit", 
                "{controller}/{action}/{AccountId}", 
                new { controller = "Account", action = "Edit", AccountId = UrlParameter.Optional });

            routes.MapRoute(
                "Exchange_CreateOrder",
                "{controller}/{action}",
                new { controller = "Exchange", action = "CreateOrder" });

            routes.MapRoute(
                "Account_Transfer",
                "{controller}/{action}/{FromAccountId}",
                new { controller = "Account", action = "Transfer", SourceAccountId = UrlParameter.Optional });

            routes.MapRoute(
                "Account_Withdraw",
                "{controller}/{action}/{FromAccountId}",
                new { controller = "Account", action = "Withdraw", SourceAccountId = UrlParameter.Optional });
        }

        /// <summary>
        /// The application_ start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            if (Container == null)
            {
                container = CreateUnityContainer();
            }

            ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
            ////DependencyResolver.SetResolver(new UnityDependencyResolver(this.Container));
        }

        /// <summary>
        /// The get unity container.
        /// </summary>
        private static IUnityContainer CreateUnityContainer()
        {
            var container = new UnityContainer().LoadConfiguration();

            // Bypass service proxy for local testing
#if Dev
            container.RegisterInstance<IFaultFactory>(new FaultFactory());
            container.RegisterType<IOliveContext, OliveContext>();

            var clientService = new ClientService();
            container.BuildUp(clientService);
            container.RegisterInstance<IClientService>(clientService);
#else
            // Register the channel factory in code for now, because I don't know how to
            // register generic types in configuration files.
            container.RegisterType<IChannelFactory<IClientService>, ChannelFactory<IClientService>>(new ContainerControlledLifetimeManager(), new InjectionConstructor(string.Empty));

            // Register the service interface with a factory that creates it using the channel.
            container.RegisterType<IClientService>(new InjectionFactory(c => c.Resolve<ChannelFactory<IClientService>>().CreateChannel()));
#endif

            return container;
        }
    }
}