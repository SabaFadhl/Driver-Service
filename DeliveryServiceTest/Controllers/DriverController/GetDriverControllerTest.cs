using AutoFixture;
using Castle.Core.Resource;
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
    public class GetDriverControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly GetDriverController _controller;

        public GetDriverControllerTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new GetDriverController(_serviceMock.Object);
        }

        [Fact]
        public void GetDriver_Should_Return_Ok_With_Driver()
        {
            // Arrange           
            Driver request = _fixture.Create<Driver>();
            _serviceMock.Setup(x => x.Driver.GetById(request.Id)).ReturnsAsync(request);

            // Act
            var result = _controller.Get(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public void GetDriver_Should_Return_NotFound_When_Wron_Id()
        {
            // Arrange           
            Driver request = _fixture.Create<Driver>();
            _serviceMock.Setup(x => x.Driver.GetById(request.Id)).ReturnsAsync((Driver)null);

            // Act
            var result = _controller.Get(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
