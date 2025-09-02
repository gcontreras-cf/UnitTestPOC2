using Domain.Entities;
using ClientEntity = Domain.Entities.Client; // Renamed alias to avoid ambiguity

namespace UnitTestPOC2.Tests.TestData.Client
{
    public static class ClientServiceTransactionalTestData
    {
        public static ClientEntity GetSampleClient() => new()
        {
            ClientSeq = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            Active = true
        };
    }
}