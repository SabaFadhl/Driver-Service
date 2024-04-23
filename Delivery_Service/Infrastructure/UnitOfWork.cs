using AutoFixture;
using Delivery_Service;
using Delivery_Service.Application.Dto.DeliveryRequest;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using DeliveryService.Controllers.DeliveryController;
using Newtonsoft.Json;
using System.IO;



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

        public async void ChangeOrderStatusAsync(string orderId, string status)
        {
            // Here, We can change the status of the order on the OrderService using API end point

            // status = pickedup/onway/delivered
        }

        public async Task<ViewOrderDetailsByOrderIdDto> GetOrderDetailsAsync(string orderId)
        {
            // This Function to get an Order Details from the OrderService and RestaurantService and get
            // Customer Address from CustomerService.

            #region Mock Data
            IFixture _fixture = new Fixture();
            var mockData = _fixture.Create<ViewOrderDetailsByOrderIdDto>();
            mockData.CustomerAddressId = "691729e1-3d23-4af3-bdcc-b75458rrr0";
            #endregion

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(MemberVariables.BASE_URL_CUSTOMER_SERVICE);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            HttpResponseMessage response = await client.GetAsync(MemberVariables.ENDPOINT_CUSTOMER_SERVICE__CUSTOMER_ADDRESS.Replace("#1", mockData.CustomerAddressId));
            response.EnsureSuccessStatusCode();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var viewCustomerAddressDto = JsonConvert.DeserializeObject<ViewCustomerAddressDto>(responseBody);

                mockData.CustomerAddress = viewCustomerAddressDto;
            }
            else
            {
                return new ViewOrderDetailsByOrderIdDto();
            }

            return mockData;
        }
    }
}
