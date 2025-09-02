using System.Collections;
using Domain.Entities; // Ensure this namespace contains the 'Product' class

namespace UnitTestPOC2.Tests.TestData.Product
{
    public class GetByProductSeqTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Domain.Entities.Product { ProductSeq = 1, Name = "Product A", Price = 10.5m, Active = true }
            };
            yield return new object[]
            {
                new Domain.Entities.Product { ProductSeq = 2, Name = "Product B", Price = 20.0m, Active = true }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}