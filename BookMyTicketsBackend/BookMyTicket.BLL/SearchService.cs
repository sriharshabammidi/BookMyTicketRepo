using AutoMapper;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookMyTicket.BLL
{
    public class SearchService : ISearchService
    {
        public ICitiesRepository _cityRepository { get; set; }
        public IMoviesRepository _movieRepository { get; set; }
        public ICinemaRepository _cinemaRepository { get; set; }
        public IShowsRepository _showsRepository { get; set; }
        private readonly IMapper _mapper;

        public SearchService(ICitiesRepository cityRepository, IMoviesRepository movieRepository, ICinemaRepository cinemaRepository,IShowsRepository showsRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _movieRepository = movieRepository;
            _cinemaRepository = cinemaRepository;
            _showsRepository = showsRepository;
            _mapper = mapper;
        }
        public List<City> GetCities()
        {
            return _mapper.Map<List<City>>(_cityRepository.GetCities());
        }
        public List<Movie> GetMoviesByCity(long cityID)
        {
            List<Movie> movies = _mapper.Map<List<Movie>>(_movieRepository.GetMovies());
            List<Cinema> cinemasInTheCity = _mapper.Map<List<Cinema>>(_cinemaRepository.GetCinemasByCity(cityID));
            List<Show> showsInTheCity = _mapper.Map<List<Show>>(_showsRepository.GetShowsByCinemas(cinemasInTheCity.Select(cinema => cinema.ID).ToList()));
            movies = movies.Where(movie=> showsInTheCity.Select(show => show.MovieID).Contains(movie.ID)).ToList();
            return movies;
        }
        public List<Cinema> GetAllShowsByMoviesAndCity(long cityID, long movieID)
        {
            List<Cinema> cinemasInTheCity = _mapper.Map<List<Cinema>>(_cinemaRepository.GetCinemasByCity(cityID));
            List<Show> showsInTheCity = _mapper.Map<List<Show>>(_showsRepository.GetShowsByMovieAndCity(cinemasInTheCity.Select(cinema => cinema.ID).ToList(), movieID));
            cinemasInTheCity.ForEach(cinema =>
            {
                cinema.Shows = showsInTheCity.Where(show => show.CinemaID == cinema.ID).ToList();
            });
            return cinemasInTheCity;
        }

    }
}
