using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookMyTicket.Entities
{
    [Table("Movies", Schema = "BookMyTicket")]
    public class Movie : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
