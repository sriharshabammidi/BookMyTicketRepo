using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyTicket.DAL.Configurations
{
    public class BookMyTicketDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<CinemaSeating> CinemaSeating { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        public BookMyTicketDBContext(DbContextOptions<BookMyTicketDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .ApplyConfiguration(new UserConfiguration());
            builder
                .ApplyConfiguration(new CinemaConfiguration());
            builder
               .ApplyConfiguration(new CinemaSeatingConfiguration());
            builder
               .ApplyConfiguration(new CityConfiguration());
            builder
               .ApplyConfiguration(new MovieConfiguration());
            builder
               .ApplyConfiguration(new ReservationConfiguration());
            builder
                .ApplyConfiguration(new ShowConfiguration());
            builder
                .ApplyConfiguration(new CinemaConfiguration());
            builder
                .ApplyConfiguration(new TicketConfiguration());
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
