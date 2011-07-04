// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBase.cs" company="Olive">
//   [Copyright]
// </copyright>
// <summary>
//   Defines the TestBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class TestBase
    {
        protected IOliveContext GetServiceContext()
        {
            return new OliveContext(@"server=.\SQLEXPRESS;user=ServiceUser;password=temp;database=OliveTest;");
        }

        protected IOliveContext GetDbaContext()
        {
            return new OliveContext(@"server=.\SQLEXPRESS;database=OliveTest;Trusted_Connection=yes;");
        }
    }
}
