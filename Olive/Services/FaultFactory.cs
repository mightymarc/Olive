using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olive.Services
{
    using System.Globalization;
    using System.ServiceModel;

    public class FaultFactory : IFaultFactory
    {
        public FaultCode UnauthorizedAccountEditFaultCode { get; protected set; }

        public FaultCode UnrecognizedCredentialsFaultCode { get; protected set; }

        public FaultCode EmailAlreadyRegisteredFaultCode { get; protected set; }

        public FaultCode UnauthorizedAccountWithdrawFaultCode { get; protected set; }

        public FaultCode SessionDoesNotExistFaultCode { get; protected set; }

        public FaultCode UnauthorizedAccountAccessFaultCode { get; protected set; }

        public FaultFactory()
        {
            this.UnauthorizedAccountEditFaultCode = new FaultCode("UnauthorizedAccountEdit");
            this.UnrecognizedCredentialsFaultCode = new FaultCode("UnrecognizedCredentials");
            this.EmailAlreadyRegisteredFaultCode = new FaultCode("EmailAlreadyRegistered");
            this.UnauthorizedAccountWithdrawFaultCode = new FaultCode("UnauthorizedAccountWithdraw");
            this.SessionDoesNotExistFaultCode = new FaultCode("SessionDoesNotExist");
            this.UnauthorizedAccountAccessFaultCode = new FaultCode("UnauthorizedAccountAccess");
        }

        public FaultException CreateUnauthorizedAccountAccessFaultException(int userId, int accountId)
        {
            const string ReasonFormat = "User #{0} does not have access to view account #{1}.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, userId, accountId)),
                this.UnauthorizedAccountAccessFaultCode);
        }

        public virtual FaultException CreateUnauthorizedAccountEditFaultException(int userId, int accountId)
        {
            const string ReasonFormat = "User #{0} does not have access to edit account #{1}.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, userId, accountId)),
                this.UnauthorizedAccountEditFaultCode);
        }

        public virtual FaultException CreateUnauthorizedAccountWithdrawFaultException(int userId, int accountId)
        {
            const string ReasonFormat = "User #{0} does not have access to withraw from account #{1}.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, userId, accountId)),
                this.UnauthorizedAccountWithdrawFaultCode);
        }

        public virtual FaultException CreateSessionDoesNotExistFaultException(Guid sessionId)
        {
            const string ReasonFormat = "The specified session, {0}, does not exist or has expired.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, sessionId)),
                this.SessionDoesNotExistFaultCode);
        }

        public virtual FaultException CreateEmailAlreadyRegisteredFaultException(string email)
        {
            const string ReasonFormat = "The specified e-mail, {0}, is already registered to another user.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, email)),
                this.EmailAlreadyRegisteredFaultCode);
        }

        public virtual FaultException CreateUnrecognizedCredentialsException(string email)
        {
            const string ReasonFormat = "The credentials e-mail {0} with an undisclosed password were unrecognized.";

            throw new FaultException(
                new FaultReason(string.Format(CultureInfo.CurrentCulture, ReasonFormat, email)),
                this.UnrecognizedCredentialsFaultCode);
        }
    }
}