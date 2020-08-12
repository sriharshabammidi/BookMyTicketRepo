using AutoMapper;
using BookMyTicket.Core.ClientContext;
using BookMyTicket.Interfaces.Repositories;
using BookMyTicket.Interfaces.Services;
using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.BLL
{
    public class TicketService: ITicketService
    {
        public IShowsRepository _showsRepository { get; set; }
        public ITicketsRepository _ticketsRepository { get; set; }
        public IReservationsRepository _reservationsRepository { get; set; }
        public IClientContext _clientContext { get; set; }
        private readonly IMapper _mapper;

        public TicketService( IShowsRepository showsRepository, ITicketsRepository ticketsRepository, IReservationsRepository reservationsRepository,IClientContext clientContext, IMapper mapper)
        {
            _showsRepository = showsRepository;
            _ticketsRepository = ticketsRepository;
            _reservationsRepository = reservationsRepository;
            _clientContext = clientContext;
            _mapper = mapper;
        }

        public Ticket BookTicket(BookTicketRequest bookTicketRequest)
        {
            var ticket = new Ticket
            {
                ShowID = bookTicketRequest.ShowID,
                Price = _showsRepository.GetPriceByShow(bookTicketRequest.ShowID) * bookTicketRequest.SeatIDs.Count,
                UserID = _clientContext.UserInfo.ID
            };
            
            ticket.ID = _ticketsRepository.InsetTicket(_mapper.Map<Entities.Ticket>(ticket));
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
            _showsRepository.Save();
            return ticket;
        }
    }
}
