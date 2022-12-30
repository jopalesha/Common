using System.Threading;
using System.Threading.Tasks;
using Jopalesha.Common.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Data.EntityFramework
{
    /// <summary>
    /// Unit of work.
    /// </summary>
    public class UnitOfWork : IUnitWork
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">Data context.</param>
        public UnitOfWork(DbContext context) => _context = context;

        /// <inheritdoc />
        public void Save() => _context.SaveChanges();

        /// <inheritdoc />
        public async Task SaveAsync(CancellationToken token) => await _context.SaveChangesAsync(token);
    }
}
