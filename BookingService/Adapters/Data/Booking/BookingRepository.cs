using Data.Pagination;
using Domain.Bookings.Entities;
using Domain.Bookings.Ports;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Data.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _dbContext;
        private readonly DbSet<Booking> _dbSet;
        private readonly PaginationService<Booking> _paginationService;

        public BookingRepository(
                HotelDbContext dbContext,
                PaginationService<Booking> paginationService)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Bookings;
            _paginationService = paginationService;
        }

        public async Task<Booking> Create(Booking booking)
        {
            _dbSet.Add(booking);
            await _dbContext.SaveChangesAsync();

            return booking;
        }

        public async Task<Booking?> Get(int bookingId)
        {
            var booking = await _dbSet.SingleOrDefaultAsync(b =>
                    b.Id.Equals(bookingId));
            
            return booking;
        }

        public async Task<(IEnumerable<Booking>, PaginationInfo)> ListBookings(
                PaginationQuery pagination)
        {
            var options = pagination.ToOptions();

            return await _paginationService.Paginate(_dbSet, options);
        }

        public async Task<(IEnumerable<Booking>, PaginationInfo)> ListBookingsByGuest(
                int guestId,
                PaginationQuery pagination)
        {
            var query = _dbSet.Include(b => b.Guest)
                    .Where(b => b.Guest.Id.Equals(guestId));

            var options = pagination.ToOptions();

            return await _paginationService.Paginate(query, options);
        }

        public async Task<(IEnumerable<Booking>, PaginationInfo)> ListBookingsByRoom(
                int roomId,
                PaginationQuery pagination)
        {
            var query = _dbSet.Include(b => b.Room)
                    .Where(b => b.Room.Id.Equals(roomId));
            
            var options = pagination.ToOptions();

            return await _paginationService.Paginate(query, options);
        }
    }
}