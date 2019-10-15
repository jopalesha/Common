using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jopalesha.Common.Infrastructure
{
    public static class Check
    {
        public static void IsTrue(Func<bool> func, string message = null)
        {
            if (!func())
            {
                throw new ArgumentException(message);
            }
        }

        public static T IsTrue<T>(T value, Func<T, bool> func, string paramName = null, string message = null)
        {
            if (!func(value))
            {
                throw new ArgumentException(message ?? $"Parameter value - {value} is not valid!",
                    paramName ?? nameof(value));
            }

            return value;
        }

        public static T NotNull<T>(T value, string paramName = null, string message = null)
        {
            if (value == null)
            {
                NotNullOrEmpty(message, paramName);
                throw new ArgumentNullException(paramName, message);
            }
            return value;
        }

        public static string NotNullOrEmpty(string value, string paramName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(paramName);
            }
            return value;
        }

        public static IEnumerable<T> NotNullOrEmpty<T>(IEnumerable<T> values, string paramName = null)
        {
            var enumerable = values as T[] ?? values?.ToArray();

            if (values == null || enumerable.Length == 0)
            {
                throw new ArgumentNullException(paramName);
            }

            return enumerable;
        }

        public static TU NotNullOrEmpty<TU>(TU values, string paramName = null)
            where TU : ICollection
        {
            if (values == null || values.Count == 0)
            {
                throw new ArgumentNullException(paramName);
            }

            return values;
        }
    }
}
