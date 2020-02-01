namespace Jopalesha.Common.Hosting
{
    public class StartupOptions : IStartupOptions
    {
        public StartupOptions() : this(false)
        {
        }

        public StartupOptions(bool isSwaggerEnabled)
        {
            IsSwaggerEnabled = isSwaggerEnabled;
        }

        public bool IsSwaggerEnabled { get; }

        public static IStartupOptions Default => new StartupOptions();
    }
}
