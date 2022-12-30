namespace Jopalesha.Common.Infrastructure.Configuration
{
    /// <summary>
    /// Configuration.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Get connection string.
        /// </summary>
        /// <param name="name">Connection string name.</param>
        /// <returns>Connection string.</returns>
        string GetConnection(string name);

        /// <summary>
        /// Get value.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="name">Section name.</param>
        /// <returns>Value.</returns>
        T GetValue<T>(string name);
    }
}
