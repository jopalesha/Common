using System;
using System.Net;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Client.Http.Models
{
    /// <summary>
    /// Http client proxy options.
    /// </summary>
    public class ProxyOptions
    {
        /// <summary>
        /// Constructor with credentials.
        /// </summary>
        /// <param name="address">Address of the proxy server.</param>
        /// <param name="type">Proxy type.</param>
        /// <param name="credential">Credentials for password-based authentication schemes.</param>
        public ProxyOptions(
            Uri address, 
            ProxyType type, 
            NetworkCredential credential) : this(address, type)
        {
            Credentials = Check.NotNull(credential);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="address">Address of the proxy server.</param>
        /// <param name="type">Proxy type.</param>
        public ProxyOptions(Uri address, ProxyType type) : this()
        {
            Address = Check.NotNull(address);
            Type = type;
        }

        /// <summary>
        /// Protected constructor.
        /// </summary>
        protected ProxyOptions()
        {
        }

        /// <summary>
        /// Address of the proxy server.
        /// </summary>
        public Uri Address { get; }

        /// <summary>
        /// Proxy type.
        /// </summary>
        public ProxyType Type { get; }

        /// <summary>
        /// Credentials for password-based authentication schemes.
        /// </summary>
        public NetworkCredential Credentials { get; }
    }
}
