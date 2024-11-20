using System.Runtime.CompilerServices;
using System.Text.Json;
using Application.Errors;
using Application.Ports;
using Application.Rooms.Dtos;
using Application.Rooms.Requests;
using Application.Rooms.Responses;
using AutoMapper;
using Domain.Rooms.Entities;
using Domain.Rooms.Ports;
using Domain.Rooms.ValueObjects;
using Shared.Pagination;

namespace Application.Rooms
{
    public class RoomManager : IRoomManager
    {
        private IRoomRepository _repository;
        private IMapper _mapper;

        public RoomManager(IRoomRepository repository)
        {
            _repository = repository;

            var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<RoomMapping>();
                });
            _mapper = new Mapper(mapConfig);
        }

        public async Task<RoomResponse> Create(CreateRoomRequest request)
        {
            var newRoom = new Room();
            _mapper.Map(request, newRoom);

            newRoom.InMaintenance = false;
            newRoom.HasGuest = false;

            await _repository.Create(newRoom);

            var result = RoomDto.MapToDto(newRoom);

            return new RoomResponse
            {
                Success = true,
                Data = result
            };
        }

        public async Task<RoomResponse> GetRoom(int roomId)
        {
            var repoRoom = await _repository.GetRoom(roomId)
                    ?? throw new NotFoundException("Room not found.");

            var result = new RoomDto()
            {
                Price = _mapper.Map(repoRoom.Price, new PriceDto())
            };

            _mapper.Map(repoRoom, result);

            return new RoomResponse
            {
                Success = result != null,
                Data = result
            };
        }

        public async Task<RoomResponseList> GetRooms(PaginationQuery pagination)
        {
            var roomsFetch = await _repository.ListRooms(pagination);

            var data = roomsFetch.Item1.Select(r =>
            {
                var dto = new RoomDto()
                {
                    Price = _mapper.Map(r.Price, new PriceDto()),
                };

                _mapper.Map(r, dto);
                return dto;
            });

            var result = new RoomResponseList()
            {
                Success = data.Any(),
                Data = data,
                Pagination = roomsFetch.Item2
            };

            return result;
        }

        public async Task<RoomResponse> OccupyDesoccupyRoom(int roomId, OccupationOpQuery query)
        {
            var room = await _repository.GetRoom(roomId)
                ?? throw new NotFoundException("Room not found.");

            Func<OccupyResult> operation = query.Operation switch {
                OccupationOp.Occupy => room.Occupy,
                OccupationOp.Disocuppy => room.Disoccupy,
                _ => throw new Exception("Operation not acceptable."),
            };

            var result = operation.Invoke();

            return result switch {
                OccupyResult.Succeeded s => new RoomResponse { Success = true, Data = RoomDto.MapToDto(s.Room) },
                OccupyResult.Failed f => throw new OccupationOpException("Operation failed.", f),
                _ => throw new Exception(),
            };
        }

        public async Task<RoomResponse> UpdateRoom(int roomId, UpdateRoomRequest request)
        {
            var room = await _repository.GetRoom(roomId)
                    ?? throw new NotFoundException("Room not found.");
            
            _mapper.Map(request, room);

            var savedRoom = await _repository.UpdateRoom(room)
                    ?? throw new UpdateException("Invalid room state for update.");

            return new RoomResponse()
            {
                Data = RoomDto.MapToDto(savedRoom),
            };
        }
    }
}