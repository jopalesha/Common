using System;
using Jopalesha.Common.Domain;
using Xunit;

namespace Common.Domain.Tests
{
    public class ValueObjectTests
    {
        [Fact]
        public void SameValueObject_Equals_ReturnsTrue()
        {
            var valueObject1 = new TestValueObject("value");
            var valueObject2 = new TestValueObject("value");

            Assert.Equal(valueObject1, valueObject2);
            Assert.Equal(valueObject1.GetHashCode(), valueObject2.GetHashCode());
        }

        [Fact]
        public void SameValueObjectWithCustomComparer_Equals_ReturnsTrue()
        {
            var valueObject1 = new StringValueObject("value");
            var valueObject2 = new StringValueObject("Value");

            Assert.Equal(valueObject1, valueObject2);
            Assert.Equal(valueObject1.GetHashCode(), valueObject2.GetHashCode());
        }

        private class TestValueObject : ValueObject<string>
        {
            public TestValueObject(string value) : base(value)
            {
            }
        }

        private class StringValueObject : ValueObject<string>
        {
            public StringValueObject(string value) : base(value, StringComparer.OrdinalIgnoreCase)
            {
            }
        }
    }
}
