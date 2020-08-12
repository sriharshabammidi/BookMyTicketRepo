using BookMyTicket.Models;
using System;

namespace BookMyTicket.Core.ClientContext
{
    public interface IClientContext
    {
        UserProfile UserInfo { get; set; }

        string SessionId { get; set; }

        DateTime? TokenExpiry { get; set; }
    }
}
