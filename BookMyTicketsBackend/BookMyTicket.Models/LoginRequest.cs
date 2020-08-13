using System.ComponentModel.DataAnnotations;

namespace BookMyTicket.Models
{
    public class LoginRequest
    {
        [Required]
        public string Email { get;set;}
        [Required]
        public string Password { get; set; }
    }
}
