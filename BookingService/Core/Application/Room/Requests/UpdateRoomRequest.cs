namespace Application.Rooms.Requests
{
    public readonly record struct UpdateRoomRequest(
        string? Name,
        int? Level,
        string? Currency,
        decimal? Value
    );
}