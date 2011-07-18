using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Website.Helpers
{
    using Microsoft.Practices.Unity;

    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}