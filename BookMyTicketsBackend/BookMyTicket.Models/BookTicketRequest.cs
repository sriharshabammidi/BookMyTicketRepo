using System.Collections.Generic;

namespace BookMyTicket.Models
{
    public class BookTicketRequest
    {
        public long ShowID { get; set; }
        public List<long> SeatIDs { get; set; }
    }
}
