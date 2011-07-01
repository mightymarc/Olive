namespace Olive.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;

    [ServiceContract]
    public interface IOlive
    {
        [OperationContract]
        Guid CreateSession(int userId, string password);

        [OperationContract]
        int CreateUser(string password);
    }
}
