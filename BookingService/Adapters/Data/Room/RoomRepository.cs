using Data.Pagination;
using Domain.Rooms.Entities;
using Domain.Rooms.Ports;
using Shared.Pagination;

namespace Data.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        private readonly PaginationService<Room> _paginationService;

        public RoomRepository(
                HotelDbContext hotelDbContext,
                PaginationService<Room> paginationService)
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

        public Task<Room> GetRoom(int roomId)
        {
            throw new NotImplementedException();
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

        public Task<Room> UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }
    }
}