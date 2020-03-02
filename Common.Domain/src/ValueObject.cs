using System.Collections.Generic;
using System.Linq;

namespace Jopalesha.Common.Domain
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityMembers();

        public static bool operator ==(ValueObject left, ValueObject right) => Equals(left, right);

        public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);

        public override bool Equals(object obj)
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

        public override int GetHashCode()
        {
            return GetEqualityMembers()
                .Aggregate(1, (current, obj) => current * 23 + (obj?.GetHashCode() ?? 0));
        }
    }
}
