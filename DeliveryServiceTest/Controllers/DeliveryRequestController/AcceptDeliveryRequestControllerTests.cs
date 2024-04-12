using AutoFixture;
using AutoMapper;
using Delivery_Service.Controllers.DeliveryRequestController;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DeliveryServiceTest.Controllers.DeliveryRequestController
{
    public class AcceptDeliveryRequestControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly AcceptDeliveryRequestController _controller;
        private readonly IMapper _mapperMock;

        public AcceptDeliveryRequestControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();
            _mapperMock = mapperMock.Object;

            _controller = new AcceptDeliveryRequestController(_serviceMock.Object);
        }

        [Fact]
        public void AcceptDeliveryRequest_Should_Return_NoContent()
        {
            // Arrange
            string driverId = Guid.NewGuid().ToString();
            string deliveryRequestId = Guid.NewGuid().ToString();
            Delivery_Service.Domain.DeliveryRequest request = _fixture.Create<Delivery_Service.Domain.DeliveryRequest>();
            _serviceMock.Setup(x => x.RequestForDelivery.SingleOrDefaultAsync(x => x.DriverId == driverId && x.Id == deliveryRequestId)).ReturnsAsync(request);

            // Act
            var result = _controller.AcceptDeliveryRequest(driverId, deliveryRequestId);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }
    }
}
