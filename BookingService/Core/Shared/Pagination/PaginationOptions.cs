namespace Shared.Pagination
{
    public record PaginationQuery(
        int Page,
        int Count
    );

    public readonly record struct PaginationOptions
    {
        public required int Offset { get; init; }
        public required int Take { get; init; }
    }

    public static class PaginationExtension
    {
        public static PaginationOptions ToOptions(
                this PaginationQuery query)
        {
            return new PaginationOptions()
            {
                Offset = (query.Page - 1) * query.Count,
                Take = query.Count,
            };
        }
    }
}