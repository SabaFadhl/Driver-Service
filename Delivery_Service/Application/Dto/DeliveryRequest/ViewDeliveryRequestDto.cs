namespace Delivery_Service.Application.Dto.DeliveryRequest
{
    public class ViewDeliveryRequestDto
    {
        string _id;
        string _orderId;
        string _orderCode;
        string _compoundName; // This may be Restaurant name with some details
        string _status;  // pickedup, onway, delivered
        private DateTime _createTime;

        public string Id { get => _id; set => _id = value; }
        public string OrderId { get => _orderId; set => _orderId = value; }
        public string OrderCode { get => _orderCode; set => _orderCode = value; }
        public string CompoundName { get => _compoundName; set => _compoundName = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime CreateTime { get => _createTime; set => _createTime = value; }
    }
}
