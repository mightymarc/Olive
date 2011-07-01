namespace Olive.DataAccess
{
    using System;

    public class Session
    {
        public Guid SessionId { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
    }
}