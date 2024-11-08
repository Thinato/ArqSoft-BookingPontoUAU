﻿using System.Text.Json.Serialization;
using Application.Guests.Dtos;
using Application.Guests.Requests;
using Application.Ports;
using Application.Responses;
using AutoMapper;
using Domain.Guests.Exceptions;
using Domain.Guests.Ports;

namespace Application.Guests
{
    public class GuestManager : IGuestManager
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IMapper _mapper;

        public GuestManager(IGuestRepository guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }
        public async Task<GuestResponse> CreateGuest(CreateGuestRequest request)
        {
            try
            {
                var guest = GuestDto.MapToEntity(request.Data);

                guest.ValidateState();

                request.Data.Id = guest.Id;
                var createdGuest = await _guestRepository.Create(guest);

                var createdGuestDto = GuestDto.MapToDto(guest);
                // to com preguica de entender esse auto mapper
                // var createdGuestDto = new GuestDto();
                // _mapper.Map(createdGuest, createdGuestDto);

                return new GuestResponse
                {
                    Data = createdGuestDto,
                    Success = true,
                };
            }
            catch (InvalidPersonDocumentIdException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_PERSON_ID,
                    Message = "The passed ID is not valid"
                };
            }
            catch (MissingRequiredInformation)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.MISSING_REQUIRED_INFORMATION,
                    Message = "Missing passed required information"
                };
            }
            catch (InvalidEmailException)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.INVALID_EMAIL,
                    Message = "The given email is not valid"
                };
            }
            catch (Exception)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<GuestResponse> GetGuest(int guestId)
        {
            var guest = await _guestRepository.Get(guestId);

            if (guest is null)
            {
                return new GuestResponse
                {
                    Success = false,
                    ErrorCode = ErrorCode.GUEST_NOT_FOUND,
                    Message = "No guest record was found with the given id"
                };
            }

            return new GuestResponse
            {
                Success = true,
                Data = GuestDto.MapToDto(guest),
            };
        }
    }
}
