using Delivery_Service.Domain;

namespace DeliveryService.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Driver> Driver { get; }
        IRepository<DeliveryRequest> RequestForDelivery { get; }
        Task SaveChangesAsync();

        void AssignOrderToDriver(string PickupOrderId);
        
        void ChangeOrderStatus(string orderId, string status);
    }
}