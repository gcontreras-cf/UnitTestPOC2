using System.Collections;
using System.Collections.Generic;

namespace UnitTestPOC2.Tests.TestData
{
    public class GetByNameTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "John", 1 };
            yield return new object[] { "Jane", 1 };
            yield return new object[] { "NonExistent", 0 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}