using System.Net;
using Jopalesha.Common.Client.Http.Models;

namespace Jopalesha.Common.Client.Http.Components
{
    /// <summary>
    /// <see cref="IWebProxy"/> factory.
    /// </summary>
    public interface IProxyFactory
    {
        /// <summary>
        /// Creates proxy with passed options.
        /// </summary>
        /// <param name="options">Proxy options.</param>
        /// <returns>Web proxy.</returns>
        IWebProxy Create(ProxyOptions options);
    }
}