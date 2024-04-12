using Delivery_Service.Domain;
using DeliveryService.Application.Interface;

namespace DeliveryService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;
        private bool _disposed;

        public IRepository<Driver> Driver { get; private set; }
        public IRepository<DeliveryRequest> RequestForDelivery { get; private set; }

        public UnitOfWork(MasterContext context)
        {
            _context = context;
            _disposed = false;

            Driver = new Repository<Driver>(_context);
            RequestForDelivery = new Repository<DeliveryRequest>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public async void AssignOrderToDriver(string PickupOrderId)
        {
            // Here, one of the algorithms can be used to choose the appropriate driver according to the criteria.
            // By default, Now we can choice the available online Driver with IsBusy status = false.
            // This code to Assign an Order To one Driver.

            DeliveryRequest requestForDelivery = await RequestForDelivery.GetById(PickupOrderId);
            if (requestForDelivery != null)
            {
                Driver driver = await Driver.SingleOrDefaultAsync(x => x.IsBusy == false && x.AvailabilityStatus == "online");
                if (driver != null)
                {
                    try
                    {
                        _context.Database.BeginTransaction();
                        driver.IsBusy = true;
                        Driver.Update(driver);
                        await Driver.SaveChanges();

                        requestForDelivery.DriverId = driver.Id;
                        RequestForDelivery.Update(requestForDelivery);
                        await RequestForDelivery.SaveChanges();
                        _context.Database.CommitTransaction();
                    }
                    catch  
                    {
                        _context.Database.RollbackTransaction();
                    }                 
                }
            }
        }

        public async void ChangeOrderStatus(string orderId, string status)
        {
            // Here, We can chanve the status of the order on the OrderService using API end point
        }
    }
}
