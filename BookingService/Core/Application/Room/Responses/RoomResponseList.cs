
using Application.Rooms.Dtos;
using Shared.Pagination;

namespace Application.Rooms.Responses
{
    public class RoomResponseList : Response
    {
        public IEnumerable<RoomDto>? Data;
        public PaginationInfo? Pagination;
    }
}