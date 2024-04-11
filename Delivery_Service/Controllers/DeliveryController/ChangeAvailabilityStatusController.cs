using DeliveryService.Application.Interface;
using Delivery_Service.Domain;
using DeliveryService.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers.DeliveryController
{
    [ApiController]
    [Route("api/Delivery")]
    public class ChangeAvailabilityStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeAvailabilityStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("ChangeAvailabilityStatus/{DeliveryId}")]
        public async Task<IActionResult> ChangeDeliveryStatus(string DeliveryId)
        {

            try
            {
                Driver Delivery = await _unitOfWork.Driver.GetById(DeliveryId);

                //if (Delivery != null)
                //{

                //    Delivery.AvailabilityStatus = !Delivery.AvailabilityStatus;
                //    _unitOfWork.GetRepository<Driver>().Update(Delivery);
                //    await _unitOfWork.SaveChangesAsync();

                //    return NoContent();
                //}
                //else
                //{
                //    return NotFound();
                //}
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
