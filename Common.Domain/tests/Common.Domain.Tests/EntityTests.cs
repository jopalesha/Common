using System;
using JetBrains.Annotations;
using Jopalesha.Common.Domain;
using Xunit;

namespace Common.Domain.Tests
{
    public class EntityTests
    {
        [Fact]
        public void Entities_WithDifferentId_NotEquals()
        {
            var entity1 = new TestEntity("id");
            var entity2 = new TestEntity("NewId");

            Verify(entity1, entity2, false);
        }

        [Fact]
        public void Entities_WithSameId_Equals()
        {
            var entity1 = new TestEntity("id");
            var entity2 = new TestEntity("ID");

            Verify(entity1, entity2, true);
        }

        [AssertionMethod]
        private static void Verify(TestEntity value1, TestEntity value2, bool isEqual)
        {
            Assert.True(value1.Equals(value2) == isEqual);
            Assert.True(Equals(value1, value2) == isEqual);
            Assert.Equal(isEqual, value1.GetHashCode() == value2.GetHashCode());
        }

        private class TestEntity : Entity<string>
        {
            public TestEntity(string id) : base(new Key<string>(id), StringComparer.OrdinalIgnoreCase)
            {
            }
        }
    }
}
