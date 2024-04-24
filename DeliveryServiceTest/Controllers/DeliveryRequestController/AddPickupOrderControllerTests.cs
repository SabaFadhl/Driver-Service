using AutoFixture;
using AutoMapper;
using Delivery_Service.Application.Dto.Common;
using Delivery_Service.Application.Dto.Driver;
using Delivery_Service.Application.Dto.DeliveryRequest;
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
using Delivery_Service.Controllers.DeliveryRequestController;

namespace DeliveryServiceTest.Controllers.DeliveryRequestController
{
    public class AddPickupOrderControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUnitOfWork> _serviceMock;
        private readonly AddPickupOrderController _controller;
        private readonly IMapper _mapperMock;

        public AddPickupOrderControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IUnitOfWork>>();

            var mapperMock = new Mock<IMapper>();
            _mapperMock = mapperMock.Object;

            _controller = new AddPickupOrderController(_serviceMock.Object);
        }

        //[Fact]
        //public void AddPickupOrder_ShouldRetunGuid()
        //{
        //    // Arrange
        //    var request = _fixture.Create<AddPickupOrderDto>();
        //    var requestForDelivery = _mapperMock.Map<AddPickupOrderDto, Delivery_Service.Domain.DeliveryRequest>(request);
        //    _serviceMock.Setup(x => x.RequestForDelivery.Add(requestForDelivery));

        //    // Act
        //    var result = _controller.Add(request);

        //    // Assert                       
        //    var resultValue = Assert.IsType<ObjectResult>(result.Result).Value as dynamic;
        //    Console.WriteLine("3333", resultValue);
        //    bool isValidGuid = Guid.TryParse(resultValue.deliveryRequestId.ToString(), out Guid guidResult);
        //    Assert.True(isValidGuid);
        //}




    }
}
