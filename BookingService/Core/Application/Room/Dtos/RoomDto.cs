using Domain.Rooms.Entities;
using Domain.Rooms.ValueObjects;

namespace Application.Rooms.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public PriceDto Price { get; set; }

    public RoomDto() {}

    public static Room MapToEntity(Room dto)
    {
        return new Room
        {
            Id = dto.Id,
            HasGuest = dto.HasGuest,
            InMaintenance = dto.InMaintenance,
            Level = dto.Level,
            Name = dto.Name,
            Price = new Price(dto.Price.Value, dto.Price.Currency)
        };

    }

    public static Room MapToDto(Room room)
    {
        return new Room
        {
            Id = room.Id,
            HasGuest = room.HasGuest,
            InMaintenance = room.InMaintenance,
            Level = room.Level,
            Name = room.Name,
            Price = new Price(room.Price.Value, room.Price.Currency)
        };
    }
}

