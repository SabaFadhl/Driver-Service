using System.ComponentModel.DataAnnotations;

namespace Delivery_Service.Application.Dto.Driver
{
    public class SetDriverAvailabilityStatusDto
    {
        string _status;

        [Required]
        public string Status { get => _status; set => _status = value; }
    }
}
