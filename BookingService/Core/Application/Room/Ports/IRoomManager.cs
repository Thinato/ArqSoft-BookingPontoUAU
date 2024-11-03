using Application.Rooms.Dtos;
using Application.Rooms.Requests;
using Application.Rooms.Responses;
using Shared.Pagination;

namespace Application.Ports
{
    public interface IRoomManager
    {
        public Task<RoomResponse> Create(CreateRoomRequest request);
        public Task<bool> PutInMaintanence(int roomId);
        public Task<RoomResponseList> GetRooms(PaginationQuery pagination);
        public Task<RoomResponse> GetRoom(int roomId);
    }
}
