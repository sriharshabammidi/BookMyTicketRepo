using AutoMapper;
using BookMyTicket.BLL;
using BookMyTicket.Core.AutoMapperProfile;
using BookMyTicket.Core.ClientContext;
using BookMyTicket.DAL;
using BookMyTicket.DAL.Configurations;
using BookMyTicket.Models;
using BookMyTicket.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookMyTicket.Test
{
    [TestClass]
    public class TicketServiceTest
    {
        private readonly TicketService ticketService;
        public TicketServiceTest()
        {
            ticketService = CreateSearchService();
        }
        [TestMethod]
        public void BookTicket_Test_1()
        {
            BookTicketRequest ticketRequest = new BookTicketRequest()
            {
                SeatIDs = new System.Collections.Generic.List<long>() { 1, 2 },
                ShowID = 1
            };
            var ticket = ticketService.BookTicket(ticketRequest);
            Assert.IsNotNull(ticket);
            Assert.AreEqual(ticket.UserID, 1);
            Assert.AreEqual(ticket.Price, 123 * 2);
            Assert.AreEqual(ticket.ReservedSeats.Count, 2);
        }
        [TestMethod]
        public void BookTicket_Test_2()
        {
            BookTicketRequest ticketRequest = new BookTicketRequest()
            {
                SeatIDs = new System.Collections.Generic.List<long>() { 1, 2 },
                ShowID = 7
            };
            var ticket = ticketService.BookTicket(ticketRequest);
            Assert.IsNull(ticket);
        }
        [TestMethod]
        [ExpectedException(typeof(AppValidationException))]//Asset exception attribute
        public void BookTicket_Test_3()
        {
            BookTicketRequest ticketRequest = new BookTicketRequest()
            {
                SeatIDs = new System.Collections.Generic.List<long>() { 1 },
                ShowID = 1
            };
            ticketService.BookTicket(ticketRequest);
            ticketRequest.SeatIDs.Add(1);
            ticketService.BookTicket(ticketRequest);
            //Asserts exception : specified as attribute
        }
        private TicketService CreateSearchService()
        {
            var options = new DbContextOptionsBuilder<BookMyTicketDBContext>()
                   .UseInMemoryDatabase(nameof(SearchServiceTest), new InMemoryDatabaseRoot())
                   .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                   .Options;
            var dbContext = new BookMyTicketDBContext(options);

            var cityRepository = new CitiesRepository(dbContext);
            var movieRepository = new MoviesRepository(dbContext);
            var cinemaRepository = new CinemasRepository(dbContext);
            var showsRepository = new ShowsRepository(dbContext);
            var cinemaSeatingRepository = new CinemaSeatingRepository(dbContext);
            var ticketsRepository = new TicketsRepository(dbContext);
            var reservationsRepository = new ReservationsRepository(dbContext);
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            var clientContext = new ClientContext()
            {
                UserInfo = new Models.UserProfile()
                {
                    ID = 1
                }
            };
            var mapper = new Mapper(configuration);
            MockDB(dbContext);
            var searchService = new SearchService(cityRepository, movieRepository, cinemaRepository, showsRepository, cinemaSeatingRepository, ticketsRepository, reservationsRepository, mapper);
            return new TicketService(showsRepository, ticketsRepository, reservationsRepository, clientContext, searchService, mapper, dbContext);
        }
        private void MockDB(BookMyTicketDBContext bookMyTicketDB)
        {
            bookMyTicketDB.Cities.Add(new Entities.City { ID = 1, Name = "City" });
            bookMyTicketDB.Movies.Add(new Entities.Movie { ID = 1, Name = "Movie", Description = "Movie1", Duration = 120, Poster = "Poster", ReleaseDate = DateTime.Now });
            bookMyTicketDB.CinemaSeating.Add(new Entities.CinemaSeating { ID = 1, SeatNumber = "A1", ColumnNumber = 0, RowNumber = 0, LayoutID = 1 });
            bookMyTicketDB.CinemaSeating.Add(new Entities.CinemaSeating { ID = 2, SeatNumber = "A2", ColumnNumber = 0, RowNumber = 1, LayoutID = 1 });
            bookMyTicketDB.Cinema.Add(new Entities.Cinema { ID = 1, Name = "Cinema", CityID = 1, LayoutID = 1 });
            bookMyTicketDB.Shows.Add(new Entities.Show { ID = 1, CinemaID = 1, MovieID = 1, ShowTime = DateTime.Now, PricePerUnit = 123 });
            bookMyTicketDB.Shows.Add(new Entities.Show { ID = 2, CinemaID = 1, MovieID = 1, ShowTime = DateTime.Now.AddDays(1), PricePerUnit = 13 });
            bookMyTicketDB.SaveChanges();
        }
    }
}
