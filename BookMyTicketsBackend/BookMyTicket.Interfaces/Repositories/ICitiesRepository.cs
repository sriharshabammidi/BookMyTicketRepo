using BookMyTicket.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ICitiesRepository
    {
        List<City> GetCities();
        void Save();
    }
}
