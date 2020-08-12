using AutoMapper;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookMyTicket.BLL
{
    public class SearchService : ISearchService
    {
        public ICitiesRepository _cityRepository { get; set; }
        public IMoviesRepository _movieRepository { get; set; }
        public ICinemaRepository _cinemaRepository { get; set; }
        public IShowsRepository _showsRepository { get; set; }
        public ICinemaSeatingRepository _cinemaSeatingRepository { get; set; }
        public ITicketsRepository _ticketsRepository { get; set; }
        public IReservationsRepository _reservationsRepository { get; set; }
        private readonly IMapper _mapper;

        public SearchService(ICitiesRepository cityRepository, IMoviesRepository movieRepository, ICinemaRepository cinemaRepository, IShowsRepository showsRepository, ICinemaSeatingRepository cinemaSeatingRepository, ITicketsRepository ticketsRepository, IReservationsRepository reservationsRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _movieRepository = movieRepository;
            _cinemaRepository = cinemaRepository;
            _showsRepository = showsRepository;
            _cinemaSeatingRepository = cinemaSeatingRepository;
            _ticketsRepository = ticketsRepository;
            _reservationsRepository = reservationsRepository;
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
            movies = movies.Where(movie => showsInTheCity.Select(show => show.MovieID).Contains(movie.ID)).ToList();
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
            cinemasInTheCity = cinemasInTheCity.Where(cinema => cinema.Shows.Count > 0).ToList();
            return cinemasInTheCity;
        }
        public List<CinemaSeat> GetSeatingLayoutByShow(long showID)
        {
            Cinema cinema = _mapper.Map<Cinema>(_cinemaRepository.GetCinemaByID(showID));
            List<CinemaSeat> cinemaSeats = _mapper.Map<List<CinemaSeat>>(_cinemaSeatingRepository.GetCinemasSeatingByLayoutID(cinema.LayoutID));
            List<Ticket> tickets = _mapper.Map<List<Ticket>>(_ticketsRepository.GetTicketsByShowID(showID));
            List<long> reservations = _reservationsRepository.GetReservedSeatsByTicketIDs(tickets.Select(ticket => ticket.ID).ToList());
            cinemaSeats.ForEach(seat =>
            {
                seat.IsBooked = reservations.Contains(seat.ID);
            });
            return cinemaSeats;
        }
    }
}
