using AutoFixture;
using Delivery_Service.Controllers.DeliveryRequestController;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryServiceTest.Controllers.DeliveryRequest
{
    public class ViewDeliveryRequestControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly ViewDeliveryRequestController _controller;

        public ViewDeliveryRequestControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new ViewDeliveryRequestController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetList_Of_DeliveryRequest_Should_Return_Ok_With_DeliveryRequest_ListAsync()
        {
            // Arrange
            string driverId = Guid.NewGuid().ToString();
            Driver request = _fixture.Create<Driver>();
            request.Id = driverId;
            _serviceMock.Setup(x => x.Driver.GetById(driverId)).ReturnsAsync(request);
            List<Delivery_Service.Domain.DeliveryRequest> requestForDeliveries = _fixture.Create<List<Delivery_Service.Domain.DeliveryRequest>>();
            _serviceMock.Setup(x => x.RequestForDelivery.FindAsync(x => x.DriverId == driverId && (x.Status.Equals("pending") || x.Status.Equals("pickedup")))).ReturnsAsync(requestForDeliveries);

            // Act
            var result = _controller.GetList(driverId);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
