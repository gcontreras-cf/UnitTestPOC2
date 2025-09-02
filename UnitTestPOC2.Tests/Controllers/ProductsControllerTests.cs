using Api.Controllers;
using Domain.DTO.Requests;
using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.Services.Transactional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTestPOC2.Tests.TestData.Client;
using UnitTestPOC2.Tests.TestData.Product;
using UnitTestPOC2.Tests.Utilities;
using Xunit;

namespace UnitTestPOC2.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<IProductServiceTransactional> _mockProductServiceTransactional;
        private readonly Mock<ILogger<ProductsController>> _mockLogger;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _mockProductServiceTransactional = new Mock<IProductServiceTransactional>();
            _mockLogger = MockLogger.CreateLogger<ProductsController>();
            _controller = new ProductsController(
                _mockProductService.Object,
                _mockProductServiceTransactional.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductSeq = 1, Name = "Product A", Price = 10.5m, Active = true },
                new Product { ProductSeq = 2, Name = "Product B", Price = 20.0m, Active = true }
            };
            _mockProductService.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(2, returnedProducts.Count());
        }

        [Fact]
        public async Task GetAll_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockProductService.Setup(s => s.GetAllAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }

        [Theory]
        [ClassData(typeof(GetByNameProductTestData))]
        public async Task GetByName_ShouldReturnFilteredProducts(string name, int expectedCount)
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductSeq = 1, Name = "Product A", Price = 10.5m, Active = true },
                new Product { ProductSeq = 2, Name = "Product B", Price = 20.0m, Active = true }
            };
            _mockProductService.Setup(s => s.GetByNameAsync(name))
                .ReturnsAsync(products.Where(p => p.Name.Contains(name)).ToList());

            // Act
            var result = await _controller.GetByName(name);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProducts = Assert.IsAssignableFrom<IEnumerable<Product>>(okResult.Value);
            Assert.Equal(expectedCount, returnedProducts.Count());
        }

        [Fact]
        public async Task GetByName_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var name = "Product";
            _mockProductService.Setup(s => s.GetByNameAsync(name)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetByName(name);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }

        [Theory]
        [ClassData(typeof(GetByProductSeqTestData))]
        public async Task GetByProductSeq_ShouldReturnProduct(Product product)
        {
            // Arrange
            _mockProductService.Setup(s => s.GetByProductSeqAsync(product.ProductSeq)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetByProductSeq(product.ProductSeq);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(product.ProductSeq, returnedProduct.ProductSeq);
        }

        [Fact]
        public async Task GetByProductSeq_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productSeq = 999;
            _mockProductService.Setup(s => s.GetByProductSeqAsync(productSeq)).ReturnsAsync((Product?)null);

            // Act
            var result = await _controller.GetByProductSeq(productSeq);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Product with ProductSeq {productSeq} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task Insert_ShouldReturnCreatedProduct()
        {
            // Arrange
            var request = new ProductInsertRequest { Name = "Product A", Price = 10.5m };
            var product = new Product { ProductSeq = 1, Name = request.Name, Price = request.Price, Active = true };
            _mockProductServiceTransactional.Setup(s => s.InsertAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _controller.Insert(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(createdResult.Value);
            Assert.Equal(request.Name, returnedProduct.Name);
        }

        [Fact]
        public async Task Insert_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var request = new ProductInsertRequest { Name = "Product A", Price = 10.5m };
            _mockProductServiceTransactional.Setup(s => s.InsertAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Insert(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedProduct()
        {
            // Arrange
            var request = new ProductUpdateRequest { ProductSeq = 1, Name = "Product Updated", Price = 15.0m };
            var product = new Product { ProductSeq = request.ProductSeq, Name = request.Name, Price = request.Price, Active = true };
            _mockProductServiceTransactional.Setup(s => s.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(product);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(request.Name, returnedProduct.Name);
        }

        [Fact]
        public async Task Update_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var request = new ProductUpdateRequest { ProductSeq = 1, Name = "Product Updated", Price = 15.0m };
            _mockProductServiceTransactional.Setup(s => s.UpdateAsync(It.IsAny<Product>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Update(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }

        [Fact]
        public async Task Inactivate_ShouldReturnNoContent()
        {
            // Arrange
            var productSeq = 1;
            _mockProductServiceTransactional.Setup(s => s.InactivateAsync(productSeq)).ReturnsAsync(true);

            // Act
            var result = await _controller.Inactivate(productSeq);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Inactivate_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productSeq = 999;
            _mockProductServiceTransactional.Setup(s => s.InactivateAsync(productSeq)).ReturnsAsync(false);

            // Act
            var result = await _controller.Inactivate(productSeq);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Product with ProductSeq {productSeq} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task Inactivate_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var productSeq = 1;
            _mockProductServiceTransactional.Setup(s => s.InactivateAsync(productSeq)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Inactivate(productSeq);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An error occurred while processing the request.", statusCodeResult.Value);
        }
    }
}