using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.DAL.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
