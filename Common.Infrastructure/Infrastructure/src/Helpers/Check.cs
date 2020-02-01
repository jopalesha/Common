using System;
using System.Collections;

namespace Jopalesha.Common.Infrastructure.Helpers
{
    public static class Check
    {
        public static T IsTrue<T>(T value, Func<T, bool> func, string? paramName = null, string? message = null)
        {
            IsTrue(NotNull(func)(value), paramName, message);
            return value;
        }

        public static void IsTrue(bool condition, string? paramName = null, string? message = null)
        {
            if (condition) return;
            throw new ArgumentException(message ?? "Condition is not true", paramName);
        }

        public static T NotNull<T>(T value, string? paramName = null, string? message = null) =>
            value ?? throw (paramName, message) switch
            {
                _ when string.IsNullOrWhiteSpace(message) => new ArgumentNullException(paramName),
                _ => new ArgumentNullException(paramName, message)
            };

        public static TEnumerable NotNullOrEmpty<TEnumerable>(
            TEnumerable values,
            string? paramName = null,
            string? message = null)
            where TEnumerable : IEnumerable
        {
            NotNull(values, paramName, message ?? "Collection is null");
            IsTrue(values.GetEnumerator().MoveNext(), paramName, message ?? "Collection is empty");

            return values;
        }

        public static string NotNullOrEmpty(string value, string? paramName = null, string? message = null)
        {
            NotNull(value, paramName, message ?? "String is null");
            IsTrue(!string.IsNullOrWhiteSpace(value), paramName, "String is empty");

            return value;
        }
    }
}
