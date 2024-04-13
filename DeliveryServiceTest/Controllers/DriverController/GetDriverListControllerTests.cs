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
    public class GetDriverListControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly GetDriverListController _controller;

        public GetDriverListControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new GetDriverListController(_serviceMock.Object);
        }

        [Fact]
        public void GetDriver_Should_Return_Ok_With_Driver_List()
        {
            // Arrange           
            List<Driver> request = _fixture.Create<List<Driver>>();
            _serviceMock.Setup(x => x.Driver.GetAllPageing(1, 5)).ReturnsAsync(request);

            // Act
            var result = _controller.GetList(1, 5);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
