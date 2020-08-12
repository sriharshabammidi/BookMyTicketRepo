using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IShowsRepository
    {
        List<Show> GetShowsByCinema(long cinemaID);
        List<Show> GetShowsByCinemas(List<long> cinemaIDs);
        List<Show> GetShowsByMovieAndCity(List<long> cinemaIDs, long movieID);
        decimal GetPriceByShow(long showID);
        void Save();
    }
}
