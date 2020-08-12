using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Models
{
    public class UserProfile
    {
        public long ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public bool IsSuspended { get; set; }

        public DateTime SuspensionDate { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}
