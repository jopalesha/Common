using System.Collections.Concurrent;
using Jopalesha.Common.Infrastructure.Extensions;
using Xunit;

namespace Common.Infrastructure.Tests
{
    public class ConcurrentQueueExtensionsTests
    {
        [Fact]
        public void DequeueAll_ReturnsAllItems()
        {
            var queue = new ConcurrentQueue<TestClass>();
            const int expectedCount = 3;
            for (var i = 0; i < expectedCount; i++)
            {
                queue.Enqueue(new TestClass {Value = i});
            }

            var actual = queue.DequeueAll();

            Assert.Equal(expectedCount, actual.Count);
            Assert.True(queue.IsEmpty);
        }

        private class TestClass
        {
            public int Value { get; set; }
        }
    }
}
