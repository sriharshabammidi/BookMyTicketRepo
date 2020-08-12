using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyTicket.Entities
{
    [Table("Cities", Schema = "BookMyTicket")]
    public class City : BaseEntity
    {
        public string Name { get; set; }
    }
}
