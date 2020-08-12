using System.ComponentModel.DataAnnotations.Schema;

namespace BookMyTicket.Entities
{
    [Table("Users", Schema = "BookMyTicket")]
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
