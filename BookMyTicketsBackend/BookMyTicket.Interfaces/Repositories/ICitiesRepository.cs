using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ICitiesRepository
    {
        List<City> GetCities();
        void Save();
    }
}
