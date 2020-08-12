using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;

namespace BookMyTicket.DAL
{
    public class CinemaSeatingRepository : BaseRepository<CinemaSeating>, ICinemaSeatingRepository
    {
        public CinemaSeatingRepository(BookMyTicketDBContext dbContext)
            : base(dbContext)
        {
        }
        public List<CinemaSeating> GetCinemasSeatingByLayoutID(long layoutID)
        {
            return GetBy(seat => seat.LayoutID == layoutID);
        }
    }
}
