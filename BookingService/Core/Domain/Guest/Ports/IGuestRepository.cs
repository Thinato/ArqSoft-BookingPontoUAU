using Domain.Guests.Entities;
using Shared.Pagination;

namespace Domain.Guests.Ports;
public interface IGuestRepository
{
    Task<Guest?> Get(int Id);
    Task<Guest> Create(Guest guest);
    Task<(IEnumerable<Guest>, PaginationInfo)> GetPaginated(PaginationQuery pagination);
}