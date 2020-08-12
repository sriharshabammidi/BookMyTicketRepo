using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IMoviesRepository
    {
        List<Movie> GetMovies();
        void Save();
    }
}
