using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookMyTicket.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchrService)
        {
            this._searchService = searchrService;
        }
        [HttpGet]
        [Route("GetAllCities")]
        public IActionResult GetAllCities()
        {
            return Ok(_searchService.GetCities());
        }
        [HttpGet]
        [Route("GetMoviesByCity/{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_searchService.GetMoviesByCity(id));
        }
        [HttpGet]
        [Route("GetAllShowsByMoviesAndCity/{cityId}/{cinemaId}")]
        public IActionResult GetAllShowsByMoviesAndCity(long cityId, long cinemaId)
        {
            return Ok(_searchService.GetAllShowsByMoviesAndCity(cityId, cinemaId));
        }
        // GET: api/<SearchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST api/<SearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
