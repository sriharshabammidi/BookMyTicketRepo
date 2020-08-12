using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Models
{
    public class Cinema
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long LayoutID { get; set; }
        public long CityID { get; set; }
    }
}
