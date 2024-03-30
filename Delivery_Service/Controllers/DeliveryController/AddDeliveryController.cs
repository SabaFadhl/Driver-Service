using DeliveryService.Application.Dto;
using DeliveryService.Application.Interface;
using DeliveryService.Domain;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Add(AddDeliveryDto addDeliveryDto)
        {
            #region Validation Fields
            if (addDeliveryDto == null)
            {
                return BadRequest(new { errorMessage = "The Delivery is null." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.Name))
            {
                return BadRequest(new { errorMessage = "You must enter Name of the Delivery." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.Email))
            {
                return BadRequest(new { errorMessage = "You must enter Email of the Delivery." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.PhoneNumber))
            {
                return BadRequest(new { errorMessage = "You must enter PhoneNumber of the Delivery." });
            }
            if (string.IsNullOrWhiteSpace(addDeliveryDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password of the Delivery." });
            }
            #endregion

            try
            {
                if ((await _unitOfWork.GetRepository<Delivery>().SingleOrDefaultAsync(c => c.Name == addDeliveryDto.Name || c.Email == addDeliveryDto.Email)) != null)
                {
                    return BadRequest(new { errorMessage = "This Delivery already exists with the same name or email." });
                }

                Delivery Delivery = new Delivery
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = addDeliveryDto.Name,
                    Email = addDeliveryDto.Email,
                    Password = addDeliveryDto.Password,
                    PhoneNumber = addDeliveryDto.PhoneNumber
                };

                _unitOfWork.GetRepository<Delivery>().Add(Delivery);

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
