using System.Collections.Generic;

namespace BookMyTicket.Models
{
    public class Cinema
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long LayoutID { get; set; }
        public long CityID { get; set; }
        public List<Show> Shows { get; set; }
    }
}
