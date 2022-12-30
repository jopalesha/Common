using Jopalesha.CheckWhenDoIt;
using Jopalesha.Common.Domain;

namespace Jopalesha.Common.Data.EntityFramework.Integration
{
    public class TestEntity : Entity<int>
    {
        public TestEntity(string value) : this(value, Id<int>.Generated)
        {
        }

        public TestEntity(string value, Id<int> id) : base(id)
        {
            Value = Check.NotEmpty(value);
        }

        public string Value { get; private set; }

        public void UpdateValue(string value)
        {
            Value = Check.NotEmpty(value);
        }

        public override string ToString() => $"Id: {Id}, Value: {Value}";
    }
}
