using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Core.ClientContext
{
    public interface IClientContext
    {
        UserProfile UserInfo { get; set; }

        string SessionId { get; set; }

        DateTime? TokenExpiry { get; set; }
    }
}
