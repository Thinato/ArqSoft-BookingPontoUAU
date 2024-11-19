using Application.Guests.Requests;
using Application.Responses;
using Shared.Pagination;

namespace Application.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
        Task<GuestResponse> GetGuest(int guestId);
        Task<GuestListResponse> GetManyGuests(PaginationQuery pagination);
    }
}
