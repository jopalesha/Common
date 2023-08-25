namespace Jopalesha.Common.Hosting
{
    public class StartUpOptionsBuilder
    {
        private bool _isSwaggerEnabled;

        public StartupOptions Build()
        {
            return new StartupOptions(_isSwaggerEnabled);
        }

        public StartUpOptionsBuilder EnableSwagger()
        {
            _isSwaggerEnabled = true;
            return this;
        }
    }
}