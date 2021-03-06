﻿using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ICinemaSeatingRepository
    {
        List<CinemaSeating> GetCinemasSeatingByLayoutID(long layoutID);
        CinemaSeating GetCinemasSeatinByID(long seatID);
        void Save();
    }
}
