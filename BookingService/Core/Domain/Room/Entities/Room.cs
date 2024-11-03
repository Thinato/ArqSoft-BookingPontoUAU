using Domain.Rooms.ValueObjects;

namespace Domain.Rooms.Entities;
public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool InMaintenance { get; set; }
    public bool HasGuest { get; set; }

    public Price Price { get; set; }

    public bool IsAvailable => InMaintenance || HasGuest;


    public void Occupy() => HasGuest = true;
    public void Disoccupy() => HasGuest = false;

    public bool PutInMaintanance() => InMaintenance = true;
    public bool EndMaintanance() => InMaintenance = false;
}
