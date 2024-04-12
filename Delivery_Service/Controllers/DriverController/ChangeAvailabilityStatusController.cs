using DeliveryService.Application.Interface;
using Delivery_Service.Domain;
using DeliveryService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Delivery_Service.Application.Dto.Driver;
using System.Text.RegularExpressions;

namespace DeliveryService.Controllers.DeliveryController
{
    [ApiController]
    [Route("api/Driver")]
    public class ChangeAvailabilityStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeAvailabilityStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This API for Changing the driver availablility status
        /// </summary>
        /// <param name="DriverId">The Driver Identiry (Should be GUID/UUID)</param>
        /// <param name="setDriverAvailabilityStatusDto">The Status should be: 'offline' or 'online'</param>      
        /// <returns>NoContent (204)</returns>

        [HttpPut("ChangeAvailabilityStatus/{DriverId}")]
        public async Task<IActionResult> ChangeDeliveryStatus(string DriverId, SetDriverAvailabilityStatusDto setDriverAvailabilityStatusDto)
        {
            #region Validation Fields
            if (setDriverAvailabilityStatusDto == null)
            {
                return BadRequest(new { errorMessage = "The SetDriverAvailabilityStatusDto is null." });
            }
            if (string.IsNullOrWhiteSpace(setDriverAvailabilityStatusDto.Status))
            {
                return BadRequest(new { errorMessage = "You must enter Status." });
            }           
            else
            {
                if (!Regex.IsMatch(setDriverAvailabilityStatusDto.Status, @"^(offline|online)$"))
                {
                    return BadRequest(new { errorMessage = "Invalid Status, The Status should be: 'offline' or 'online'" });
                }
            }
             
            #endregion
            try
            {
                Driver driver = await _unitOfWork.Driver.GetById(DriverId);

                if (driver != null)
                {
                    driver.AvailabilityStatus = setDriverAvailabilityStatusDto.Status;                 
                    _unitOfWork.Driver.Update(driver);
                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound(new { ErrorMessag = "There is no Driver with this Id '" + DriverId + "'" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
