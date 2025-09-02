using System.Collections;
using Xunit;

namespace UnitTestPOC2.Tests.TestData.Product
{
    public class GetByNameProductTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "Product", 2 };
            yield return new object[] { "A", 1 };
            yield return new object[] { "NonExistent", 0 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}