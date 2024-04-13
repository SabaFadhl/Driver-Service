using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Delivery_Service.Controllers.DriverController
{
    [Route("api/Driver")]
    [ApiController]
    public class DeleteDriverController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDriverController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This API to allow system admin or driver to delete the driver user.
        /// </summary>
        /// <param name="driverId">Driver identity</param>
        /// <returns></returns>
        [HttpDelete("{driverId}")]
        public async Task<IActionResult> Delete(string driverId)
        {
            try
            {
                Driver customer = await _unitOfWork.Driver.GetById(driverId);
                if (customer != null)
                {
                    _unitOfWork.Driver.Remove(customer);

                    await _unitOfWork.SaveChangesAsync();

                    return NoContent();
                }
                else
                {
                    return NotFound(new { ErrorMessage = "There is no Driver with this Id '" + driverId + "'" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
