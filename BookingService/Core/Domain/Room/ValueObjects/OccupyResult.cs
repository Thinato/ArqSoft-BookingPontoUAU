namespace Domain.Room.ValueObjects
{
    public abstract record OccupyResult()
    {
        public record Failed(bool IsOccupied, bool InMaintenance) : OccupyResult;
        public record Succeeded(): OccupyResult;
    }
}