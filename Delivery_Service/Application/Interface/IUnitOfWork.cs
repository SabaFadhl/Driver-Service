using Delivery_Service.Domain;

namespace DeliveryService.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Driver> Driver { get; }
        IRepository<RequestForDelivery> RequestForDelivery { get; }
        Task SaveChangesAsync();

        void AssignDriverForOrder(string PickupOrderId);
    }
}