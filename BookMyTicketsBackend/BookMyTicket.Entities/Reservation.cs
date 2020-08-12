using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyTicket.Entities
{
    [Table("Reservations", Schema = "BookMyTicket")]
    public class Reservation : BaseEntity
    {
        public long TicketID { get; set; }
        public long SeatID { get; set; }
    }
}
