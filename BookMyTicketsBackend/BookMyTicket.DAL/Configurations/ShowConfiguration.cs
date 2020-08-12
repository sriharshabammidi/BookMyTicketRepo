using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.DAL.Configurations
{
    class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public ShowConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
