using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.DAL
{
    public class CitiesRepository : BaseRepository<City>, ICitiesRepository
    {
        public CitiesRepository(BookMyTicketDBContext dbContext)
            : base(dbContext)
        {
        }
        public List<City> GetCities()
        {
            return this.GetAll();
        }
    }
}
