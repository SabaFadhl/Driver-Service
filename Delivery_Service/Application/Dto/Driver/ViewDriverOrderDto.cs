namespace Delivery_Service.Application.Dto.Driver
{
    public class ViewDriverOrderDto
    {
        string _orderId;
        string _orderCode;
        string _orderStatus;

        public string OrderId { get => _orderId; set => _orderId = value; }
        public string OrderCode { get => _orderCode; set => _orderCode = value; }
        public string OrderStatus { get => _orderStatus; set => _orderStatus = value; }
    }
}
