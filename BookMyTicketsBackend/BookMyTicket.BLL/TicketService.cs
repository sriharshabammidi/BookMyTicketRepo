using AutoMapper;
using BookMyTicket.Core.ClientContext;
using BookMyTicket.DAL.Configurations;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookMyTicket.BLL
{
    public class TicketService : ITicketService
    {
        private readonly IShowsRepository _showsRepository;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IClientContext _clientContext;
        private readonly ISearchService _searchService;
        private readonly BookMyTicketDBContext _bookMyTicketDBContext;
        private readonly IMapper _mapper;

        public TicketService(IShowsRepository showsRepository, ITicketsRepository ticketsRepository, IReservationsRepository reservationsRepository, IClientContext clientContext, ISearchService searchService, IMapper mapper, BookMyTicketDBContext bookMyTicketDBContext)
        {
            _showsRepository = showsRepository;
            _ticketsRepository = ticketsRepository;
            _reservationsRepository = reservationsRepository;
            _clientContext = clientContext;
            _mapper = mapper;
            _searchService = searchService;
            _bookMyTicketDBContext = bookMyTicketDBContext;
        }

        public Ticket BookTicket(BookTicketRequest bookTicketRequest)
        {
            Ticket ticket=null;
            var reservedSeatsForTheShow = _searchService.GetReservedSeatsByShow(bookTicketRequest.ShowID);
            if (reservedSeatsForTheShow.Intersect(bookTicketRequest.SeatIDs).Any())
            {
                throw new Models.Core.AppValidationException("One or more seats are already reserved, please try other seats.");
            }
            var pricePerUnit = _showsRepository.GetPriceByShow(bookTicketRequest.ShowID);
            if (pricePerUnit != null)
            {
                using (var transaction = _bookMyTicketDBContext.Database.BeginTransaction())
                {
                    ticket = new Ticket
                    {
                        ShowID = bookTicketRequest.ShowID,
                        Price = (decimal)(pricePerUnit * bookTicketRequest.SeatIDs.Count),
                        UserID = _clientContext.UserInfo.ID
                    };

                    ticket.ID = _ticketsRepository.InsertTicket(_mapper.Map<Entities.Ticket>(ticket));
                    ticket.ReservedSeats = new List<long>();

                    bookTicketRequest.SeatIDs.ForEach(seat =>
                    {
                        var reservation = new Reservation()
                        {
                            TicketID = ticket.ID,
                            SeatID = seat
                        };
                        _reservationsRepository.InsertReservation(_mapper.Map<Entities.Reservation>(reservation));
                        ticket.ReservedSeats.Add(seat);
                    });
                    transaction.Commit();
                }
            }
            return ticket;
        }
    }
}
