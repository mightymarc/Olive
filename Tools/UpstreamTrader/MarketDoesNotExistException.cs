// -----------------------------------------------------------------------
// <copyright file="Mnee.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.UpstreamTrader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class MarketDoesNotExistException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public MarketDoesNotExistException()
        {
        }

        public MarketDoesNotExistException(string message)
            : base(message)
        {
        }

        public MarketDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MarketDoesNotExistException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
