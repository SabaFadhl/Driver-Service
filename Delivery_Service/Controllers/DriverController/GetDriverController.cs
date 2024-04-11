using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Service.Controllers.DriverController
{
    [Route("api/Driver")]
    [ApiController]
    public class GetDriverController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDriverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// This API to allow system admin to view Driver by Id
        /// </summary>
        /// <param name="driverId">Driver identity</param>
        /// <returns></returns>
        [HttpGet("{driverId}")]
        public async Task<ActionResult> Get(string driverId)
        {
            try
            {
                Driver driver = await _unitOfWork.Driver.GetById(driverId);

                if (driver != null)
                {
                    ViewDriverDto viewDriverDto = new ViewDriverDto
                    {
                        PhoneNumber = driver.PhoneNumber,
                        Id = driver.Id,
                        Name = driver.Name,
                        AvailabilityStatus = driver.AvailabilityStatus,
                        Email = driver.Email,
                        CreateTime = DateTime.Now,
                        UpdateTime = driver.UpdateTime,
                    };

                    return Ok(viewDriverDto);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
