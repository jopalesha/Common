namespace Jopalesha.Common.Hosting
{
    public class StartupOptions
    {
        public StartupOptions() : this(false)
        {
        }

        public StartupOptions(bool isSwaggerEnabled)
        {
            IsSwaggerEnabled = isSwaggerEnabled;
        }

        public bool IsSwaggerEnabled { get; }
    }
}
