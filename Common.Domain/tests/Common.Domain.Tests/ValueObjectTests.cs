using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace Jopalesha.Common.Domain.Tests
{
    public class ValueObjectTests
    {
        [Fact]
        public void Equals_ForDifferentObjects_ReturnsFalse()
        {
            var value1 = new TestValueObject(1);
            var value2 = new TestValueObject(2);

            Verify(value1, value2, false);
        }

        [Fact]
        public void Equals_ForSameValueObject_ReturnsTrue()
        {
            var value1 = new TestValueObject(1);
            var value2 = new TestValueObject(1);

            Verify(value1, value2, true);
        }

        [Fact]
        public void Equals_ForSameStringValueObjects_ReturnsTrue()
        {
            var value1 = new StringValueObject("value");
            var value2 = new StringValueObject("Value");

            Verify(value1, value2, true);
        }

        [Fact]
        public void Equals_ForComplexObjects_ReturnsTrue()
        {
            var value1 = new ComplexObject(new StringValueObject("value"));
            var value2 = new ComplexObject(new StringValueObject("value"));

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

        private class ComplexObject : ValueObject
        {
            private readonly StringValueObject _valueObject;

            public ComplexObject(StringValueObject valueObject)
            {
                _valueObject = valueObject;
            }

            protected override IEnumerable<object> GetEqualityMembers()
            {
                yield return _valueObject;
            }
        }
    }
}
