using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyTicket.Entities
{
    [Table("CinemaSeating", Schema = "BookMyTicket")]
    public class CinemaSeating : BaseEntity
    {
        public string SeatNumber { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public long LayoutID { get; set; }
    }
}
