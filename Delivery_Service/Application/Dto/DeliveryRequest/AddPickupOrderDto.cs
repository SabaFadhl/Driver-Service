namespace Delivery_Service.Application.Dto.DeliveryRequest
{
    public class AddPickupOrderDto
    {      
        string _orderId;
        string _orderCode;
        string _compoundName; // This may be Restaurant name with some details      

        /// <summary>
        /// The OrderId is the identity of an order that created in OrderService
        /// </summary>
        public string OrderId { get => _orderId; set => _orderId = value; }

        /// <summary>
        /// The OrderCode is the Numer or Code of an order that created in OrderService.
        /// </summary>
        public string OrderCode { get => _orderCode; set => _orderCode = value; }

        /// <summary>
        /// The CompoundName field represents Name or Short description to read it by Driver when view an orders on the list.
        /// This field may be restaurant name and some details
        /// </summary>
        public string CompoundName { get => _compoundName; set => _compoundName = value; }
    }
}
