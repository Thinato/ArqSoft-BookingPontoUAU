using AutoMapper;
using Application.Rooms.Dtos;
using Domain.Rooms.ValueObjects;
using Domain.Rooms.Enums;
using Domain.Rooms.Entities;

namespace Application.Rooms
{
    public class RoomMapping : Profile
    {
        public RoomMapping()
        {
            CreateMap<Price, PriceDto>()
                .ForMember(
                    dest => dest.Currency,
                    opt => opt.MapFrom(src => src.Currency.ToString()));
            
            CreateMap<PriceDto, Price>()
                .ForMember(
                    dest => dest.Currency,
                    opt => opt.MapFrom(src => Enum.Parse<AcceptedCurrencies>(src.Currency, true)));
            
            CreateMap<Room, RoomDto>();
        }
    }
}