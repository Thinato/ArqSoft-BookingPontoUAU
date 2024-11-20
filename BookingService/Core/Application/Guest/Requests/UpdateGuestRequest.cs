using Application.Guests.Dtos;

namespace Application.Guests.Requests;

public readonly record struct UpdateGuestRequest(
    string? Name,
    string? Surname,
    string? Email
)
{ }