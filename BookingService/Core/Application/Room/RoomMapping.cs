using AutoMapper;
using Application.Rooms.Dtos;
using Domain.Rooms.ValueObjects;
using Domain.Rooms.Enums;
using Domain.Rooms.Entities;
using Application.Rooms.Requests;

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
                .ConstructUsing(src => new Price(src.Value, Enum.Parse<AcceptedCurrencies>(src.Currency, true)));

            CreateMap<Room, RoomDto>();

            // logic for Price.
            CreateMap<CreateRoomRequest, Room>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => new Price(src.Value, Enum.Parse<AcceptedCurrencies>(src.Currency, true))));
            
            CreateMap<UpdateRoomRequest, Room>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => new Price(src.Value!.Value, Enum.Parse<AcceptedCurrencies>(src.Currency!, true))))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember is not null));
        }
    }
}