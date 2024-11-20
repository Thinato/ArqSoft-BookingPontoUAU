using Application.Bookings.Dtos;
using Application.Bookings.Requests;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace API.Controllers
{
    [ApiController]
    [Route("booking")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IBookingManager _bookingManager;

        public BookingController(
            ILogger<GuestController> logger,
            IBookingManager guestManager)
        {
            _logger = logger;
            _bookingManager = guestManager;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> Create(CreateBookingRequest request)
        {
            var res = await _bookingManager.Create(request);

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

        [HttpGet]
        public async Task<ActionResult<BookingDto>> Get([FromQuery] int id)
        {
            var res = await _bookingManager.GetBooking(id);

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAll([FromQuery] PaginationQuery pagination)
        {
            var res = await _bookingManager.GetBookings(new PaginationQuery(pagination.Page, pagination.Count));

            if (res.Success) return Ok(res.Data);

            return NotFound(res);
        }

        [HttpPut("pay")]
        public async Task<ActionResult<BookingDto>> PayBooking(CreateBookingRequest request)
        {
            var res = await _bookingManager.PayBooking(request);

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
    }
}
