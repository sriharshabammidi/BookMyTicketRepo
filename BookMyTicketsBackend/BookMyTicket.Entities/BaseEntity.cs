﻿using System.ComponentModel.DataAnnotations;

namespace BookMyTicket.Entities
{
    public class BaseEntity
    {
        [Key]
        public long ID { get; set; }
        //public string CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime ModifiedDate { get; set; }

    }
}
