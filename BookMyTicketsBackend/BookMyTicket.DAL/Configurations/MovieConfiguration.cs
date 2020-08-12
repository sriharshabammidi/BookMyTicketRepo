using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.DAL.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
