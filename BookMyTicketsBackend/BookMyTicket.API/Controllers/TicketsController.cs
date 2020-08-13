using BookMyTicket.API.Filters;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticektService)
        {
            _ticketService = ticektService;
        }

        [Authorize]
        [ServiceFilter(typeof(EnsureUserLoggedIn))]
        [HttpPost]
        [Route("BookTicket")]
        public Ticket BookTicket(BookTicketRequest ticketRequest)
        {
            return _ticketService.BookTicket(ticketRequest);
        }
    }
}
