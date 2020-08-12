namespace BookMyTicket.Models
{
    public class CinemaSeat
    {
        public long ID { get; set; }
        public string SeatNumber { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public bool IsBooked { get; set; }
    }
}
