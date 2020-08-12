using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.DAL
{
    class CinemasRepository : BaseRepository<Cinema>, ICinemaRepository
    {
        public CinemasRepository(BookMyTicketDBContext dbContext)
            : base(dbContext)
        {
        }
        public List<Cinema> GetCinemasByCity(long CityID)
        {
            return this.GetBy(cinema => cinema.CityID == CityID);
        }
    }
}
