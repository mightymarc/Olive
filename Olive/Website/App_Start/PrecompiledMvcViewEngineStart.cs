using System.Web.Mvc;
using System.Web.WebPages;
using PrecompiledMvcViewEngine;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Olive.Website.App_Start.PrecompiledMvcViewEngineStart), "Start")]

namespace Olive.Website.App_Start {
    public static class PrecompiledMvcViewEngineStart {
        public static void Start() {
            var engine = new PrecompiledMvcEngine(typeof(PrecompiledMvcViewEngineStart).Assembly);

            ViewEngines.Engines.Insert(0, engine);

            // StartPage lookups are done by WebPages. 
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}
