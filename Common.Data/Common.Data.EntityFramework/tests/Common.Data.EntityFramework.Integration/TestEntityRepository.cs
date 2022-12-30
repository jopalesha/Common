using Microsoft.EntityFrameworkCore;

namespace Jopalesha.Common.Data.EntityFramework.Integration
{
    internal class TestEntityRepository : Repository<TestEntity, int>
    {
        public TestEntityRepository(DbContext context) : base(context)
        {
        }
    }

}
