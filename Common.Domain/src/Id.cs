using Jopalesha.CheckWhenDoIt;
#pragma warning disable CS8618

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
        /// Used in child classes.
        /// </summary>
        protected Id()
        {
        }

        /// <summary>
        /// Gets generated id.
        /// </summary>
        public static GeneratedId<T> Generated => new();

        /// <summary>
        /// Gets id value.
        /// </summary>
        public virtual T Value { get; }

        /// <summary>
        /// Implicit operator.
        /// </summary>
        /// <param name="value">Value.</param>
        public static implicit operator Id<T>(T value) => new(value);
    }
}
