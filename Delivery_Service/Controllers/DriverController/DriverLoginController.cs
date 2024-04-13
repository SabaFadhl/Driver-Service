
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.RegularExpressions;

namespace CustomerService.Controllers.CustomerController
{

    [ApiController]
    [Route("api/Driver")]
    public class DriverLoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriverLoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This API to allow Driver to Login.
        /// </summary>
        /// <param name="loginDriverDto">Driver login Credentials.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(DriverLoginDto loginDriverDto)
        {
            #region Validation Fields
            if (loginDriverDto == null)
            {
                return BadRequest(new { errorMessage = "The LoginDriverDto is null." });
            }
            if (string.IsNullOrWhiteSpace(loginDriverDto.EmailOrName))
            {
                return BadRequest(new { errorMessage = "You must enter Email or Name." });
            }
            if (string.IsNullOrWhiteSpace(loginDriverDto.Password))
            {
                return BadRequest(new { errorMessage = "You must enter Password." });
            }
            #endregion

            try
            {
                Driver driver = await _unitOfWork.Driver.SingleOrDefaultAsync(u => (u.Email == loginDriverDto.EmailOrName || u.Name == loginDriverDto.EmailOrName) && u.Password == loginDriverDto.Password);
                if (driver != null)
                {
                    // Successfully authenticated login                    
                    return StatusCode(200, new { driver.Id });
                }
                else
                {  // Failed to log in
                    return BadRequest(new { errorMessage = "You  failed to log in with the wrong name or email." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
