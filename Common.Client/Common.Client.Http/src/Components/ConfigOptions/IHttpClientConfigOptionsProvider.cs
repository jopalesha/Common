using Jopalesha.Common.Client.Http.Models;

namespace Jopalesha.Common.Client.Http.Components.ConfigOptions;

/// <summary>
/// <see cref="HttpClientOptions" /> provider.
/// </summary>
internal interface IHttpClientConfigOptionsProvider
{
    /// <summary>
    /// Get options from config.
    /// </summary>
    /// <param name="section">Section in configuration file.</param>
    /// <returns>Client options.</returns>
    HttpClientOptions Get(string section);
}