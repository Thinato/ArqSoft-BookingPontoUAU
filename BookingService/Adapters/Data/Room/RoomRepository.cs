using System.Text.Json;
using System.Text.Json.Serialization;
using Data.Pagination;
using Domain.Rooms.Entities;
using Domain.Rooms.Ports;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Data.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        private readonly PaginationService _paginationService;

        public RoomRepository(
                HotelDbContext hotelDbContext,
                PaginationService paginationService)
        {
            _hotelDbContext = hotelDbContext;
            _paginationService = paginationService;
        }

        public async Task<Room> Create(Room room)
        {

            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room;
        }

        public async Task<Room?> GetRoom(int roomId)
        {
            return await _hotelDbContext.Rooms.SingleOrDefaultAsync(r => r.Id == roomId);
        }

        public async Task<bool> IsRoomAvailable(int roomId, DateTime startDate, DateTime endDate)
        {
            return false; // TODO: implement when bookings are implemented.
        }

        public async Task<(IEnumerable<Room>, PaginationInfo)> ListRooms(PaginationQuery pagination)
        {
            var query = _hotelDbContext.Rooms;
            var options = pagination.ToOptions();

            var result = await _paginationService.Paginate(query, options);

            return result;
        }

        public async Task<Room?> UpdateRoom(Room room)
        {
            var currentRoom = await _hotelDbContext.Rooms.SingleOrDefaultAsync(r => r.Id.Equals(room.Id));

            if (currentRoom is null)
                return null;

            _hotelDbContext.Entry(currentRoom).CurrentValues.SetValues(room);
            await _hotelDbContext.SaveChangesAsync();

            return room;
        }
    }
}