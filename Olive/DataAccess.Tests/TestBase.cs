// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBase.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the TestBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    public abstract class TestBase
    {
        protected IOliveContext GetDbaContext()
        {
            return new OliveContext(@"server=.\SQLEXPRESS;database=OliveTest;Trusted_Connection=yes;");
        }

        protected IOliveContext GetServiceContext()
        {
            return new OliveContext(@"server=.\SQLEXPRESS;user=ServiceUser;password=temp;database=OliveTest;");
        }
    }
}