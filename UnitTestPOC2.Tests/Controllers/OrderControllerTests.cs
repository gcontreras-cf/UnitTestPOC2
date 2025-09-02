using Api.Controllers;
using Domain.DTO.Requests;
using Domain.DTO.Responses;
using Domain.Interfaces.Services.Transactional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTestPOC2.Tests.TestData;
using UnitTestPOC2.Tests.Utilities;
using Xunit;

namespace UnitTestPOC2.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderServiceTransactional> _mockOrderServiceTransactional;
        private readonly Mock<ILogger<OrderController>> _mockLogger;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockOrderServiceTransactional = new Mock<IOrderServiceTransactional>();
            _mockLogger = MockLogger.CreateLogger<OrderController>();
            _controller = new OrderController(
                _mockOrderServiceTransactional.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnOk_WhenOrderIsCreatedSuccessfully()
        {
            // Arrange
            var request = new OrderCreateRequest
            {
                ClientSeq = 1,
                OrderDetails = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest { ProductSeq = 1, Quantity = 2 },
                    new OrderDetailRequest { ProductSeq = 2, Quantity = 1 }
                }
            };
            var response = new OrderCreateResponse { Success = true, Message = "Order created successfully." };
            _mockOrderServiceTransactional.Setup(s => s.CreateOrderAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response.Message, okResult.Value);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnBadRequest_WhenOrderCreationFails()
        {
            // Arrange
            var request = new OrderCreateRequest
            {
                ClientSeq = 1,
                OrderDetails = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest { ProductSeq = 1, Quantity = 2 },
                    new OrderDetailRequest { ProductSeq = 2, Quantity = 1 }
                }
            };
            var response = new OrderCreateResponse { Success = false, Message = "Failed to create order." };
            _mockOrderServiceTransactional.Setup(s => s.CreateOrderAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(response.Message, badRequestResult.Value);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var request = new OrderCreateRequest
            {
                ClientSeq = 1,
                OrderDetails = new List<OrderDetailRequest>
                {
                    new OrderDetailRequest { ProductSeq = 1, Quantity = 2 },
                    new OrderDetailRequest { ProductSeq = 2, Quantity = 1 }
                }
            };
            _mockOrderServiceTransactional.Setup(s => s.CreateOrderAsync(request)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.CreateOrder(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }

        [Fact]
        public async Task GetTodayOrders_ShouldReturnOkWithOrders()
        {
            // Arrange
            var orders = new List<TodayOrderResponse>
            {
                new TodayOrderResponse
                {
                    ClientName = "John Doe",
                    OrderDetails = new List<TodayOrderDetailResponse>
                    {
                        new TodayOrderDetailResponse { ProductName = "Product A", Quantity = 2, UnitPrice = 10.5m },
                        new TodayOrderDetailResponse { ProductName = "Product B", Quantity = 1, UnitPrice = 20.0m }
                    }
                },
                new TodayOrderResponse
                {
                    ClientName = "Jane Doe",
                    OrderDetails = new List<TodayOrderDetailResponse>
                    {
                        new TodayOrderDetailResponse { ProductName = "Product C", Quantity = 3, UnitPrice = 15.0m }
                    }
                }
            };
            _mockOrderServiceTransactional.Setup(s => s.GetTodayOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _controller.GetTodayOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedOrders = Assert.IsAssignableFrom<IEnumerable<TodayOrderResponse>>(okResult.Value);
            Assert.Equal(2, returnedOrders.Count());
        }

        [Fact]
        public async Task GetTodayOrders_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockOrderServiceTransactional.Setup(s => s.GetTodayOrdersAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetTodayOrders();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }
    }
}