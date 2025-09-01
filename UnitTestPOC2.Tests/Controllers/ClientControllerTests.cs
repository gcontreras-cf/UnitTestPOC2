using Api.Controllers;
using Domain.DTO.Requests;
using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.Services.Transactional;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTestPOC2.Tests.TestData;
using UnitTestPOC2.Tests.Utilities;
using Xunit;

namespace UnitTestPOC2.Tests.Controllers
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientService> _mockClientService;
        private readonly Mock<IClientServiceTransactional> _mockClientServiceTransactional;
        private readonly Mock<ILogger<ClientController>> _mockLogger;
        private readonly ClientController _controller;

        public ClientControllerTests()
        {
            _mockClientService = new Mock<IClientService>();
            _mockClientServiceTransactional = new Mock<IClientServiceTransactional>();
            _mockLogger = MockLogger.CreateLogger<ClientController>();
            _controller = new ClientController(
                _mockClientService.Object,
                _mockClientServiceTransactional.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkWithClients()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client { ClientSeq = 1, Name = "John Doe", Email = "john.doe@example.com", Active = true },
                new Client { ClientSeq = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Active = true }
            };
            _mockClientService.Setup(s => s.GetAllAsync()).ReturnsAsync(clients);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedClients = Assert.IsAssignableFrom<IEnumerable<Client>>(okResult.Value);
            Assert.Equal(2, returnedClients.Count());
        }

        [Theory]
        [ClassData(typeof(GetByNameTestData))]
        public async Task GetByName_ShouldReturnFilteredClients(string name, int expectedCount)
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client { ClientSeq = 1, Name = "John Doe", Email = "john.doe@example.com", Active = true },
                new Client { ClientSeq = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Active = true }
            };
            _mockClientService.Setup(s => s.GetByNameAsync(name))
                .ReturnsAsync(clients.Where(c => c.Name.Contains(name)).ToList());

            // Act
            var result = await _controller.GetByName(name);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedClients = Assert.IsAssignableFrom<IEnumerable<Client>>(okResult.Value);
            Assert.Equal(expectedCount, returnedClients.Count());
        }

        [Theory]
        [ClassData(typeof(GetByClientSeqTestData))]
        public async Task GetByClientSeq_ShouldReturnClient(Client client)
        {
            // Arrange
            _mockClientService.Setup(s => s.GetByClientSeqAsync(client.ClientSeq)).ReturnsAsync(client);

            // Act
            var result = await _controller.GetByClientSeq(client.ClientSeq);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedClient = Assert.IsType<Client>(okResult.Value);
            Assert.Equal(client.ClientSeq, returnedClient.ClientSeq);
        }

        [Fact]
        public async Task Insert_ShouldReturnCreatedClient()
        {
            // Arrange
            var request = new ClientInsertRequest { Name = "John Doe", Email = "john.doe@example.com" };
            var client = new Client { ClientSeq = 1, Name = request.Name, Email = request.Email, Active = true };
            _mockClientServiceTransactional.Setup(s => s.InsertAsync(It.IsAny<Client>())).ReturnsAsync(client);

            // Act
            var result = await _controller.Insert(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedClient = Assert.IsType<Client>(createdResult.Value);
            Assert.Equal(request.Name, returnedClient.Name);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedClient()
        {
            // Arrange
            var request = new ClientUpdateRequest { ClientSeq = 1, Name = "John Updated", Email = "john.updated@example.com" };
            var client = new Client { ClientSeq = request.ClientSeq, Name = request.Name, Email = request.Email, Active = true };
            _mockClientServiceTransactional.Setup(s => s.UpdateAsync(It.IsAny<Client>())).ReturnsAsync(client);

            // Act
            var result = await _controller.Update(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedClient = Assert.IsType<Client>(okResult.Value);
            Assert.Equal(request.Name, returnedClient.Name);
        }

        [Fact]
        public async Task Inactivate_ShouldReturnNoContent()
        {
            // Arrange
            var clientSeq = 1;
            _mockClientServiceTransactional.Setup(s => s.InactivateAsync(clientSeq)).ReturnsAsync(true);

            // Act
            var result = await _controller.Inactivate(clientSeq);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}