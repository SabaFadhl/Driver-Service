using AutoFixture;
using AutoMapper;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Controllers.DeliveryRequestController;
using DeliveryService.Application.Interface;
using DeliveryService.Controllers.DeliveryController;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryServiceTest.Controllers.DriverController
{
    public class ChangeAvailabilityStatusControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly ChangeAvailabilityStatusController _controller;
        private readonly IMapper _mapperMock;

        public ChangeAvailabilityStatusControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();
            _mapperMock = mapperMock.Object;

            _controller = new ChangeAvailabilityStatusController(_serviceMock.Object);
        }

        [Fact]
        public async void ChangeAvailabilityStatus_Should_Return_NoContent()
        {
            // Arrange
            string driverId = Guid.NewGuid().ToString();
            Delivery_Service.Domain.Driver request = _fixture.Create<Delivery_Service.Domain.Driver>();
            _serviceMock.Setup(x => x.Driver.GetById(driverId)).ReturnsAsync(request);

            SetDriverAvailabilityStatusDto setDriverAvailabilityStatusDto = new();
            setDriverAvailabilityStatusDto.Status = "online";

            // Act
            var result = _controller.ChangeDeliveryStatus(driverId, setDriverAvailabilityStatusDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }
    }
}
