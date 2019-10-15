using System.Threading;
using System.Threading.Tasks;

namespace Jopalesha.Common.Infrastructure.Data
{
    public interface ISaveContext
    {
        void Save();

        Task SaveAsync(CancellationToken token  = default);
    }
}
