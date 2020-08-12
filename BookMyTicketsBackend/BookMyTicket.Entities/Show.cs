using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyTicket.Entities
{
    [Table("Shows", Schema = "BookMyTicket")]
    public class Show : BaseEntity
    {
        public DateTime ShowTime { get; set; }
        public long MovieID { get; set; }
        public long CinemaID { get; set; }
        public decimal PricePerUnit { get; set; }
    }
}
