using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Models
{
    public class Movie
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
