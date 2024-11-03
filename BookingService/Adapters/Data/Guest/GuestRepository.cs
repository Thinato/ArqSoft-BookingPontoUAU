using Domain.Guests.Entities;
using Domain.Guests.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guests
{
    public class GuestRepository : IGuestRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;

        }

        async Task<Guest> IGuestRepository.Create(Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest;
        }

        async Task<Guest?> IGuestRepository.Get(int Id)
        {
            return await _hotelDbContext.Guests?.Where(g => g.Id == Id).SingleOrDefaultAsync()!;
        }
    }
}
