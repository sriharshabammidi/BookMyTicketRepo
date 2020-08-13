using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;

namespace BookMyTicket.DAL
{
    public class ShowsRepository : BaseRepository<Show>, IShowsRepository
    {
        public ShowsRepository(BookMyTicketDBContext dbContext)
           : base(dbContext)
        {
        }
        public List<Show> GetShowsByCinema(long cinemaID)
        {
            return GetBy(show => show.CinemaID == cinemaID);
        }

        public List<Show> GetShowsByCinemas(List<long> cinemaIDs)
        {
            return GetBy(show => cinemaIDs.Contains(show.CinemaID));
        }

        public List<Show> GetShowsByMovieAndCity(List<long> cinemaIDs, long movieID)
        {
            return GetBy(show => show.MovieID == movieID && cinemaIDs.Contains(show.CinemaID));
        }

        public decimal? GetPriceByShow(long showID)
        {
            return GetById(showID)?.PricePerUnit;
        }
    }
}
