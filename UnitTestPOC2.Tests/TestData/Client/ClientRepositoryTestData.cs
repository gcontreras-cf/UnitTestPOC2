using Domain.Entities;
using ClientEntity = Domain.Entities.Client; // Renamed alias to avoid ambiguity

namespace UnitTestPOC2.Tests.TestData.Client
{
    public static class ClientRepositoryTestData
    {
        public static ClientEntity GetSampleClient()
        {
            return new ClientEntity
            {
                ClientSeq = 1,
                Name = "Sample Client",
                Email = "sample.client@example.com",
                Active = true
            };
        }

        public static List<ClientEntity> GetSampleClients()
        {
            return new List<ClientEntity>
                {
                    new ClientEntity
                    {
                        ClientSeq = 1,
                        Name = "John",
                        Email = "client1@example.com",
                        Active = true
                    },
                    new ClientEntity
                    {
                        ClientSeq = 2,
                        Name = "Jane",
                        Email = "client2@example.com",
                        Active = true
                    }
                };
        }
    }
}