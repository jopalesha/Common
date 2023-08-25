using Jopalesha.CheckWhenDoIt;

// ReSharper disable once CheckNamespace
namespace Jopalesha.Common.Client.Http.Models
{
    /// <summary>
    /// Http client options from configuration file.
    /// </summary>
    internal class HttpClientConfigOptions : HttpClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfigOptions"/> class.
        /// </summary>
        /// <param name="section">Config section.</param>
        public HttpClientConfigOptions(string section)
        {
            Section = Check.NotEmpty(section);
        }

        /// <summary>
        /// Gets section in configuration file.
        /// </summary>
        public string Section { get; }
    }
}
