// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnityDependencyResolver.cs" company="">
//   
// </copyright>
// <summary>
//   The unity dependency resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Website.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Microsoft.Practices.Unity;

    /// <summary>
    /// The unity dependency resolver.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        /// <summary>
        ///   The container.
        /// </summary>
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Resolves singly registered services that support arbitrary object creation.
        /// </summary>
        /// <param name="serviceType">
        /// The type of the requested service or object.
        /// </param>
        /// <returns>
        /// The requested service or object.
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
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">
        /// The type of the requested services.
        /// </param>
        /// <returns>
        /// The requested services.
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