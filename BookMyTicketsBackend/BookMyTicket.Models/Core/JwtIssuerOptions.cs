using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Models.Core
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
