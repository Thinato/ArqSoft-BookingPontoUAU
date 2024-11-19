using Domain.Bookings.Entities;
using Domain.Room.ValueObjects;
using Domain.Rooms.ValueObjects;

namespace Domain.Rooms.Entities;
public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public bool HasGuest { get; set; }

    public Price? Price { get; set; }

    public bool IsAvailable => InMaintenance || HasGuest;

    public IEnumerable<Booking> Bookings = [];

    public OccupyResult Occupy()
    {
        if (!IsAvailable)
            return new OccupyResult.Failed(HasGuest, InMaintenance);
        
        HasGuest = true;

        return new OccupyResult.Succeeded();
    }

    public DisoccupyResult Disoccupy()
    {
        if (IsAvailable)
            return new DisoccupyResult.Failed();
        
        HasGuest = false;

        return new DisoccupyResult.Succeeded();
    }

    public bool PutInMaintanance() => InMaintenance = true;
    public bool EndMaintanance() => InMaintenance = false;

    public override string ToString()
    {
        return $"Id: {Id},\nName: {Name},\nLevel: {Level},\nInMaintenance: {InMaintenance},\nHasGuest: {HasGuest},\nPrice: {{ {Price} }}";
    }
}
