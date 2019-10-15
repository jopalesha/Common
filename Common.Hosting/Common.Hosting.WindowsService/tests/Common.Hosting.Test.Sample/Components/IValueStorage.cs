namespace Jopalesha.Common.Hosting.Test.Sample.Components
{
    public interface IValueStorage
    {
        int Get();

        void Set(int value);

        void Remove();
    }
}