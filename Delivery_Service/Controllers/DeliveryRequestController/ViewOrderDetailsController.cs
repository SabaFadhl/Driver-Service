using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Service.Controllers.DeliveryRequestController
{
    [Route("api/OrderDetails")]
    [ApiController]
    public class ViewOrderDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewOrderDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(string orderId)
        {
            #region Validation Fields
            if (string.IsNullOrWhiteSpace(orderId))
            {
                return BadRequest(new { errorMessage = "The orderId is null." });
            }             
            #endregion

            try
            {               
                ViewOrderDetailsByOrderIdDto requestForDeliveries = await _unitOfWork.GetOrderDetailsAsync(orderId);                
                return Ok(requestForDeliveries);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
