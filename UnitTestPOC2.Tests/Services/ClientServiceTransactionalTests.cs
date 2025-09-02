using Core.Service.Transactional;
using Domain.Entities;
using Domain.Interfaces.UnitOfWork.Transactional;
using Domain.Interfaces.Repositories.Transactional;
using Moq;
using UnitTestPOC2.Tests.TestData.Client;
using Xunit;

namespace UnitTestPOC2.Tests.Services
{
    public class ClientServiceTransactionalTests
    {
        private readonly Mock<IClientUnitOfWorkTransactional> _mockUnitOfWork;
        private readonly Mock<IClientRepositoryTransactional> _mockClientRepositoryTransactional;
        private readonly ClientServiceTransactional _service;

        public ClientServiceTransactionalTests()
        {
            _mockUnitOfWork = new Mock<IClientUnitOfWorkTransactional>();
            _mockClientRepositoryTransactional = new Mock<IClientRepositoryTransactional>();

            _mockUnitOfWork.Setup(u => u.ClientRepositoryTransactional).Returns(_mockClientRepositoryTransactional.Object);

            _service = new ClientServiceTransactional(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task InsertAsync_ShouldInsertClient()
        {
            // Arrange
            var client = ClientServiceTransactionalTestData.GetSampleClient();
            _mockClientRepositoryTransactional.Setup(r => r.InsertAsync(client)).ReturnsAsync(client);

            // Act
            var result = await _service.InsertAsync(client);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.Name, result.Name);
            _mockClientRepositoryTransactional.Verify(r => r.InsertAsync(client), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateClient()
        {
            // Arrange
            var client = ClientServiceTransactionalTestData.GetSampleClient();
            _mockClientRepositoryTransactional.Setup(r => r.UpdateAsync(client)).ReturnsAsync(client);

            // Act
            var result = await _service.UpdateAsync(client);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.Name, result.Name);
            _mockClientRepositoryTransactional.Verify(r => r.UpdateAsync(client), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(999, false)]
        public async Task InactivateAsync_ShouldReturnExpectedResult(int clientSeq, bool expectedResult)
        {
            // Arrange
            _mockClientRepositoryTransactional.Setup(r => r.InactivateAsync(clientSeq)).ReturnsAsync(expectedResult);

            // Act
            var result = await _service.InactivateAsync(clientSeq);

            // Assert
            Assert.Equal(expectedResult, result);
            _mockClientRepositoryTransactional.Verify(r => r.InactivateAsync(clientSeq), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), expectedResult ? Times.Once : Times.Never);
        }
    }
}