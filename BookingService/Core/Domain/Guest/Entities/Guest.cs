﻿using Domain.Guests.Exceptions;
using Domain.Guests.Ports;
using Domain.Guests.ValueObjects;
using Shared;

namespace Domain.Guests.Entities;
public class Guest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public PersonId? DocumentId { get; set; }

    public void ValidateState()
    {
        if (
            DocumentId == null ||
            string.IsNullOrEmpty(DocumentId.IdNumber) ||
            DocumentId.IdNumber.Length <= 3 ||
            DocumentId.DocumentType == 0)
        {
            throw new InvalidPersonDocumentIdException();
        }

        if (string.IsNullOrEmpty(Name) ||
            string.IsNullOrEmpty(Surname) ||
            string.IsNullOrEmpty(Email))
        {
            throw new MissingRequiredInformation();
        }

        if (Utils.ValidateEmail(Email) == false)
        {
            throw new InvalidEmailException();
        }
    }

    public override string ToString()
    {
        return $"Id: {Id},\nName: {Name},\nSurname: {Surname},\nEmail: {Email},\nDocumentId: {{ {DocumentId} }}";
    }
}
