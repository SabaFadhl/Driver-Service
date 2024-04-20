using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Service.Controllers.DriverController
{
    [Route("api/GetDriverOrdersList")]
    [ApiController]
    public class GetDriverOrdersListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDriverOrdersListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This API Allowing the driver to view the current orders he has.
        /// </summary>
        /// <param name="driverId"></param>         
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] string driverId)
        {
            try
            {
                List<ViewDriverOrderDto> viewDriverOrderDtos = await _unitOfWork.GetDriverOrdersListAsync(driverId);               
                
                return Ok(viewDriverOrderDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
