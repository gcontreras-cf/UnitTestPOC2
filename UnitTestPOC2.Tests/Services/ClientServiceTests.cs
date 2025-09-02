using Core.Service.NoTransactional;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork.NoTransactional;
using Domain.Interfaces.Repositories.NoTransactional;
using Moq;
using UnitTestPOC2.Tests.TestData.Client;
using Xunit;

namespace UnitTestPOC2.Tests.Services
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly ClientService _service;

        public ClientServiceTests()
        {
            _mockUnitOfWork = new Mock<IClientUnitOfWork>();
            _mockClientRepository = new Mock<IClientRepository>();

            _mockUnitOfWork.Setup(u => u.ClientRepository).Returns(_mockClientRepository.Object);

            _service = new ClientService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllClients()
        {
            // Arrange
            var clients = ClientServiceTestData.GetSampleClients();
            _mockClientRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(clients);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clients.Count, result.Count());
        }

        [Theory]
        [InlineData("John", 1)]
        [InlineData("Jane", 1)]
        [InlineData("Nonexistent", 0)]
        public async Task GetByNameAsync_ShouldReturnFilteredClients(string name, int expectedCount)
        {
            // Arrange
            var clients = ClientServiceTestData.GetSampleClients();
            _mockClientRepository.Setup(r => r.GetByNameAsync(name))
                .ReturnsAsync(clients.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList());

            // Act
            var result = await _service.GetByNameAsync(name);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(999, false)]
        public async Task GetByClientSeqAsync_ShouldReturnClientOrNull(int clientSeq, bool shouldExist)
        {
            // Arrange
            var clients = ClientServiceTestData.GetSampleClients();
            var client = clients.FirstOrDefault(c => c.ClientSeq == clientSeq);
            _mockClientRepository.Setup(r => r.GetByClientSeqAsync(clientSeq)).ReturnsAsync(client);

            // Act
            var result = await _service.GetByClientSeqAsync(clientSeq);

            // Assert
            if (shouldExist)
            {
                Assert.NotNull(result);
                Assert.Equal(clientSeq, result!.ClientSeq);
            }
            else
            {
                Assert.Null(result);
            }
        }
    }
}