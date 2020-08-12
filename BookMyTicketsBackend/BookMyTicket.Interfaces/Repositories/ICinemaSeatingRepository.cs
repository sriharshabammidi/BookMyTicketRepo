using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ICinemaSeatingRepository
    {
        List<CinemaSeating> GetCinemasSeatingByLayoutID(long layoutID);
        void Save();
    }
}
