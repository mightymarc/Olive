// -----------------------------------------------------------------------
// <copyright file="OliveContextFactory.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class OliveContextFactory
    {
        internal static OliveContext GetServiceContext()
        {
            return new OliveContext(@"server=.\SQLEXPRESS;user=ServiceUser;password=temp;database=OliveTest;");
        }

        internal static OliveContext GetDbaContext()
        {
            return new OliveContext(@"server=.\SQLEXPRESS;database=OliveTest;Trusted_Connection=yes;");
        }
    }
}
