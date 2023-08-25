using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Jopalesha.Common.Data.EntityFramework.Integration
{
    public class TestContextFactory : IDesignTimeDbContextFactory<TestContext>
    {
        public TestContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlServer(@"Server=localhost;Database=Test;user=sa;password=P@ssw0rd");

            return new TestContext(dbContextOptionsBuilder.Options);
        }
    }
}
