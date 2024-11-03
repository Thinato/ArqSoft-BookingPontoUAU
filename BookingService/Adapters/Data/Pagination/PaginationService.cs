using Microsoft.EntityFrameworkCore;

namespace Shared.Pagination
{
    public class PaginationService
    {
        public async Task<(IEnumerable<TEntity>, PaginationInfo)> Paginate<TEntity>(
                IQueryable<TEntity> query,
                PaginationOptions pagination)
        {
            var totalItems = await query.CountAsync<TEntity>();

            if (totalItems <= pagination.Offset)
                throw new Exception("Offset exceeds maximum of items."); // TODO: create exception type and treat it on handler

            query = query.Skip(pagination.Offset).Take(pagination.Take);

            var paginationInfo = new PaginationInfo
            {
                Items = totalItems,
                CurrentPage = pagination.Offset / pagination.Take + 1,
                TotalPages = (int)Math.Ceiling((double)totalItems / pagination.Take),
            };

            var data = query.ToList();

            return (data, paginationInfo);
        }
    }
}