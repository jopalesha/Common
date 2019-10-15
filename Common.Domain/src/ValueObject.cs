using System;
using System.Collections.Generic;
using Jopalesha.Common.Infrastructure;

namespace Jopalesha.Common.Domain
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        protected ValueObject(T value)
        {
            Value = GetValid(value);
        }

        public T Value { get; }

        protected virtual void Validate(T value)
        {
            if (value is string valueStr)
            {
                Check.NotNullOrEmpty(valueStr);
            }
            else
            {
                Check.NotNull(value);
            }
        }

        public bool Equals(ValueObject<T> other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ValueObject<T>) obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }

        private T GetValid(T value)
        {
            Validate(value);
            return value;
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !Equals(left, right);
        }
    }
}
