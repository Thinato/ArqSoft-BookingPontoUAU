using Application.Guests.Dtos;
using Application.Guests.Requests;
using Application.Ports;
using Application.Responses;
using Domain.Guests.Entities;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace API.Controllers
{
    [ApiController]
    [Route("guest")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _guestManager;

        public GuestController(
            ILogger<GuestController> logger,
            IGuestManager guestManager)
        {
            _logger = logger;
            _guestManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDto>> Post(GuestDto guest)
        {
            var request = new CreateGuestRequest
            {
                Data = guest,
            };

            var res = await _guestManager.CreateGuest(request);

            if (res.Success) return Created("", res.Data);

            if (res.ErrorCode == Application.ErrorCode.NOT_FOUND)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.INVALID_PERSON_ID)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == Application.ErrorCode.INVALID_EMAIL)
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

        [HttpGet]
        public async Task<ActionResult<GuestDto>> Get(int id)
        {
            var res = await _guestManager.GetGuest(id);

            if (res.Success) return Created("", res.Data);

            return NotFound(res);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var res = await _guestManager.DeleteGuest(id);

            if (res.Success) return Ok(true);

            return NotFound(res);
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<GuestListResponse>> GetMany(
                [FromQuery] PaginationQuery query)
        {
            var result = await _guestManager.GetManyGuests(query);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<GuestResponse>> Update(
            [FromQuery] int guestId,
            UpdateGuestRequest request)
        {
            var res = await _guestManager.UpdateGuest(guestId, request);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }
    }
}
