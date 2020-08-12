using BookMyTicket.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyTicket.DAL.Configurations
{
    public class BookMyTicketDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Show> Cinemas { get; set; }


        public BookMyTicketDBContext(DbContextOptions<BookMyTicketDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .ApplyConfiguration(new UserConfiguration());
        }
    }
}
