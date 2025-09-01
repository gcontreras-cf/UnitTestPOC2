using System.Collections;
using Domain.Entities;

namespace UnitTestPOC2.Tests.TestData
{
    public class GetByClientSeqTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Client { ClientSeq = 1, Name = "John Doe", Email = "john.doe@example.com", Active = true }
            };
            yield return new object[]
            {
                new Client { ClientSeq = 2, Name = "Jane Doe", Email = "jane.doe@example.com", Active = true }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}