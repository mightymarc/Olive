using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.Helpers
{
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;

    public class UnityControllerFactory : IControllerFactory
    {
        #region IControllerFactory Members

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var containerAccessor =
                requestContext.HttpContext.ApplicationInstance as IContainerAccessor;

            var currentAssembly = Assembly.GetExecutingAssembly();
            var controllerTypes = from t in currentAssembly.GetTypes()
                                  where t.Name.Contains(controllerName + "Controller")
                                  select t;

            if (controllerTypes.Count() > 0)
            {
                var controllerType = controllerTypes.Single();
                var controller = (IController)containerAccessor.Container.Resolve(controllerType, controllerName);
                return controller;
            }

            return null;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            controller = null;
        }

        #endregion
    }

}