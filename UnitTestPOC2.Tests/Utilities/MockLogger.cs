using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTestPOC2.Tests.Utilities
{
    public static class MockLogger
    {
        public static Mock<ILogger<T>> CreateLogger<T>()
        {
            return new Mock<ILogger<T>>();
        }
    }
}