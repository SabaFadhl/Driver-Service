using AutoFixture;
using Delivery_Service.Application.Dto.DeliveryRequest;
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

namespace DeliveryServiceTest.Controllers.DeliveryRequestController
{
    public class ViewOrderDetailsControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly ViewOrderDetailsController _controller;

        public ViewOrderDetailsControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();
            _controller = new ViewOrderDetailsController(_serviceMock.Object);
        }

        /// <summary>
        /// This is an Integration test with Customer Service.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ViewOrderDetails_Should_Return_Ok_With_OrderDetailsAsync()
        {
            // Arrange
            string orderId = Guid.NewGuid().ToString();
            ViewOrderDetailsByOrderIdDto request = _fixture.Create<ViewOrderDetailsByOrderIdDto>();
            _serviceMock.Setup(x => x.GetOrderDetailsAsync(orderId)).ReturnsAsync(request);
           
            // Act
            var result = _controller.Get(orderId);

            // Assert            
            var objectResult = Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(200, objectResult.StatusCode);
        }
    }
}
