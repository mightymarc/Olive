// -----------------------------------------------------------------------
// <copyright file="MockInjectionFilterProvider.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Website.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public class MockInjectionFilterProvider : IFilterProvider
    {
        public Func<ActionExecutingContext, bool> Action { get; set; }

        public class ActionFilter : ActionFilterAttribute
        {
            private bool disabled;

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (this.disabled)
                {
                    return;
                }

                if (!this.Action(filterContext))
                {
                    this.disabled = true;
                }
            }

            public Func<ActionExecutingContext, bool> Action { get; set; }
        }

        public MockInjectionFilterProvider(Func<ActionExecutingContext, bool> action)
        {
            this.Action = action;
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            yield return new Filter(new ActionFilter { Action = this.Action }, FilterScope.Action, null);
        }
    }

}
