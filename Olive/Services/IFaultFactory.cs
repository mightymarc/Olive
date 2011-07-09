namespace Olive.Services
{
    using System;
    using System.ServiceModel;

    public interface IFaultFactory
    {
        FaultCode UnauthorizedAccountEditFaultCode { get; }

        FaultCode UnrecognizedCredentialsFaultCode { get; }

        FaultCode EmailAlreadyRegisteredFaultCode { get; }

        FaultCode UnauthorizedAccountWithdrawFaultCode { get; }

        FaultCode SessionDoesNotExistFaultCode { get; }

        FaultCode UnauthorizedAccountAccessFaultCode { get; }

        FaultException CreateUnauthorizedAccountAccessFaultException(int userId, int accountId);

        FaultException CreateUnauthorizedAccountEditFaultException(int userId, int accountId);

        FaultException CreateUnauthorizedAccountWithdrawFaultException(int userId, int accountId);

        FaultException CreateSessionDoesNotExistFaultException(Guid sessionId);

        FaultException CreateEmailAlreadyRegisteredFaultException(string email);

        FaultException CreateUnrecognizedCredentialsException(string email);
    }
}