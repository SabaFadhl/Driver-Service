using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Service.Controllers.DriverController
{
    [Route("api/Driver")]
    [ApiController]
    public class GetDriverListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDriverListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This API to allow system admin to view all available Drivers
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageIndex, int pageSize)
        {
            try
            {
                List<Driver> drivers = await _unitOfWork.Driver.GetAllPageing(pageIndex, pageSize);

                List<ViewDriverDto> viewDriverDtos = new();
                foreach (Driver item in drivers)
                {
                    viewDriverDtos.Add(new ViewDriverDto
                    {
                        CreateTime = item.CreateTime,
                        Email = item.Email,
                        Id = item.Id,
                        Name = item.Name,
                        AvailabilityStatus = item.AvailabilityStatus,
                        PhoneNumber = item.PhoneNumber,
                        UpdateTime = item.UpdateTime,
                    });
                }
                return Ok(viewDriverDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
