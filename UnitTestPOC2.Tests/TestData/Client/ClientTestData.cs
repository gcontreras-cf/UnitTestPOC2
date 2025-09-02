using System.Collections;
using Domain.Entities;
using ClientEntity = Domain.Entities.Client; // Renamed alias to avoid ambiguity

namespace UnitTestPOC2.Tests.TestData.Client
{
    public class ClientTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new ClientEntity { ClientSeq = 1, Name = "John Doe", Email = "john.doe@example.com", Active = true }
            };
            yield return new object[]
            {
                new ClientEntity { ClientSeq = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Active = true }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}