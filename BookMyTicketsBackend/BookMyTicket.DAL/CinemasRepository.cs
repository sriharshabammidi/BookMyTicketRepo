using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;

namespace BookMyTicket.DAL
{
    public class CinemasRepository : BaseRepository<Cinema>, ICinemaRepository
    {
        public CinemasRepository(BookMyTicketDBContext dbContext)
            : base(dbContext)
        {
        }

        public Cinema GetCinemaByID(long cinemaID)
        {
            return GetById(cinemaID);
        }

        public List<Cinema> GetCinemasByCity(long CityID)
        {
            return GetBy(cinema => cinema.CityID == CityID);
        }
    }
}
