using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Delivery_Service.Controllers.DriverController
{
    [Route("api/Driver")]
    [ApiController]
    public class UpdateDriverController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDriverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// To allow Driver or system admin to Update Driver information.
        /// </summary>
        /// <param name="driverId">Driver identity</param>
        /// <param name="updateDriverDto">Driver information.</param>
        /// <returns></returns>
        [HttpPut("{driverId}")]
        public async Task<IActionResult> Update(string driverId, UpdateDriverDto updateDriverDto)
        {
            #region Validation Fields
            if (updateDriverDto == null)
            {
                return BadRequest(new { errorMessage = "The UpdateDriverDto is null." });
            }
            if (string.IsNullOrWhiteSpace(updateDriverDto.Name))
            {
                return BadRequest(new { errorMessage = "You must enter Name of the Driver." });
            }
            if (string.IsNullOrWhiteSpace(updateDriverDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter Email of the Driver." });
            }

            if (string.IsNullOrWhiteSpace(updateDriverDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter PhoneNumber of the Driver." });
            }
            else
            {
                if (!Regex.IsMatch(updateDriverDto.PhoneNumber, @"^\+967\s\d{9}$"))
                {
                    return BadRequest(new { errorMessage = "Invalid PhoneNumber, The PhoneNumber must be like this format: +000 000000000" });
                }
            }
            if (string.IsNullOrWhiteSpace(updateDriverDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password of the Driver." });
            }
            #endregion

            try
            {
                Driver driver = await _unitOfWork.Driver.GetById(driverId);
                if (driver != null)
                {
                    driver.Name = updateDriverDto.Name;
                    driver.Email = updateDriverDto.Email;
                    driver.Password = updateDriverDto.Password;
                    driver.PhoneNumber = updateDriverDto.PhoneNumber;
                    driver.UpdateTime = DateTime.Now;

                    _unitOfWork.Driver.Update(driver);

                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound(new { ErrorMesssage = "There is no Driver with given Id." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
