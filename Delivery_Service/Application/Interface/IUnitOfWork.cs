using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;

namespace DeliveryService.Application.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Driver> Driver { get; }
        IRepository<DeliveryRequest> RequestForDelivery { get; }
        Task SaveChangesAsync();

        void AssignOrderToDriver(string PickupOrderId);
        
        void ChangeOrderStatusAsync(string orderId, string status);
        Task<ViewOrderDetailsByOrderIdDto> GetOrderDetailsAsync(string orderId);

        Task<List<ViewDriverOrderDto>> GetDriverOrdersListAsync(string driverId);
    }
}