using AutoMapper;
using BookMyTicket.Entities;
using BookMyTicket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Core.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, User>().ReverseMap();
            CreateMap<Entities.City, Models.City>().ReverseMap();
            CreateMap<Entities.Movie, Models.Movie>().ReverseMap();
        }
    }
}
