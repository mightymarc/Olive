// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultFactory.cs" company="Olive">
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
//   The fault factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System;
    using System.Globalization;
    using System.ServiceModel;

    /// <summary>
    /// The fault factory.
    /// </summary>
    public class FaultFactory : IFaultFactory
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FaultFactory"/> class.
        /// </summary>
        public FaultFactory()
        {
            this.UnauthorizedAccountEditFaultCode = new FaultCode("UnauthorizedAccountEdit");
            this.UnrecognizedCredentialsFaultCode = new FaultCode("UnrecognizedCredentials");
            this.EmailAlreadyRegisteredFaultCode = new FaultCode("EmailAlreadyRegistered");
            this.UnauthorizedAccountWithdrawFaultCode = new FaultCode("UnauthorizedAccountWithdraw");
            this.SessionDoesNotExistFaultCode = new FaultCode("SessionDoesNotExist");
            this.UnauthorizedAccountAccessFaultCode = new FaultCode("UnauthorizedAccountAccess");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets EmailAlreadyRegisteredFaultCode.
        /// </summary>
        public FaultCode EmailAlreadyRegisteredFaultCode { get; protected set; }

        /// <summary>
        /// Gets or sets SessionDoesNotExistFaultCode.
        /// </summary>
        public FaultCode SessionDoesNotExistFaultCode { get; protected set; }

        /// <summary>
        /// Gets or sets UnauthorizedAccountAccessFaultCode.
        /// </summary>
        public FaultCode UnauthorizedAccountAccessFaultCode { get; protected set; }

        /// <summary>
        /// Gets or sets UnauthorizedAccountEditFaultCode.
        /// </summary>
        public FaultCode UnauthorizedAccountEditFaultCode { get; protected set; }

        /// <summary>
        /// Gets or sets UnauthorizedAccountWithdrawFaultCode.
        /// </summary>
        public FaultCode UnauthorizedAccountWithdrawFaultCode { get; protected set; }

        /// <summary>
        /// Gets or sets UnrecognizedCredentialsFaultCode.
        /// </summary>
        public FaultCode UnrecognizedCredentialsFaultCode { get; protected set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The create email already registered fault exception.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        public virtual FaultException CreateEmailAlreadyRegisteredFaultException(string email)
        {
            const string ReasonFormat = "The specified e-mail, {0}, is already registered to another user.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, email)), 
                this.EmailAlreadyRegisteredFaultCode);
        }

        /// <summary>
        /// The create session does not exist fault exception.
        /// </summary>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        public virtual FaultException CreateSessionDoesNotExistFaultException(Guid sessionId)
        {
            const string ReasonFormat = "The specified session, {0}, does not exist or has expired.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, sessionId)), 
                this.SessionDoesNotExistFaultCode);
        }

        /// <summary>
        /// The create unauthorized account access fault exception.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        public FaultException CreateUnauthorizedAccountAccessFaultException(int userId, int accountId)
        {
            const string ReasonFormat = "User #{0} does not have access to view account #{1}.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, userId, accountId)), 
                this.UnauthorizedAccountAccessFaultCode);
        }

        /// <summary>
        /// The create unauthorized account edit fault exception.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        public virtual FaultException CreateUnauthorizedAccountEditFaultException(int userId, int accountId)
        {
            const string ReasonFormat = "User #{0} does not have access to edit account #{1}.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, userId, accountId)), 
                this.UnauthorizedAccountEditFaultCode);
        }

        /// <summary>
        /// The create unauthorized account withdraw fault exception.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="accountId">
        /// The account id.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        public virtual FaultException CreateUnauthorizedAccountWithdrawFaultException(int userId, int accountId)
        {
            const string ReasonFormat = "User #{0} does not have access to withraw from account #{1}.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, userId, accountId)), 
                this.UnauthorizedAccountWithdrawFaultCode);
        }

        /// <summary>
        /// The create unrecognized credentials exception.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="FaultException">
        /// </exception>
        public virtual FaultException CreateUnrecognizedCredentialsException(string email)
        {
            const string ReasonFormat = "The credentials e-mail {0} with an undisclosed password were unrecognized.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, email)), 
                this.UnrecognizedCredentialsFaultCode);
        }

        #endregion
    }
}