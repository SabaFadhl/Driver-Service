using AutoFixture;
using AutoMapper;
using Castle.Core.Resource;
using Delivery_Service.Application.Dto.Common;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
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
    public class AddDriverControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly AddDriverController _controller;
        private readonly IMapper _mapperMock;

        public AddDriverControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();           
            _mapperMock = mapperMock.Object;

            _controller = new AddDriverController(_serviceMock.Object);
        }

        [Fact]
        public void AddDriver_ShouldRetunGuid()
        {
            // Arrange
            var request = _fixture.Create<AddDriverDto>();
            request.PhoneNumber = "+967 123456789";
            var driver = _mapperMock.Map<AddDriverDto, Driver>(request);
            _serviceMock.Setup(x => x.Driver.Add(driver)).Verifiable();

            // Act
            var result = _controller.Add(request);

            // Assert                       
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            bool isValidGuid = Guid.TryParse(((ReturnGuidDto)objectResult.Value).Id, out Guid guidResult);
            Assert.True(isValidGuid);
        }

        [Fact]
        public void AddDriver_Should_Return_BadRequest_For_Validate_Name()
        {
            // Arrange
            var request = _fixture.Create<AddDriverDto>();
            var driver = _mapperMock.Map<AddDriverDto, Driver>(request);
            _serviceMock.Setup(x => x.Driver.Add(driver)).Verifiable();
           
            request.Name = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void AddDriver_Should_Return_BadRequest_For_Validate_Email()
        {
            // Arrange
            var request = _fixture.Create<AddDriverDto>();
            var driver = _mapperMock.Map<AddDriverDto, Driver>(request);
            _serviceMock.Setup(x => x.Driver.Add(driver)).Verifiable();
            
            request.Email = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void AddDriver_Should_Return_BadRequest_For_Validate_Password()
        {
            // Arrange
            var request = _fixture.Create<AddDriverDto>();
            var driver = _mapperMock.Map<AddDriverDto, Driver>(request);
            _serviceMock.Setup(x => x.Driver.Add(driver)).Verifiable();
            
            request.Password = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public void AddDriver_Should_Return_BadRequest_For_Validate_PhoneNumber()
        {
            // Arrange
            var request = _fixture.Create<AddDriverDto>();
            var driver = _mapperMock.Map<AddDriverDto, Driver>(request);
            _serviceMock.Setup(x => x.Driver.Add(driver)).Verifiable();
            
            request.PhoneNumber = null;

            // Act
            var result = _controller.Add(request);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
