using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Jopalesha.Common.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static T DeepClone<T>(this T a)
        {
            using var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, a);
            stream.Position = 0;
            return (T)formatter.Deserialize(stream);
        }

        public static bool In<T>(this T x, params T[] set)
        {
            return set.Contains(x);
        }

        public static bool Implements<T>(this Type source) where T : class
        {
            return typeof(T).IsAssignableFrom(source);
        }

        public static T GetValueOrThrow<T>(this T? nullable) where T : struct
        {
            if (nullable.HasValue)
                return nullable.Value;

            throw new ArgumentNullException();
        }
    }
}
