namespace Olive.Website.Helpers
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ISiteSession))]
    public abstract class ISiteSessionContract : ISiteSession
    {
        public bool HasSession
        {
            get
            {
                return default(bool);
            }
        }

        public Guid SessionId
        {
            get
            {
                Contract.Ensures(Contract.Result<Guid>() != Guid.Empty);
                return default(Guid);
            }
            set
            {
                Contract.Requires<ArgumentException>(value != Guid.Empty, "value");
                return;
            }
        }
    }
}