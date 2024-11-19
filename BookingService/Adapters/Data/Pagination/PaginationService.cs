using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Data.Pagination
{
    public class PaginationService
    {
        public async Task<(IEnumerable<TEntity>, PaginationInfo)> Paginate<TEntity>(
                IQueryable<TEntity> query,
                PaginationOptions pagination)
        {
            var totalItems = await query.CountAsync();

            if (totalItems == 0)
                return (new List<TEntity>(), new PaginationInfo
                {
                    Items = 0,
                    CurrentPage = 1,
                    TotalPages = 1
                });

            if (totalItems <= pagination.Offset)
                throw new Exception("Offset exceeds maximum of items."); // TODO: create exception type and treat it on handler

            query = query.Skip(pagination.Offset).Take(pagination.Take);

            var paginationInfo = new PaginationInfo
            {
                Items = totalItems,
                CurrentPage = pagination.Offset / pagination.Take + 1,
                TotalPages = (int)Math.Ceiling((double)totalItems / pagination.Take),
            };

            var data = await query.ToListAsync();

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