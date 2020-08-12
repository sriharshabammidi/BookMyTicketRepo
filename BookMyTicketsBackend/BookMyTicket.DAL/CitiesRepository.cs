using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;

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
            return GetAll();
        }
    }
}
