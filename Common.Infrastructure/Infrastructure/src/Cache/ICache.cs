using System.Threading;
using System.Threading.Tasks;

namespace Jopalesha.Common.Infrastructure.Cache
{
    public interface ICache
    {
        Task Add<T>(string key, T value, CancellationToken token = default);

        Task<T> Get<T>(string key, CancellationToken token = default);

        Task<T> Find<T>(string key, CancellationToken token = default);

        Task Delete(string key, CancellationToken token = default);

        Task Clear(CancellationToken token = default);
    }
}
