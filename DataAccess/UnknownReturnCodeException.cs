// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnknownReturnCodeException.cs" company="Olive">
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
//   Defines the UnknownReturnCodeException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// The unknown return code exception.
    /// </summary>
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
        /// Initializes a new instance of the <see cref="UnknownReturnCodeException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        internal UnknownReturnCodeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownReturnCodeException"/> class.
        /// </summary>
        /// <param name="returnCode">
        /// The return code.
        /// </param>
        internal UnknownReturnCodeException(int returnCode)
            : base(string.Format(CultureInfo.CurrentCulture, "The return code {0} is unknown.", returnCode))
        {
            this.ReturnCode = returnCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownReturnCodeException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        internal UnknownReturnCodeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownReturnCodeException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null. 
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). 
        /// </exception>
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