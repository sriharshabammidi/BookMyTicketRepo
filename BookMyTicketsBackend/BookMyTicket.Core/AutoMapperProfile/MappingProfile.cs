using AutoMapper;
using BookMyTicket.Entities;
using BookMyTicket.Models;

namespace BookMyTicket.Core.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, User>().ReverseMap();
            CreateMap<Entities.Cinema, Models.Cinema>().ReverseMap();
            CreateMap<Entities.CinemaSeating, Models.CinemaSeat>().ReverseMap();
            CreateMap<Entities.City, Models.City>().ReverseMap();
            CreateMap<Entities.Movie, Models.Movie>().ReverseMap();
            CreateMap<Entities.Reservation, Models.Reservation>().ReverseMap();
            CreateMap<Entities.Show, Models.Show>().ReverseMap();
            CreateMap<Entities.Ticket, Models.Ticket>().ReverseMap();
        }
    }
}
