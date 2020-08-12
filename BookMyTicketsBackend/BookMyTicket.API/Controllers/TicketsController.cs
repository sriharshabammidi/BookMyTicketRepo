using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyTicket.API.Filters;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        // GET: api/<Tickets>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Tickets>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Tickets>
        [Authorize]
        [ServiceFilter(typeof(EnsureUserLoggedIn))]
        [HttpPost]
        [Route("BookTicket")]
        public Ticket BookTicket(BookTicketRequest ticketRequest)
        {
            return _ticketService.BookTicket(ticketRequest);
        }

        // PUT api/<Tickets>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Tickets>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
