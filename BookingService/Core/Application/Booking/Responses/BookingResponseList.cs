using Application.Bookings.Dtos;
using Shared.Pagination;

namespace Application.Bookings.Responses
{
    public class BookingResponseList : Response
    {
        public IEnumerable<BookingDto>? Data;
        public PaginationInfo? Pagination;
    }
}