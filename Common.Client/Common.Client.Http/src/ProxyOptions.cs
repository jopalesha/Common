namespace Jopalesha.Common.Client.Http
{
    public class ProxyOptions
    {
        public string Address { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public ProxyType Type { get; set; }

        public static ProxyOptions FromConfig => new ConfigProxyOptions();
    }

    internal class ConfigProxyOptions : ProxyOptions
    {
    }
}
