using Domain.Rooms.Entities;
using Shared.Pagination;

namespace Domain.Rooms.Ports;
public interface IRoomRepository
{
    Task<bool> IsRoomAvailable(int roomId, DateTime startDate, DateTime endDate);
    Task<Room> Create(Room room);
    Task<Room?> GetRoom(int roomId);
    Task<(IEnumerable<Room>, PaginationInfo)> ListRooms(PaginationQuery pagination);
    Task<Room> UpdateRoom(Room room);
}
