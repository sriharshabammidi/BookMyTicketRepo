using BookMyTicket.Models;

namespace BookMyTicket.Interfaces.Services
{
    public interface ITicketService
    {
        Ticket BookTicket(BookTicketRequest bookTicketRequest);
    }
}
