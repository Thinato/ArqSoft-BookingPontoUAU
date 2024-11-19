using Data.Pagination;
using Domain.Guests.Entities;
using Domain.Guests.Ports;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Data.Guests
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        private readonly PaginationService _paginationService;

        public GuestRepository(
                HotelDbContext hotelDbContext,
                PaginationService paginationService)
        {
            _hotelDbContext = hotelDbContext;
            _paginationService = paginationService;
        }

        public async Task<(IEnumerable<Guest>, PaginationInfo)> GetPaginated(PaginationQuery pagination)
        {
            var query = _hotelDbContext.Guests.Where(g => g.DeletedAt == null);
            var result = await _paginationService.Paginate(
                query,
                pagination.ToOptions());

            return result;
        }

        public async Task<Guest?> Update(Guest guest)
        {
            var currentGuest = await _hotelDbContext.Guests.SingleOrDefaultAsync(g =>
                g.Id.Equals(guest.Id) &&
                g.DeletedAt == null);

            if (currentGuest is null)
                return null;

            _hotelDbContext.Entry(currentGuest).CurrentValues.SetValues(guest);
            await _hotelDbContext.SaveChangesAsync();

            return guest;
        }

        async Task<Guest> IGuestRepository.Create(Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest;
        }

        async Task<Guest?> IGuestRepository.Get(int Id)
        {
            return await _hotelDbContext.Guests?.Where(g => g.Id == Id && g.DeletedAt == null).SingleOrDefaultAsync()!;
        }
    }
}
