using Application.Bookings.Dtos;
using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Application.Ports;
using Domain.Bookings.Ports;
using Shared.Pagination;

namespace Application.Bookings
{
    public class BookingManager : IBookingManager
    {
        private IBookingRepository _repo;

        public BookingManager(IBookingRepository repository)
        {
            _repo = repository;
        }

        public Task<BookingResponse> Create(CreateBookingRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<BookingResponse> GetBooking(int roomId)
        {
            throw new NotImplementedException();
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

        public Task<BookingResponse> PayBooking(CreateBookingRequest request)
        {
            throw new NotImplementedException();
        }
    }
}