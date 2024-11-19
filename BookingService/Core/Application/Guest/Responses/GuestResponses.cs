using Application.Guests.Dtos;
using Shared.Pagination;

namespace Application.Responses
{
    public class GuestResponse : Response
    {
        public GuestDto Data;
    }

    public class GuestListResponse : Response
    {
        public required IEnumerable<GuestDto> Data;
        public required PaginationInfo PaginationInfo;
    }
}
