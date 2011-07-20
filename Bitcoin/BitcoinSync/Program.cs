// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Olive">
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
//   Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Bitcoin.BitcoinSync
{
    using System;
    using System.Net;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Threading;

    using log4net;
    using log4net.Config;

    using Microsoft.Practices.Unity;
    using Microsoft.Practices.Unity.Configuration;

    using Olive.Bitcoin.BitcoinSync.Properties;
    using Olive.DataAccess;
    using Olive.Services;

    public class Program
    {
        private ILog logger;

        private readonly BitcoinSyncSettings settings = new BitcoinSyncSettings();

        private IUnityContainer container;

        static void Main(string[] args)
        {
            new Program();
        }

        public Program()
        {
            Console.SetBufferSize(200, 500);
            ////Console.SetWindowSize(200, 70);

            XmlConfigurator.Configure();
            this.logger = LogManager.GetLogger(typeof(Program));

            this.container = new UnityContainer().LoadConfiguration();

            this.container.RegisterInstance(this.logger);
            this.container.RegisterInstance<ICrypto>(new Crypto());
            this.container.RegisterInstance<IFaultFactory>(new FaultFactory());

#if Dev
            var clientService = new ClientService();
            this.container.BuildUp(clientService);
#else
            var clientService = new ChannelFactory<IClientService>("OliveService").CreateChannel();
#endif

            this.container.RegisterInstance(clientService);

            this.container.RegisterType<IOliveContext, OliveContext>();
            this.container.RegisterInstance<IBitcoinService>(clientService);

            var rpcCredential = new NetworkCredential(
                this.settings.BitcoinDaemonUsername, this.settings.BitcoinDaemonPassword);
            this.container.RegisterInstance<IRpcClient>(new RpcClient
                {
                    Credential = rpcCredential, 
#if Dev
                    Hostname = "localhost",
#else
                    Hostname = "oapp1.olive.local",
#endif
                    PortNumber = this.settings.BitconDaemonPort
                });

            Console.WriteLine("Bitcoin Sync");
            Console.WriteLine();

            var sessionId = clientService.CreateSession(this.settings.ServiceEmail, this.settings.ServicePassword);

            while (true)
            {
#if !Dev
            try
            {
#endif
                this.ProcessIncomingTransactions(sessionId);
                this.GenerateReceiveAddresses(sessionId);
#if !Dev
            }
            catch (Exception e)
            {
                this.logger.Error("Unhandled exception: ", e);   
            }
#endif

                Thread.Sleep(5 * 1000);
            }

            Console.ReadLine();
        }

        private void GenerateReceiveAddresses(Guid sessionId)
        {
            var generator = new ReceiveAddressGenerator();
            this.container.BuildUp(generator);

            generator.Process(sessionId);
        }

        private void ProcessIncomingTransactions(Guid sessionId)
        {
            var processor = new IncomingTransactionProcessor();
            this.container.BuildUp(processor);

            processor.Process(sessionId);
        }
    }
}
