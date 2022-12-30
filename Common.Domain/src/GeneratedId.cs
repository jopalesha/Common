using Jopalesha.Common.Domain.Exceptions;

namespace Jopalesha.Common.Domain
{
    /// <summary>
    /// Id, which generates after saving in repository.
    /// </summary>
    /// <typeparam name="T">Type of id.</typeparam>
    public class GeneratedId<T> : Id<T>
    {
        /// <inheritdoc/>
        public override T Value => throw new IdNotGeneratedException();
    }
}
