using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Delivery_Service.Domain
{
    [Table("RequestForDelivery", Schema = "delivery")]
    public class RequestForDelivery
    {
        string _id;
        string _orderId;
        string _orderCode;
        string _compoundName; // This may be Restaurant name with some details
        string _driverId; // This can be null before assign the order to specific driver
        string _status;  // pickedup, onway, delivered
        private DateTime _createTime;
        private DateTime _onwayTime;
        private DateTime _deliveredTime;

        [Key]
        [MaxLength(60, ErrorMessage = "This field must be less than or equals 60 character")]
        public string Id { get => _id; set => _id = value; }

        [Required]
        [MaxLength(60, ErrorMessage = "This field must be less than or equals 60 character")]
        public string OrderId { get => _orderId; set => _orderId = value; }

        [Required]
        [MaxLength(20, ErrorMessage = "This field must be less than or equals 20 character")]
        public string OrderCode { get => _orderCode; set => _orderCode = value; }

        [Required]
        [MaxLength(250, ErrorMessage = "This field must be less than or equals 250 character")]
        public string CompoundName { get => _compoundName; set => _compoundName = value; }
        
        [MaxLength(60, ErrorMessage = "This field must be less than or equals 60 character")]
        public string DriverId { get => _driverId; set => _driverId = value; }

        [Required]
        [MaxLength(10, ErrorMessage = "This field must be less than or equals 10 character")]
        public string Status { get => _status; set => _status = value; }

        [Required]
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }

        public DateTime OnwayTime { get => _onwayTime; set => _onwayTime = value; }

        public DateTime DeliveredTime { get => _deliveredTime; set => _deliveredTime = value; }
       
    }
}
