using Application.Bookings.Dtos;
using Application.Bookings.Requests;
using Application.Guests.Dtos;
using Application.Rooms.Dtos;
using Application.Rooms.Requests;
using AutoMapper;
using Domain.Bookings.Entities;
using Domain.Guests.Entities;
using Domain.Guests.ValueObjects;
using Domain.Rooms.Entities;
using Domain.Rooms.Enums;
using Domain.Rooms.ValueObjects;

namespace Application.Bookings;

public class BookingMapping : Profile
{
    public BookingMapping()
    {
        // Existing mappings for Room and Price.
        CreateMap<Price, PriceDto>()
            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));

        CreateMap<PriceDto, Price>()
            .ConstructUsing(src => new Price(src.Value, Enum.Parse<AcceptedCurrencies>(src.Currency, true)));

        CreateMap<Room, RoomDto>();
        CreateMap<CreateRoomRequest, Room>()
            .ForMember(dest => dest.Price,
                       opt => opt.MapFrom(src => new Price(src.Value, Enum.Parse<AcceptedCurrencies>(src.Currency, true))));

        // Mapping for Booking and its nested objects.
        CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.CurrentStatus.ToString()))
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room))
            .ForMember(dest => dest.GuestId, opt => opt.MapFrom(src => src.Guest));

        CreateMap<Booking, CreateBookingRequest>()
            .ForMember(dest => dest.GuestId,
                    opt => opt.Ignore())
            .ForMember(dest => dest.GuestId,
                    opt => opt.Ignore())
            .ForMember(dest => dest.Status,
                    opt => opt.Ignore());

        CreateMap<CreateBookingRequest, Booking>()
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Room, opt => opt.Ignore())
            .ForMember(dest => dest.Guest, opt => opt.Ignore());

        CreateMap<Guest, GuestDto>();
        CreateMap<PersonId, PersonId>();
    }
}