using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ICinemaRepository
    {
        List<Cinema> GetCinemasByCity(long cityID);
        Cinema GetCinemaByID(long cinemaID);
        void Save();
    }
}
