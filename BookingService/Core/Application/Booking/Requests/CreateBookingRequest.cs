namespace Application.Bookings.Requests;

public readonly record struct CreateBookingRequest(
    DateTime PlacedAt,
    DateTime Start,
    DateTime End,
    int RoomId,
    int GuestId,
    string Status
);

