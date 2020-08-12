using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyTicket.DAL.Configurations
{
    class CinemaSeatingConfiguration : IEntityTypeConfiguration<CinemaSeating>
    {
        public CinemaSeatingConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<CinemaSeating> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
