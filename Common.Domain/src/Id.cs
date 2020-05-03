using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Domain
{
    public class Id<T>
    {
        public Id(T value)
        {
            Value = Check.NotDefault(value);
        }

        protected Id()
        {
        }

        public T Value { get; }

        public static GeneratedId<T> Generated => new GeneratedId<T>();

        public static implicit operator Id<T>(T value) => new Id<T>(value);
    }

    public class GeneratedId<T> : Id<T>
    {
    }
}