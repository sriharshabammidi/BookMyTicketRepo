using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Core.ClientContext
{
    public class ClientContext : IClientContext
    {
        public UserProfile UserInfo { get; set; }
        public string SessionId { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}
