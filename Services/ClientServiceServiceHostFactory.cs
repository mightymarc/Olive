using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Services
{
    using System.ServiceModel;
    using System.ServiceModel.Activation;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    public class ClientServiceServiceHostFactory : ServiceHostFactoryBase
    {
        /// <summary>
        /// When overridden in a derived class, creates a <see cref="T:System.ServiceModel.ServiceHostBase"/> with a specific base address using custom initiation data.
        /// </summary>
        /// <param name="constructorString">The initialization data that is passed to the <see cref="T:System.ServiceModel.ServiceHostBase"/> instance being constructed by the factory.</param>
        /// <param name="baseAddresses">An <see cref="T:System.Array"/> of type <see cref="T:System.Uri"/> that contains the base addresses of the host.</param>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.ServiceHostBase"/> object with the specified base addresses and initialized with the custom initiation data.
        /// </returns>
        public override ServiceHostBase CreateServiceHost(
            string constructorString,
            Uri[] baseAddresses)
        {
            var container = new UnityContainer().LoadConfiguration();
            var instance = new ClientService { Container = container };
            var serviceBusHost = new ServiceHost(instance, baseAddresses);
            return serviceBusHost;
        }
    }
}