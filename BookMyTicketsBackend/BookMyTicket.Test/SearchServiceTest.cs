using AutoMapper;
using BookMyTicket.BLL;
using BookMyTicket.Core.AutoMapperProfile;
using BookMyTicket.DAL;
using BookMyTicket.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookMyTicket.Test
{
    [TestClass]
    public class SearchServiceTest
    {
        private readonly SearchService searchService;
        public SearchServiceTest() {
            searchService = CreateSearchService();
        }
        [TestMethod]
        public void GetAllCities_Test_1()
        {
            var cities = searchService.GetCities();
            Assert.IsTrue(cities.Count == 1);
            Assert.AreEqual(cities[0].Name,"City");
        }
        [TestMethod]
        public void GetMoviesByCity_Test_1()
        {
            var movie = searchService.GetMoviesByCity(1);
            Assert.IsTrue(movie.Count == 1);
            Assert.AreEqual(movie[0].Name, "Movie");
            Assert.AreEqual(movie[0].Description, "Movie1");
            Assert.AreEqual(movie[0].Duration, 120);
            Assert.AreEqual(movie[0].Poster, "Poster");
        }
        [TestMethod]
        public void GetMoviesByCity_Test_2()
        {
            var movie = searchService.GetMoviesByCity(2);
            Assert.IsTrue(movie.Count == 0);
        }
        [TestMethod]
        public void GetSeatingLayoutByShow_Test_1()
        {
            var seats = searchService.GetSeatingLayoutByShow(1);
            Assert.IsTrue(seats.Count == 2);
            Assert.AreEqual(seats[0].SeatNumber,"A1");
            Assert.AreEqual(seats[1].SeatNumber, "A2");
        }
        [TestMethod]
        public void GetSeatingLayoutByShow_Test_2()
        {
            var seats = searchService.GetSeatingLayoutByShow(2);
            Assert.IsNull(seats);
        }
        [TestMethod]
        public void GetAllShowsByMoviesAndCity_Test_1()
        {
            var cinemasWithShows = searchService.GetAllShowsByMoviesAndCity(1,1);
            Assert.IsTrue(cinemasWithShows.Count == 1);
            Assert.AreEqual(cinemasWithShows[0].Name, "Cinema");
            Assert.AreEqual(cinemasWithShows[0].Shows.Count, 2);
        }
        [TestMethod]
        public void GetAllShowsByMoviesAndCity_Test_2()
        {
            var seats = searchService.GetAllShowsByMoviesAndCity(1,2);
            Assert.IsTrue(seats.Count == 0);
        }
        private SearchService CreateSearchService()
        {
            var options = new DbContextOptionsBuilder<BookMyTicketDBContext>()
                   .UseInMemoryDatabase(nameof(SearchServiceTest), new InMemoryDatabaseRoot())
                   .Options;
            var dbContext = new BookMyTicketDBContext(options);

            var cityRepository = new CitiesRepository(dbContext);
            var movieRepository = new MoviesRepository(dbContext);
            var cinemaRepository = new CinemasRepository(dbContext);
            var showsRepository = new ShowsRepository(dbContext);
            var cinemaSeatingRepository = new CinemaSeatingRepository(dbContext);
            var ticketsRepository = new TicketsRepository(dbContext);
            var reservationsRepository = new ReservationsRepository(dbContext);
            var productProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            MockDB(dbContext);
            return new SearchService(cityRepository, movieRepository, cinemaRepository, showsRepository, cinemaSeatingRepository, ticketsRepository, reservationsRepository, mapper);
        }
        private void MockDB(BookMyTicketDBContext bookMyTicketDB)
        {
            bookMyTicketDB.Cities.Add(new Entities.City { ID = 1, Name = "City" });
            bookMyTicketDB.Movies.Add(new Entities.Movie { ID = 1, Name = "Movie", Description="Movie1", Duration=120, Poster="Poster", ReleaseDate= DateTime.Now });
            bookMyTicketDB.CinemaSeating.Add(new Entities.CinemaSeating { ID = 1, SeatNumber= "A1", ColumnNumber=0, RowNumber=0, LayoutID =1});
            bookMyTicketDB.CinemaSeating.Add(new Entities.CinemaSeating { ID = 2, SeatNumber= "A2", ColumnNumber=0, RowNumber=1, LayoutID =1});
            bookMyTicketDB.Cinema.Add(new Entities.Cinema { ID = 1, Name = "Cinema", CityID=1, LayoutID =1});
            bookMyTicketDB.Shows.Add(new Entities.Show { ID = 1, CinemaID=1, MovieID=1, ShowTime= DateTime.Now, PricePerUnit= 123});
            bookMyTicketDB.Shows.Add(new Entities.Show { ID = 2, CinemaID=1, MovieID=1, ShowTime= DateTime.Now.AddDays(1), PricePerUnit= 13});
            bookMyTicketDB.SaveChanges();
        }
    }
}
