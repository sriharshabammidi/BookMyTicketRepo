using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookMyTicket.Entities
{
    [Table("Cinemas", Schema = "BookMyTicket")]
    public class Cinema : BaseEntity
    {
        public string Name { get; set; }
        public long LayoutID { get; set; }
        public long CityID { get; set; }
    }
}
