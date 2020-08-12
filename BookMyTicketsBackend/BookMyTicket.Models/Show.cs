using System;

namespace BookMyTicket.Models
{
    public class Show
    {
        public long ID { get; set; }
        public DateTime ShowTime { get; set; }
        public long MovieID { get; set; }
        public long CinemaID { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
