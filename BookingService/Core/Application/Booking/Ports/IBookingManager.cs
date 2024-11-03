using Application.Bookings.Requests;
using Application.Bookings.Responses;
using Shared.Pagination;

namespace Application.Ports
{
    public interface IBookingManager
    {
        public Task<BookingResponse> Create(CreateBookingRequest request);
        public Task<BookingResponseList> GetBookings(PaginationQuery pagination);
        public Task<BookingResponse> GetBooking(int roomId);
        public Task<BookingResponse> PayBooking(CreateBookingRequest request);
    }
}

