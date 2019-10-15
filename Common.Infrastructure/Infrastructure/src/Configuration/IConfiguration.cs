namespace Jopalesha.Common.Infrastructure.Configuration
{
    public interface IConfiguration
    {
        string GetConnection(string value);

        T GetSection<T>(string section);
    }
}
