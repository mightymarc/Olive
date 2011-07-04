namespace Olive.Website.Controllers
{
    using System;

    public interface ISiteSession
    {
        /// <summary>
        /// Gets a value indicating whether HasSession.
        /// </summary>
        bool HasSession { get; }

        /// <summary>
        /// Gets or sets SessionId.
        /// </summary>
        Guid SessionId { get; set; }
    }
}