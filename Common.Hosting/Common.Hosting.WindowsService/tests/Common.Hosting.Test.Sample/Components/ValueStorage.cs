namespace Jopalesha.Common.Hosting.Test.Sample.Components
{
    public class ValueStorage : IValueStorage
    {
        public volatile int Value; 

        public int Get()
        {
            return Value;
        }

        public void Set(int value)
        {
            Value = value;
        }

        public void Remove()
        {
            Value = 0;
        }
    }
}