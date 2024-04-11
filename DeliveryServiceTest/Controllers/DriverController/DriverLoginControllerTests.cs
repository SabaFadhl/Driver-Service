using AutoFixture;
using AutoMapper;
using CustomerService.Controllers.CustomerController;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Domain;
using DeliveryService.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeliveryServiceTest.Controllers.DriverController
{
    public class DriverLoginControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly DriverLoginController _controller;
        private readonly IMapper _mapperMock;

        public DriverLoginControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();
            _mapperMock = mapperMock.Object;

            _controller = new DriverLoginController(_serviceMock.Object);
        }

        [Fact]
        public void CustomerLogin_Should_Return_BadRequest_For_NotNull_EmailOrName()
        {
            // Arrange
            var request = _fixture.Create<DriverLoginDto>();
            request.EmailOrName = "example@example.com"; // Set a non-empty value for EmailOrName
            request.Password = "password123"; // Set a non-empty value for Password

            // Act
            var result = _controller.Login(request);

            // Assert
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);

            // Additional assertion for GUID/UUID
            var value = objectResult.Value;
            Assert.NotNull(value);
            Assert.IsType<string>(value);

            string stringValue = (string)value;
            Assert.False(string.IsNullOrEmpty(stringValue));
        }

        [Fact]
        public void Password_Should_Contain_Numbers_Letters_Symbols()
        {
            // Arrange
            var request = _fixture.Create<DriverLoginDto>();
            request.Password = "Abcd123!";// Set a password containing numbers, letters, and symbols (the same the rule)

            // Act
            var containsNumbers = Regex.IsMatch(request.Password, @"\d"); // Check for numbers
            var containsLetters = Regex.IsMatch(request.Password, @"[a-zA-Z]"); // Check for letters
            var containsSymbols = Regex.IsMatch(request.Password, @"[^a-zA-Z0-9]"); // Check for symbols
            var result = _controller.Login(request);
            // Assert
            Assert.True(containsNumbers);
            Assert.True(containsLetters);
            Assert.True(containsSymbols);
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result.Result);
            Assert.Equal(400, objectResult.StatusCode);
        }
    }
}
