using AutoFixture;
using Castle.Core.Resource;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Controllers.DriverController;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryServiceTest.Controllers.DriverController
{
    public class UpdateDriverControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly UpdateDriverController _controller;

        public UpdateDriverControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new UpdateDriverController(_serviceMock.Object);
        }

        [Fact]
        public void UpdateDriver_Should_Return_NoContent()
        {
            // Arrange           
            Driver request = _fixture.Create<Driver>();
            _serviceMock.Setup(x => x.Driver.GetById(request.Id)).ReturnsAsync(request);

            var requestDto = _fixture.Create<UpdateDriverDto>();
            requestDto.PhoneNumber = "+967 123456789";

            // Act
            var result = _controller.Update(request.Id, requestDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void UpdateDriver_Should_Return_NotFound_When_Wrong_DriverId()
        {
            // Arrange           
            Driver request = _fixture.Create<Driver>();
            _serviceMock.Setup(x => x.Driver.GetById(request.Id)).ReturnsAsync((Driver)null);

            var requestDto = _fixture.Create<UpdateDriverDto>();
            requestDto.PhoneNumber = "+967 123456789";

            // Act
            var result = _controller.Update(request.Id, requestDto);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
