using System.Collections.Generic;
using System.Linq;

namespace Jopalesha.Common.Domain
{
    /// <summary>
    /// Base value object class.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">First item.</param>
        /// <param name="right">Second item.</param>
        /// <returns>True if items are equal; otherwise, false.</returns>
        public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);

        /// <summary>
        /// Non equality operator.
        /// </summary>
        /// <param name="left">First item.</param>
        /// <param name="right">Second item.</param>
        /// <returns>True if items are not equal; otherwise, false.</returns>
        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject)obj;
            using var thisValues = GetEqualityMembers().GetEnumerator();
            using var otherValues = other.GetEqualityMembers().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (thisValues.Current is null ^ otherValues.Current is null)
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return GetEqualityMembers()
                .Aggregate(1, (current, obj) => (current * 23) + (obj?.GetHashCode() ?? 0));
        }

        /// <summary>
        /// Get list of members to compare.
        /// </summary>
        /// <returns>Sequence of objects.</returns>
        protected abstract IEnumerable<object> GetEqualityMembers();
    }
}
