using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Delivery_Service.Controllers.DeliveryRequestController
{
    [Route("api/AcceptDeliveryRequest")]
    [ApiController]
    public class AcceptDeliveryRequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AcceptDeliveryRequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// This API to allow Driver to accept(Picked Up) DeliveryRequest.
        /// </summary>
        /// <param name="driverId">Driver Id</param>
        /// <param name="deliveryRequestId">DeliveryRequestId</param>
        /// <returns></returns>

        [HttpPut("{driverId}/{deliveryRequestId}")]
        public async Task<IActionResult> AcceptDeliveryRequest(string driverId, string deliveryRequestId)
        {
            #region Validation Fields
            if (string.IsNullOrWhiteSpace(driverId))
            {
                return BadRequest(new { errorMessage = "The driverId is null." });
            }
            if (string.IsNullOrWhiteSpace(deliveryRequestId))
            {
                return BadRequest(new { errorMessage = "The deliveryRequestId is null." });
            }
            
            #endregion

            try
            {
                DeliveryRequest deliveryRequest = await _unitOfWork.RequestForDelivery.SingleOrDefaultAsync(x => x.DriverId == driverId && x.Id == deliveryRequestId);

                if (deliveryRequest != null)
                {
                    deliveryRequest.Status = "pickedup";
                    _unitOfWork.RequestForDelivery.Update(deliveryRequest);
                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound(new { ErrorMessag = "There is no DeliveryRequest with DriverId '" + driverId + "' And DeliveryRequestId '" + deliveryRequestId + "'" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
