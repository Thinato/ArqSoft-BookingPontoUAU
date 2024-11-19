namespace Domain.Room.ValueObjects
{
    public abstract record DisoccupyResult
    {
        public record Failed(): DisoccupyResult;
        public record Succeeded(): DisoccupyResult;
    }
}