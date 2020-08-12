using BookMyTicket.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IShowsRepository
    {
        List<Show> GetShowsByCinema(long cinemaID);
        List<Show> GetShowsByCinemas(List<long> cinemaIDs);
        void Save();
    }
}
