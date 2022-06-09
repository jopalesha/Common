using Jopalesha.CheckWhenDoIt;

// ReSharper disable once CheckNamespace
namespace Jopalesha.Common.Client.Http.Models;

/// <summary>
/// Http client options from configuration file.
/// </summary>
internal class HttpClientConfigOptions : HttpClientOptions
{
    public HttpClientConfigOptions(string section)
    {
        Section = Check.NotEmpty(section);
    }

    /// <summary>
    /// Section in configuration file.
    /// </summary>
    public string Section { get; }
}