using System.Collections.Generic;

namespace BookMyTicket.Models
{
    public class Ticket
    {
        public long ID { get; set; }
        public long ShowID { get; set; }
        public long UserID { get; set; }
        public decimal Price { get; set; }
        public List<long> ReservedSeats { get; set; }
    }
}
