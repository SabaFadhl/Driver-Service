using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Domain;

namespace DeliveryService.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Driver> Driver { get; }
        IRepository<DeliveryRequest> RequestForDelivery { get; }
        Task SaveChangesAsync();

        void AssignOrderToDriver(string PickupOrderId);
        
        void ChangeOrderStatusAsync(string deliveryRequestId, string orderId, string status, string deliveryPhoneNumber);
        Task<ViewOrderDetailsByOrderIdDto> GetOrderDetailsAsync(string orderId);
    }
}