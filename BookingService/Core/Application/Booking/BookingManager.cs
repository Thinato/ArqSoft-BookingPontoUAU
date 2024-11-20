using Application.Bookings.Dtos;
using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Application.Errors;
using Application.Ports;
using AutoMapper;
using Domain.Bookings.Entities;
using Domain.Bookings.Enums;
using Domain.Bookings.Ports;
using Domain.Guests.Ports;
using Domain.Rooms.Ports;
using Domain.Rooms.ValueObjects;
using Shared.Pagination;

namespace Application.Bookings
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _repo;
        private readonly IGuestRepository _guestRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;

        public BookingManager(
                IBookingRepository repository,
                IGuestRepository guestRepository,
                IRoomRepository roomRepository)
        {
            _repo = repository;
            _guestRepo = guestRepository;
            _roomRepo = roomRepository;

            var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<BookingMapping>();
                });
            _mapper = new Mapper(mapConfig);
        }

        public async Task<BookingResponse> Create(CreateBookingRequest request)
        {
            var guest = await _guestRepo.Get(request.GuestId)
                    ?? throw new NotFoundException("Guest not found.");

            var room = await _roomRepo.GetRoom(request.RoomId)
                    ?? throw new NotFoundException("Room not found.");
            
            var occupationResult = room.Occupy();
            if (occupationResult is OccupyResult.Failed f)
                throw new OccupationOpException("Room was unavailable.", f);
            

            var status = Enum.Parse<Status>(request.Status, true);

            var newBooking = new Booking()
            {
                Guest = guest,
                Room = room,
                Status = status
            };

            _mapper.Map(request, newBooking);

            await _roomRepo.UpdateRoom(room);
            var savedBooking = await _repo.Create(newBooking);

            return new BookingResponse()
            {
                Success = true,
                Data = BookingDto.MapToDto(savedBooking)
            };
        }

        public async Task<BookingResponse> GetBooking(int bookingId)
        {
            var booking = await _repo.Get(bookingId)
                    ?? throw new NotFoundException("Booking not found.");

            return new BookingResponse()
            {
                Success = true,
                Data = BookingDto.MapToDto(booking)
            };
        }

        public async Task<BookingResponseList> GetBookings(PaginationQuery pagination)
        {
            var bookingsFetch = await _repo.ListBookings(pagination);

            return new BookingResponseList()
            {
                Data = bookingsFetch.Item1.Select(BookingDto.MapToDto),
                Pagination = bookingsFetch.Item2
            };
        }

        public async Task<BookingResponseList> GetBookingsByGuest(int guestId, PaginationQuery pagination)
        {
            var bookingsList = await _repo.ListBookingsByGuest(guestId, pagination);

            return new BookingResponseList()
            {
                Success = true,
                Data = bookingsList.Item1.Select(BookingDto.MapToDto),
                Pagination = bookingsList.Item2
            };
        }

        public async Task<BookingResponseList> GetBookingsByRoom(int roomId, PaginationQuery pagination)
        {
            var bookingsList = await _repo.ListBookingsByRoom(roomId, pagination);

            return new BookingResponseList()
            {
                Success = true,
                Data = bookingsList.Item1.Select(BookingDto.MapToDto),
                Pagination = bookingsList.Item2
            };
        }

        public Task<BookingResponse> PayBooking(CreateBookingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}