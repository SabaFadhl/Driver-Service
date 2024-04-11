using Delivery_Service.Domain;
using DeliveryService.Application.Interface;

namespace DeliveryService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterContext _context;
        private bool _disposed;

        public IRepository<Driver> Driver { get; private set; }
        public IRepository<RequestForDelivery> RequestForDelivery { get; private set; }

        public UnitOfWork(MasterContext context)
        {
            _context = context;
            _disposed = false;

            Driver = new Repository<Driver>(_context);
            RequestForDelivery = new Repository<RequestForDelivery>(_context);
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
    }
}
