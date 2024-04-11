using Delivery_Service.Application.Dto.Common;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace DeliveryService.Controllers.DeliveryController
{
    [ApiController]
    [Route("api/Delivery")]
    public class AddDeliveryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddDeliveryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDriverDto addDeliveryDto)
        {
            #region Validation Fields
            if (addDeliveryDto == null)
            {
                return BadRequest(new { errorMessage = "The Driver is null." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.Name))
            {
                return BadRequest(new { errorMessage = "You must enter Name of the Driver." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter Email of the Driver." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter PhoneNumber of the Driver." });
            }
            else
            {
                if (!Regex.IsMatch(addDeliveryDto.PhoneNumber, @"^\+967\s\d{9}$"))
                {
                    return BadRequest(new { errorMessage = "Invalid PhoneNumber, The PhoneNumber must be like this format: +967 000000000" });
                }
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password of the Driver." });
            }
            #endregion

            try
            {
                if ((await _unitOfWork.GetRepository<Driver>().SingleOrDefaultAsync(c => c.Name == addDeliveryDto.Name || c.Email == addDeliveryDto.Email)) != null)
                {
                    return BadRequest(new { errorMessage = "This Driver already exists with the same name or email." });
                }

                Driver Delivery = new Driver
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = addDeliveryDto.Name,
                    Email = addDeliveryDto.Email,
                    Password = addDeliveryDto.Password,
                    PhoneNumber = addDeliveryDto.PhoneNumber
                };

                _unitOfWork.GetRepository<Driver>().Add(Delivery);

                await _unitOfWork.SaveChangesAsync();

                return StatusCode(201, new ReturnGuidDto { Id = Delivery.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
