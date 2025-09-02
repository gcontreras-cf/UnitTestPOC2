using Domain.Entities;
using ClientEntity = Domain.Entities.Client; // Renamed alias to avoid ambiguity

namespace UnitTestPOC2.Tests.TestData.Client
{
    public static class ClientServiceTestData
    {
        public static List<ClientEntity> GetSampleClients() => new()
        {
            new ClientEntity { ClientSeq = 1, Name = "John Doe", Email = "john.doe@example.com", Active = true },
            new ClientEntity { ClientSeq = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Active = true }
        };
    }
}