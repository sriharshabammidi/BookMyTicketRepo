using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
