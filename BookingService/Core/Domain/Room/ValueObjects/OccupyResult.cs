using Domain.Rooms.Entities;

namespace Domain.Rooms.ValueObjects
{
    public abstract record OccupyResult()
    {
        public record Failed(bool IsOccupied, bool InMaintenance) : OccupyResult;
        public record Succeeded(Room Room): OccupyResult;
    }
}