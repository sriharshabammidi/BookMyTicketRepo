using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
