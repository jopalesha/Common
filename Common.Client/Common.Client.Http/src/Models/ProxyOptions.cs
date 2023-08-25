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
        /// Initializes a new instance of the <see cref="ProxyOptions"/> class with credential.
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
        /// Initializes a new instance of the <see cref="ProxyOptions"/> class without credential.
        /// </summary>
        /// <param name="address">Address of the proxy server.</param>
        /// <param name="type">Proxy type.</param>
        public ProxyOptions(Uri address, ProxyType type) : this()
        {
            Address = Check.NotNull(address);
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyOptions"/> class.
        /// </summary>
        protected ProxyOptions()
        {
        }

        /// <summary>
        /// Gets address of the proxy server.
        /// </summary>
        public Uri Address { get; }

        /// <summary>
        /// Gets proxy type.
        /// </summary>
        public ProxyType Type { get; }

        /// <summary>
        /// Gets credentials for password-based authentication schemes.
        /// </summary>
        public NetworkCredential Credentials { get; }
    }
}
