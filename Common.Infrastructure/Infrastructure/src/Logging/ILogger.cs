namespace Jopalesha.Common.Infrastructure.Logging;

/// <summary>
/// Logger.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Log debug info.
    /// </summary>
    /// <param name="message">Message.</param>
    void Debug(string message);

    /// <summary>
    /// Log debug info.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="args">Arguments.</param>
    void Debug(string message, params object[] args);

    /// <summary>
    /// Log info.
    /// </summary>
    /// <param name="message">Message.</param>
    void Info(string message);

    /// <summary>
    /// Log info.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="args">Arguments.</param>
    void Info(string message, params object[] args);

    /// <summary>
    /// Log warning.
    /// </summary>
    /// <param name="message">Message.</param>
    void Warning(string message);

    /// <summary>
    /// Log warning.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="args">Arguments.</param>
    void Warning(string message, params object[] args);

    /// <summary>
    /// Log error.
    /// </summary>
    /// <param name="message">Message.</param>
    void Error(string message);

    /// <summary>
    /// Log error.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="args">Arguments.</param>
    void Error(string message, params object[] args);

    /// <summary>
    /// Log error.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="message">Message.</param>
    void Error(Exception exception, string message);

    /// <summary>
    /// Log error.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="message">Message.</param>
    /// <param name="args">Arguments.</param>
    void Error(Exception exception, string message, params object[] args);

    /// <summary>
    /// Log fatal error.
    /// </summary>
    /// <param name="message">Message.</param>
    void Fatal(string message);

    /// <summary>
    /// Log fatal error.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="args">Template arguments.</param>
    void Fatal(string message, params object[] args);

    /// <summary>
    /// Log fatal error.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="message">Message.</param>
    void Fatal(Exception exception, string message);

    /// <summary>
    /// Log fatal error.
    /// </summary>
    /// <param name="exception">Exception.</param>
    /// <param name="message">Message.</param>
    /// <param name="args">Template arguments.</param>
    void Fatal(Exception exception, string message, params object[] args);
}
