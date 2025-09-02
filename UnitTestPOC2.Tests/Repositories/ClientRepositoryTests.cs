using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repositories.NoTransactional;
using Microsoft.EntityFrameworkCore;
using UnitTestPOC2.Tests.TestData.Client;
using UnitTestPOC2.Tests.Utilities;
using Xunit;

namespace UnitTestPOC2.Tests.Repositories
{
    public class ClientRepositoryTests
    {
        private readonly AppDbContext _context;
        private readonly ClientRepository _repository;

        public ClientRepositoryTests()
        {
            _context = DbContextUtilities.GetInMemoryDbContext();
            _repository = new ClientRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllActiveClients()
        {
            // Arrange
            var clients = ClientRepositoryTestData.GetSampleClients();
            await _context.Clients.AddRangeAsync(clients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(clients.Count(c => c.Active), result.Count());
        }

        [Theory]
        [InlineData("John", 1)]
        [InlineData("Jane", 1)]
        [InlineData("Nonexistent", 0)]
        public async Task GetByNameAsync_ShouldReturnFilteredClients(string name, int expectedCount)
        {
            // Arrange
            var clients = ClientRepositoryTestData.GetSampleClients();
            await _context.Clients.AddRangeAsync(clients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByNameAsync(name);

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
            var clients = ClientRepositoryTestData.GetSampleClients();
            await _context.Clients.AddRangeAsync(clients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByClientSeqAsync(clientSeq);

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