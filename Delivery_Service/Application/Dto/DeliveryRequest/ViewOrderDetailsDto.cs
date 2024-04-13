namespace Delivery_Service.Application.Dto.DeliveryRequest
{
    public class ViewOrderDetailsByOrderIdDto
    {
        private string _restaurantId;
       private string _customerAddressId;
        private ViewCustomerAddressDto _customerAddress;
        private double _totalPrice;
        private List<ViewOrderItemDetailsDto> _viewOrderItemDetailsListDto;

        public string RestaurantId { get => _restaurantId; set => _restaurantId = value; }
        public string CustomerAddressId { get => _customerAddressId; set => _customerAddressId = value; }
        public ViewCustomerAddressDto CustomerAddress { get => _customerAddress; set => _customerAddress = value; }
        public double TotalPrice { get => _totalPrice; set => _totalPrice = value; }
        public List<ViewOrderItemDetailsDto> ViewOrderItemDetailsListDto { get => _viewOrderItemDetailsListDto; set => _viewOrderItemDetailsListDto = value; }        
    }
}
