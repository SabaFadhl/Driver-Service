using System.ComponentModel.DataAnnotations;

namespace Delivery_Service.Application.Dto.DeliveryRequest
{
    public class ChangeOrderStatusDto
    {
        string _status;

        /// <summary>
        /// The Status should be: 'pickedup' or 'onway' or 'delivered'
        /// </summary>
        [Required]
        public string Status { get => _status; set => _status = value; }
    }
}
