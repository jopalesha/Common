using System.Collections.Generic;
using JetBrains.Annotations;
using Jopalesha.Common.Domain;
using Xunit;

namespace Common.Domain.Tests
{
    public class ValueObjectTests
    {
        [Fact]
        public void DifferentValueObjects_NotEquals()
        {
            var value1 = new TestValueObject(1);
            var value2 = new TestValueObject(2);

            Verify(value1, value2, false);
        }

        [Fact]
        public void SameValueObject_Equals()
        {
            var value1 = new TestValueObject(1);
            var value2 = new TestValueObject(1);

            Verify(value1, value2, true);
        }

        [Fact]
        public void SameStringValueObject_Equals()
        {
            var value1 = new StringValueObject("value");
            var value2 = new StringValueObject("Value");

            Verify(value1, value2, true);
        }

        [AssertionMethod]
        private static void Verify(ValueObject value1, ValueObject value2, bool isEqual)
        {
            Assert.True(value1.Equals(value2) == isEqual);
            Assert.True(Equals(value1, value2) == isEqual);
            Assert.Equal(isEqual, value1.GetHashCode() == value2.GetHashCode());
        }

        private class TestValueObject : ValueObject
        {
            public TestValueObject(int value)
            {
                Value = value;
            }

            private int Value { get; }

            protected override IEnumerable<object> GetEqualityMembers()
            {
                yield return Value;
            }
        }

        private class StringValueObject : ValueObject
        {
            public StringValueObject(string value)
            {
                Value = value;
            }

            private string Value { get; }

            protected override IEnumerable<object> GetEqualityMembers()
            {
                yield return Value.ToLower();
            }
        }
    }
}
