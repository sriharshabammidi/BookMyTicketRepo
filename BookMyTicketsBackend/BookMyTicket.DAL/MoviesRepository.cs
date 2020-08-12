using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;

namespace BookMyTicket.DAL
{
    public class MoviesRepository : BaseRepository<Movie>, IMoviesRepository
    {
        public MoviesRepository(BookMyTicketDBContext dbContext)
           : base(dbContext)
        {
        }
        public List<Movie> GetMovies()
        {
            return GetAll();
        }
    }
}
