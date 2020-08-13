using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BookMyTicket.DAL
{
    public class ReservationsRepository : BaseRepository<Reservation>, IReservationsRepository
    {
        public ReservationsRepository(BookMyTicketDBContext dbContext)
           : base(dbContext)
        {
        }

        public List<Reservation> GetReservationsByTicketID(long ticketID)
        {
            return GetBy(reservatrion => reservatrion.TicketID == ticketID);
        }
        public List<long> GetReservedSeatsByTicketIDs(List<long> ticketIDs)
        {
            return GetBy(reservatrion => ticketIDs.Contains(reservatrion.TicketID)).Select(reservation => reservation.SeatID).ToList();
        }
        public long InsertReservation(Reservation reservation)
        {
            return Insert(reservation).ID;
        }
    }
}
