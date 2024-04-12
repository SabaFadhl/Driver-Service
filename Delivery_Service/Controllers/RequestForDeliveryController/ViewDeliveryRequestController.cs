using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Application.Dto.RequestForDelivery;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Service.Controllers.RequestForDeliveryController
{
    [Route("api/ViewDeliveryRequest")]
    [ApiController]
    public class ViewDeliveryRequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewDeliveryRequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This API to allow Driver to view all Delivery Request assigned to him.
        /// </summary>
        /// <param name="driverId">Driver Id</param>        
        /// <returns></returns>
        [HttpGet("{driverId}")]
        public async Task<IActionResult> GetList(string driverId)
        {
            try
            {
                Driver driver = await _unitOfWork.Driver.GetById(driverId);
                if (driver == null)
                {
                    return BadRequest(new { errorMessage = "There is no Driver with this Id ('"+ driverId + "')" });
                }

                List<RequestForDelivery> requestForDeliveries = await _unitOfWork.RequestForDelivery.FindAsync(x => x.DriverId == driverId && (x.Status.Equals("pending") || x.Status.Equals("pickedup")));

                List<ViewDeliveryRequestDto> viewDeliveryRequestDtos = new();
                foreach (RequestForDelivery item in requestForDeliveries)
                {
                    viewDeliveryRequestDtos.Add(new ViewDeliveryRequestDto
                    {
                        CompoundName = item.CompoundName,
                        CreateTime = item.CreateTime,
                        Id = item.Id,
                        OrderCode = item.OrderCode,
                        OrderId = item.OrderId,
                        Status = item.Status

                    });
                }
                return Ok(viewDeliveryRequestDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
