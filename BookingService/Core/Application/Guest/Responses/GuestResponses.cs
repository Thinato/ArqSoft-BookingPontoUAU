using Application.Guests.Dtos;
using Shared.Pagination;

namespace Application.Responses
{
    public class GuestResponse : Response
    {
        public GuestDto Data { get; set; }
    }

    public class GuestListResponse : Response
    {
        public required IEnumerable<GuestDto> Data { get; set; }
        public required PaginationInfo PaginationInfo { get; set; }
    }
}
