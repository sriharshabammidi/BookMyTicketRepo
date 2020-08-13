using BookMyTicket.Models;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Services
{
    public interface ISearchService
    {
        List<City> GetCities();
        List<Movie> GetMoviesByCity(long cityID);
        List<Cinema> GetAllShowsByMoviesAndCity(long cityID, long movieID);
        List<CinemaSeat> GetSeatingLayoutByShow(long showID);
        List<long> GetReservedSeatsByShow(long showID);
    }
}
