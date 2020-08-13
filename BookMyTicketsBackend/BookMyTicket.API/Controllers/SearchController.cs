using BookMyTicket.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookMyTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchrService)
        {
            _searchService = searchrService;
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
        public IActionResult GetAllShowsByMoviesAndCity(long cityID, long cinemaID)
        {
            return Ok(_searchService.GetAllShowsByMoviesAndCity(cityID, cinemaID));
        }
        [HttpGet]
        [Route("GetSeatingLayoutByShow/{id}")]
        public IActionResult GetSeatingLayoutByShow(long id)
        {
            return Ok(_searchService.GetSeatingLayoutByShow(id));
        }
    }
}
