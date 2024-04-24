using Delivery_Service.Application.Dto.Common;
using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Application.Dto.Driver;
 using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using DeliveryService.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Delivery_Service.Controllers.DeliveryRequestController
{
    [Route("api/PickupOrder")]
    [ApiController]
    public class AddPickupOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPickupOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// To allow RestaurantService to send the new orders in order to deliver.
        /// </summary>
        /// <param name="addDeliveryDto">Driver Information.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddPickupOrderDto addPickupOrderDto)
        {

            #region Validation Fields            
            if (addPickupOrderDto == null)
            {
                return BadRequest(new { errorMessage = "The AddPickupOrderDto is null." });
            }
            if (string.IsNullOrWhiteSpace(addPickupOrderDto.OrderId))
            {
                return BadRequest(new { errorMessage = "You should be provide an order Identity." });
            }
            else
            {

            }
            if (string.IsNullOrWhiteSpace(addPickupOrderDto.OrderCode))
            {
                return BadRequest(new { errorMessage = "You should be provide an order Code or Number." });
            }
            if (string.IsNullOrWhiteSpace(addPickupOrderDto.CompoundName))
            {
                return BadRequest(new { errorMessage = "You should be provide an order CompoundName." });
            }             
            #endregion

            try
            {
                DeliveryRequest requestForDelivery = new DeliveryRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = addPickupOrderDto.OrderId,
                    CompoundName = addPickupOrderDto.CompoundName,
                    OrderCode = addPickupOrderDto.OrderCode
                };

                _unitOfWork.RequestForDelivery.Add(requestForDelivery);

                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.AssignOrderToDriver(requestForDelivery.Id);

                return StatusCode(201, new  { deliveryRequestId = requestForDelivery.Id, driverId = requestForDelivery.DriverId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
