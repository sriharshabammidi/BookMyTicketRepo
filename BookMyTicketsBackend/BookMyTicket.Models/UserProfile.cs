using System.ComponentModel.DataAnnotations;

namespace BookMyTicket.Models
{
    public class UserProfile
    {
        public long ID { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
