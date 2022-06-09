using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace Jopalesha.Common.Domain.Tests
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

        [Fact]
        public void GetId_WithGeneratedId_ThrowsArgumentException() => Assert.Throws<ArgumentException>(() => new TestEntity().Id);

        [Fact]
        public void Create_WithDefaultId_ThrowsArgumentException() =>
            Assert.Throws<ArgumentNullException>(() => new TestEntity(null));

        [Fact]
        public void Equals_WithGenerateId_ThrowsArgumentException() =>
            Assert.Throws<ArgumentException>(() => Equals(new TestEntity(), new TestEntity()));

        [AssertionMethod]
        private static void Verify(TestEntity value1, TestEntity value2, bool isEqual)
        {
            Assert.True(value1.Equals(value2) == isEqual);
            Assert.True(Equals(value1, value2) == isEqual);
            Assert.Equal(isEqual, value1.GetHashCode() == value2.GetHashCode());
        }

        private class TestEntity : Entity<string>
        {
            public TestEntity() : this(Id<string>.Generated)
            {
            }

            public TestEntity(Id<string> id) : base(id)
            {
            }

            protected override IEqualityComparer<string> Comparer => StringComparer.OrdinalIgnoreCase;
        }
    }
}
