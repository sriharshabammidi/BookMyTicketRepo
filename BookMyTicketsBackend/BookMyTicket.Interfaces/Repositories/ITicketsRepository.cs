using BookMyTicket.Entities;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface ITicketsRepository
    {
        Ticket GetTicketByID(long ticketID);
        List<Ticket> GetTicketsByShowID(long ticketID);
        long InsetTicket(Ticket ticket);
    }
}
