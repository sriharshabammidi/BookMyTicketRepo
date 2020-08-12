using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyTicket.DAL.Configurations
{
    class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public CinemaConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
