using System;
using System.Collections.Generic;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Domain
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        private readonly IEqualityComparer<T> _comparer;

        protected ValueObject(T value) : this(value, EqualityComparer<T>.Default)
        {
        }

        protected ValueObject(T value, IEqualityComparer<T> comparer)
        {
            Value = GetValid(value);
            _comparer = Check.NotNull(comparer);
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

        public virtual bool Equals(ValueObject<T> other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || _comparer.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ValueObject<T>) obj);
        }

        public override int GetHashCode()
        {
            return _comparer.GetHashCode(Value);
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !Equals(left, right);
        }

        private T GetValid(T value)
        {
            Validate(value);
            return value;
        }
    }
}
