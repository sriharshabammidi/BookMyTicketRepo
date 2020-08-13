using BookMyTicket.DAL.Configurations;
using BookMyTicket.Entities;
using BookMyTicket.Interfaces.Repositories;
using System.Collections.Generic;

namespace BookMyTicket.DAL
{
    public class TicketsRepository : BaseRepository<Ticket>, ITicketsRepository
    {
        public TicketsRepository(BookMyTicketDBContext dbContext)
           : base(dbContext)
        {
        }
        public Ticket GetTicketByID(long ticketID)
        {
            return GetById(ticketID);
        }

        public List<Ticket> GetTicketsByShowID(long ticketID)
        {
            return GetBy(ticket => ticket.ShowID == ticketID);
        }

        public long InsertTicket(Ticket ticket)
        {
            return Insert(ticket).ID;            
        }
    }
}
