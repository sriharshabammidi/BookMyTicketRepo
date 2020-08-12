using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Models
{
    public class BookTicketRequest
    {
        public long ShowID { get; set; }
        public List<long> SeatIDs { get; set; }
    }
}
