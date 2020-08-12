using System;

namespace BookMyTicket.Models
{
    public class UserProfile
    {
        public long ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
    }
}
