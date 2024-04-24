using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace Delivery_Service.Controllers.DeliveryRequestController
{
    [Route("api/AcceptDeliveryRequest")]
    [ApiController]
    public class ChangeDeliveryRequestStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDeliveryRequestStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// This API to allow Driver to accept(Picked Up) DeliveryRequest.
        /// </summary>
        /// <param name="driverId">Driver Id</param>
        /// <param name="deliveryRequestId">DeliveryRequestId</param>
        /// <returns></returns>

        [HttpPut("{deliveryRequestId}")]
        public async Task<IActionResult> ChangeDeliveryRequestStatus(string deliveryRequestId, [FromBody] ChangeOrderStatusDto changeOrderStatusDto)
        {
            #region Validation Fields
            //if (string.IsNullOrWhiteSpace(driverId))
            //{
            //    return BadRequest(new { errorMessage = "The driverId is null." });
            //}
            if (string.IsNullOrWhiteSpace(deliveryRequestId))
            {
                return BadRequest(new { errorMessage = "The deliveryRequestId is null." });
            }
            if (changeOrderStatusDto == null)
            {
                return BadRequest(new { errorMessage = "The ChangeOrderStatusDto is null." });
            }
            else
            {
                string pattern = @"(pickedup|onway|delivered)";
                Match match = Regex.Match(changeOrderStatusDto.Status, pattern, RegexOptions.IgnoreCase);

                if (!match.Success)
                {
                    return BadRequest(new { errorMessage = "The status must be one of (pickedup, onway, delivered)." });
                }
            }
            
            #endregion

            try
            {
                DeliveryRequest deliveryRequest = await _unitOfWork.RequestForDelivery.SingleOrDefaultAsync(x => x.Id == deliveryRequestId);

                if (deliveryRequest != null)
                {
                    deliveryRequest.Status = "pickedup";
                    _unitOfWork.RequestForDelivery.Update(deliveryRequest);
                    await _unitOfWork.SaveChangesAsync();

                    // This to change Order status on OrderService.
                     _unitOfWork.ChangeOrderStatusAsync(deliveryRequest.OrderId, deliveryRequest.Status);

                    return Ok();
                }
                else
                {
                    return NotFound(new { ErrorMessag = "There is no DeliveryRequest with DeliveryRequestId '" + deliveryRequestId + "'" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
