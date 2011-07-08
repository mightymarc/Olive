// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnknownReturnCodeException.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the UnknownReturnCodeException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnknownReturnCodeException : Exception
    {
        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReturnCodeException" /> class.
        /// </summary>
        internal UnknownReturnCodeException()
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReturnCodeException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        internal UnknownReturnCodeException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReturnCodeException" /> class.
        /// </summary>
        /// <param name = "returnCode">The return code.</param>
        internal UnknownReturnCodeException(int returnCode)
            : base(string.Format(CultureInfo.CurrentCulture, "The return code {0} is unknown.", returnCode))
        {
            this.ReturnCode = returnCode;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReturnCodeException" /> class.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "inner">The inner.</param>
        internal UnknownReturnCodeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "UnknownReturnCodeException" /> class.
        /// </summary>
        /// <param name = "info">The <see cref = "T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name = "context">The <see cref = "T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref = "T:System.ArgumentNullException">The <paramref name = "info" /> parameter is null. </exception>
        /// <exception cref = "T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref = "P:System.Exception.HResult" /> is zero (0). </exception>
        protected UnknownReturnCodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///   Gets or sets the return code.
        /// </summary>
        /// <value>
        ///   The return code.
        /// </value>
        public int ReturnCode { get; set; }
    }
}