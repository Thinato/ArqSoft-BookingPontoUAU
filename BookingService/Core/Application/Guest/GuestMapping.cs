using Application.Guests.Dtos;
using Application.Guests.Requests;
using AutoMapper;
using Domain.Guests.Entities;

namespace Application.Guests
{
    public class GuestMapping : Profile
    {
        public GuestMapping()
        {
            CreateMap<GuestDto, Guest>();


            CreateMap<UpdateGuestRequest, Guest>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}