namespace Delivery_Service.Application.Dto.DeliveryRequest
{
    public class ViewOrderItemDetailsDto
    {
        private string _itemId;
        private string _itemName;
        private double _price;
        private int _quantity;
        private string _notes;

        public string ItemId { get => _itemId; set => _itemId = value; }
        public string ItemName { get => _itemName; set => _itemName = value; }
        public double Price { get => _price; set => _price = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public string Notes { get => _notes; set => _notes = value; }
    }
}
