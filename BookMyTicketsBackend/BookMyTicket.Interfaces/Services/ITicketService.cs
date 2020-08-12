using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Interfaces.Services
{
    public interface ITicketService
    {
        Ticket BookTicket(BookTicketRequest bookTicketRequest);
    }
}
