using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repositories.Transactional;
using UnitTestPOC2.Tests.TestData.Client;
using UnitTestPOC2.Tests.Utilities;
using Xunit;

namespace UnitTestPOC2.Tests.Repositories
{
    public class ClientRepositoryTransactionalTests
    {
        private readonly AppDbContext _context;
        private readonly ClientRepositoryTransactional _repository;

        public ClientRepositoryTransactionalTests()
        {
            _context = DbContextUtilities.GetInMemoryDbContext();
            _repository = new ClientRepositoryTransactional(_context);
        }

        [Fact]
        public async Task InsertAsync_ShouldInsertClient()
        {
            // Arrange
            var client = ClientRepositoryTestData.GetSampleClient();

            // Act
            var result = await _repository.InsertAsync(client);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(client.Name, result.Name);
            Assert.Equal(client.Email, result.Email);

            var insertedClient = await _context.Clients.FindAsync(client.ClientSeq);
            Assert.NotNull(insertedClient);
            Assert.Equal(client.Name, insertedClient!.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateClient()
        {
            // Arrange
            var client = ClientRepositoryTestData.GetSampleClient();
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            client.Name = "Updated Name";
            client.Email = "updated.email@example.com";

            // Act
            var result = await _repository.UpdateAsync(client);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
            Assert.Equal("updated.email@example.com", result.Email);

            var updatedClient = await _context.Clients.FindAsync(client.ClientSeq);
            Assert.NotNull(updatedClient);
            Assert.Equal("Updated Name", updatedClient!.Name);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(999, false)]
        public async Task InactivateAsync_ShouldReturnExpectedResult(int clientSeq, bool expectedResult)
        {
            // Arrange
            var clients = ClientRepositoryTestData.GetSampleClients();
            await _context.Clients.AddRangeAsync(clients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.InactivateAsync(clientSeq);

            // Assert
            Assert.Equal(expectedResult, result);

            var client = await _context.Clients.FindAsync(clientSeq);
            if (expectedResult)
            {
                Assert.NotNull(client);
                Assert.False(client!.Active);
            }
            else
            {
                Assert.Null(client);
            }
        }
    }
}