using Application.Bookings.Dtos;
using Application.Bookings.Requests;
using Application.Ports;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<BookingDto>> Post(BookingDto guest)
        {
            throw new NotImplementedException();
            var resquest = new CreateBookingRequest
            {
                Data = guest,
            };

            // // var res = await _bookingManager.CreateBooking(resquest);

            // if (res.Success) return Created("", res.Data);

            // if (res.ErrorCode == Application.ErrorCode.NOT_FOUND)
            // {
            //     return BadRequest(res);
            // }
            // else if (res.ErrorCode == Application.ErrorCode.MISSING_REQUIRED_INFORMATION)
            // {
            //     return BadRequest(res);
            // }
            // else if (res.ErrorCode == Application.ErrorCode.COULD_NOT_STORE_DATA)
            // {
            //     return BadRequest(res);
            // }




            // _logger.LogError("Response with unkwn ErrorCode Returned", res);
            // return BadRequest();

        }

        [HttpGet]
        public async Task<ActionResult<BookingDto>> Get(int guestId)
        {
            throw new NotImplementedException();
            // var res = await _bookingManager.GetGuest(guestId);

            // if (res.Success) return Created("", res.Data);

            // return NotFound(res);
        }
    }
}
