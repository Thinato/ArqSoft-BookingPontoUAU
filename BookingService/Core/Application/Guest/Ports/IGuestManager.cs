using Application.Guests.Requests;
using Application.Responses;
using Shared.Pagination;

namespace Application.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
        Task<GuestResponse> GetGuest(int guestId);
        Task<GuestResponse> DeleteGuest(int guestId);
        Task<GuestResponse> UpdateGuest(int id, UpdateGuestRequest request);
        Task<GuestListResponse> GetManyGuests(PaginationQuery pagination);
    }
}
