using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thynk_Code.Model.Entities;
using Thynk_Code.Model.ServiceModels;
using Thynk_Code.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Thynk_Code.Controllers
{
    [Route("api/[controller]")]
    public class PcrController : BaseAPIController
    {
        IPcrService _pcrService;
        public PcrController(IPcrService pcrService)
        {
            _pcrService = pcrService;
        }


        [HttpPost("allocateSpace")]
        public async Task<IActionResult> AllocateSpace([FromBody] SpaceAllocationModel model)
        {
            try
            {
                SpaceAllocation spaceAllocation = await _pcrService.CreateAndAllocateSpace(model);
                return Ok(spaceAllocation);

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("set_vailability")]
        public async Task<IActionResult> SetAvailability(AvailabilityModel availabilityModel)
        {
            try
            {
                await _pcrService.SetAvailability(availabilityModel.spaceId, availabilityModel.isAvailable);
                return Ok();

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_all_availablespace")]
        public async Task<IActionResult> GetAllAvailableSpace()
        {
            try
            {
                IEnumerable<SpaceAllocation> spaces = await _pcrService.GetAllAvalableSpace();
                return Ok(spaces);

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_bookings")]
        public async Task<IActionResult> GetBookings()
        {
            try
            {
                IEnumerable<BookingDetail> bookings = await _pcrService.Getbookings();
                return Ok(bookings);

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get_bookings_user")]
        public async Task<IActionResult> GetBookingsByUser(string userId)
        {
            try
            {
                IEnumerable<BookingDetail> bookings = await _pcrService.GetbookingsByUser(userId);
                return Ok(bookings);

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("bookSpace")]
        public async Task<IActionResult> BookSpace([FromBody] BookingModel bookingModel)
        {
            try
            {
                 await _pcrService.BookSpace(bookingModel);
                return Ok();

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cancelbooking")]
        public async Task<IActionResult> Cancelbooking([FromBody] UpdateBookingModel setCancelModel)
        {
            try
            {
                await _pcrService.Cancelbooking(setCancelModel.userId, setCancelModel.bookingId);
                return Ok();

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("setLabResult")]
        public async Task<IActionResult> SetLabResult([FromBody] UpdateBookingModel setCancelModel)
        {
            try
            {
                await _pcrService.SetLabResult(setCancelModel.userId, setCancelModel.bookingId, setCancelModel.testStatus.Value);
                return Ok();

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("report")]
        public async Task<IActionResult> Report()
        {
            try
            {
                ReportModel reportModel = await _pcrService.Report();
                return Ok(reportModel);

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }
    }
}
