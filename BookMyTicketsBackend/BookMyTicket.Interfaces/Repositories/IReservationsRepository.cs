using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IReservationsRepository
    {
        List<Reservation> GetReservationsByTicketID(long ticketID);
        List<long> GetReservedSeatsByTicketIDs(List<long> ticketIDs);
        long InsertReservation(Reservation reservation);
        void Save();
    }
}
