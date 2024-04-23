using AutoFixture;
using AutoMapper;
using Delivery_Service.Application.Dto.DeliveryRequest;
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
        private readonly ChangeDeliveryRequestStatusController _controller;
        private readonly IMapper _mapperMock;

        public AcceptDeliveryRequestControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();
            _mapperMock = mapperMock.Object;

            _controller = new ChangeDeliveryRequestStatusController(_serviceMock.Object);
        }

        //[Fact]
        //public void ChangeDeliveryRequestStatus_to_delivered_Should_Return_NoContent()
        //{
        //    // Arrange
        //    string driverId = Guid.NewGuid().ToString();
        //    string deliveryRequestId = Guid.NewGuid().ToString();
        //    Delivery_Service.Domain.DeliveryRequest request = _fixture.Create<Delivery_Service.Domain.DeliveryRequest>();
        //    _serviceMock.Setup(x => x.RequestForDelivery.SingleOrDefaultAsync(x => x.DriverId == driverId && x.Id == deliveryRequestId)).ReturnsAsync(request);
        //    ChangeOrderStatusDto statusDto = new();
        //    statusDto.Status = "delivered";

        //    // Act
        //    var result = _controller.ChangeDeliveryRequestStatus(deliveryRequestId, statusDto);

        //    // Assert            
        //    var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
        //    Assert.Equal(204, objectResult.StatusCode);
        //}
       
        [Fact]
        public void ChangeDeliveryRequestStatus_Use_Any_Word_As_Status_Should_Return_BadRequest()
        {
            // Arrange
            string driverId = Guid.NewGuid().ToString();
            string deliveryRequestId = Guid.NewGuid().ToString();
            Delivery_Service.Domain.DeliveryRequest request = _fixture.Create<Delivery_Service.Domain.DeliveryRequest>();
            _serviceMock.Setup(x => x.RequestForDelivery.SingleOrDefaultAsync(x => x.DriverId == driverId && x.Id == deliveryRequestId)).ReturnsAsync(request);
            ChangeOrderStatusDto statusDto = new();
            statusDto.Status = "any word as status";

            // Act
            var result = _controller.ChangeDeliveryRequestStatus(driverId, deliveryRequestId, statusDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
