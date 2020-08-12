using BookMyTicket.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ICinemaRepository
    {
        List<Cinema> GetCinemasByCity(long CityID);
        void Save();
    }
}
