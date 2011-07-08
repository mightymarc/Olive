// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetAccountAccount.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the GetAccountAccount type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.Services
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetAccountAccount
    {
        [DataMember]
        public string DisplayName { get; set; }
    }
}