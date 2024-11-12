using Application.Guests.Dtos;
using Application.Guests.Requests;
using Application.Ports;
using Application.Rooms.Dtos;
using Application.Rooms.Requests;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace API.Controllers
{
    [ApiController]
    [Route("room")]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;

        public RoomController(
            ILogger<RoomController> logger,
            IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDto>> Post(CreateRoomRequest request)
        {
            var res = await _roomManager.Create(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unkwn ErrorCode Returned", res);
            return BadRequest();

        }

        [HttpGet()]
        public async Task<ActionResult<RoomDto>> Get([FromQuery] int id)
        {
            var res = await _roomManager.GetRoom(id);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll([FromQuery] PaginationQuery pagination)
        {
            var res = await _roomManager.GetRooms(new PaginationQuery(pagination.Page, pagination.Count));

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpPut]
        public async Task<ActionResult> PutInMaintenance([FromQuery]int roomId)
        {
            var res = await _roomManager.PutInMaintanence(roomId);

            if (res) return Ok();

            return NotFound();
        }

        [HttpPatch]
        public async Task<ActionResult> Patch(
                [FromQuery] int roomId,
                [FromBody] UpdateRoomRequest payload)
        {
            var res = await _roomManager.UpdateRoom(roomId, payload);
            
            return new OkObjectResult(res);
        }
    }
}
