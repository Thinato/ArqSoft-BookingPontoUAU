using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Data.Pagination
{
    public class PaginationService<TEntity>
    {
        public async Task<(IEnumerable<TEntity>, PaginationInfo)> Paginate(
                IQueryable<TEntity> query,
                PaginationOptions pagination)
        {
            var totalItems = await query.CountAsync<TEntity>();

            System.Console.WriteLine($"Total items: {totalItems}");

            if (totalItems <= pagination.Offset)
                throw new Exception("Offset exceeds maximum of items."); // TODO: create exception type and treat it on handler

            query = query.Skip(pagination.Offset).Take(pagination.Take);

            var paginationInfo = new PaginationInfo
            {
                Items = totalItems,
                CurrentPage = pagination.Offset / pagination.Take + 1,
                TotalPages = (int)Math.Ceiling((double)totalItems / pagination.Take),
            };

            var data = await query.ToListAsync<TEntity>();

            return (data, paginationInfo);
        }
    }

    public static class PaginationResultExtension
    {
        public static IEnumerable<TDto> TransformData<TEntity, TDto>(
                this (IEnumerable<TEntity>, PaginationInfo) paginationResult,
                IMapper mapper)
            where TDto : new()
        {
            var data = paginationResult.Item1.Select(e =>
            {
                var dto = new TDto();

                mapper.Map(e, dto);
                return dto;
            });

            return data;
        }
    }
}