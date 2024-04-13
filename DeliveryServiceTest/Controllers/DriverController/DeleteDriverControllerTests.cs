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
    public class DeleteDriverControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly DeleteDriverController _controller;

        public DeleteDriverControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new DeleteDriverController(_serviceMock.Object);
        }

        [Fact]
        public void DeleteDriver_Should_Return_NoContent_When_Delete()
        {
            // Arrange           
            Driver request = _fixture.Create<Driver>();
            _serviceMock.Setup(x => x.Driver.GetById(request.Id)).ReturnsAsync(request);
            _serviceMock.Setup(x => x.Driver.Remove(request)).Verifiable();

            // Act
            var result = _controller.Delete(request.Id);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NoContentResult>(result.Result);
            Assert.Equal(204, objectResult.StatusCode);
        }

        [Fact]
        public void DeleteDriver_Should_Return_NotFound_When_Id_is_Notfount()
        {
            // Arrange           
            var driverId = _fixture.Create<Guid>().ToString();
            _serviceMock.Setup(x => x.Driver.GetById(driverId)).ReturnsAsync((Driver)null);

            // Act
            var result = _controller.Delete(driverId);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
