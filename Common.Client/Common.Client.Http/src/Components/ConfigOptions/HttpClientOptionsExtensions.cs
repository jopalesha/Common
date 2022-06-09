// ReSharper disable once CheckNamespace
namespace Jopalesha.Common.Client.Http.Models;

/// <summary>
/// <see cref="HttpClientOptions"/> extensions.
/// </summary>
public partial class HttpClientOptions
{
    /// <summary>
    /// Take client options from configuration file.
    /// </summary>
    /// <param name="section">Configuration section/</param>
    /// <returns>Config options.</returns>
    public static HttpClientOptions FromConfig(string section) => new HttpClientConfigOptions(section);
}