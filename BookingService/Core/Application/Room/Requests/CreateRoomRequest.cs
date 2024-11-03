namespace Application.Rooms.Requests
{
    public readonly record struct CreateRoomRequest(
        string Name,
        int Level,
        string Currency,
        decimal Value
    );
}