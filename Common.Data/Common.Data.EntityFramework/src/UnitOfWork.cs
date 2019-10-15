using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Data.EntityFramework
{
    public class UnitOfWork : IUnitWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context) => _context = context;

        public void Save() => _context.SaveChanges();

        public async Task SaveAsync(CancellationToken token) => await _context.SaveChangesAsync(token);
    }
}
