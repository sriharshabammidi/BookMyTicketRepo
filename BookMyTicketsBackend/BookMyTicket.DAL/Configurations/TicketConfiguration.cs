using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMyTicket.DAL.Configurations
{
    class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public TicketConfiguration()
        {

        }
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .Property(p => p.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
