using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyTicket.Entities
{
    [Table("Tickets", Schema = "BookMyTicket")]
    public class Ticket : BaseEntity
    {
        public long ShowID { get; set; }
        public long UserID { get; set; }
        public decimal Price { get; set; }
    }
}
