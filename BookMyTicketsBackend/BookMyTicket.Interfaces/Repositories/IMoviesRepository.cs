using BookMyTicket.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IMoviesRepository
    {
        List<Movie> GetMovies();
        void Save();
    }
}
