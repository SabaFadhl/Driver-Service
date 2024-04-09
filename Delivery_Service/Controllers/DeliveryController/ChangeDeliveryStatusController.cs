using DeliveryService.Application.Dto;
using DeliveryService.Application.Interface;
using DeliveryService.Domain;
using DeliveryService.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Controllers.DeliveryController
{
    [ApiController]
    [Route("api/Delivery")]
    public class ChangeDeliveryStatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeDeliveryStatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPut("ChangeAvailabilityStatus/{DeliveryId}")]
        public async Task<IActionResult> ChangeDeliveryStatus(string DeliveryId)
    {

        try
        {
            Delivery Delivery = await _unitOfWork.GetRepository<Delivery>().GetById(DeliveryId);

            if (Delivery != null)
            {
                
                Delivery.AvailabilityStatus=!Delivery.AvailabilityStatus;
                 _unitOfWork.GetRepository<Delivery>().Update(Delivery);
                await _unitOfWork.SaveChangesAsync();
                
                return NoContent();
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
