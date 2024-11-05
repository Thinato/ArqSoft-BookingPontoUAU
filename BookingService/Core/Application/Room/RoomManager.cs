using Application.Ports;
using Application.Rooms.Dtos;
using Application.Rooms.Requests;
using Application.Rooms.Responses;
using AutoMapper;
using Domain.Rooms.Entities;
using Domain.Rooms.Ports;
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

        async Task<RoomResponse> IRoomManager.Create(CreateRoomRequest request)
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
            var repoRoom = await _repository.GetRoom(roomId);

            var result = new RoomDto()
            {
                Price = _mapper.Map(repoRoom.Price, new PriceDto())
            };

            return new RoomResponse
            {
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
                Data = data,
                Pagination = roomsFetch.Item2
            };

            return result;
        }

        public async Task<bool> PutInMaintanence(int roomId)
        {
            var repoRoom = await _repository.GetRoom(roomId);

            if (repoRoom.InMaintenance)
            {
                return true;
            }

            var sucess = repoRoom.PutInMaintanance();

            if (sucess)
            {
                await _repository.UpdateRoom(repoRoom);
                return true;
            }

            return false;
        }
    }
}