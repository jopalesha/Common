using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Domain
{
    /// <summary>
    /// identifier.
    /// </summary>
    /// <typeparam name="T">Type of id.</typeparam>
    public class Id<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Id{T}"/> class.
        /// </summary>
        /// <param name="value">Id value.</param>
        public Id(T value)
        {
            Value = Check.NotDefault(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id{T}"/> class.
        /// </summary>
        protected Id()
        {
        }

        /// <summary>
        /// Gets id value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets generated id.
        /// </summary>
        public static GeneratedId<T> Generated => new();

        /// <summary>
        /// Implicit operator.
        /// </summary>
        /// <param name="value">Value.</param>
        public static implicit operator Id<T>(T value) => new(value);
    }

    /// <summary>
    /// Id, which generates after saving in repository.
    /// </summary>
    /// <typeparam name="T">Type of id.</typeparam>
    public class GeneratedId<T> : Id<T>
    {
    }
}
