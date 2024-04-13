using System.ComponentModel.DataAnnotations;

namespace Delivery_Service.Application.Dto.Driver
{    
    public class SetDriverAvailabilityStatusDto
    {
        string _status;

        /// <summary>
        /// The Status should be: 'offline' or 'online'
        /// </summary>
        [Required]        
        public string Status { get => _status; set => _status = value; }
    }
}
