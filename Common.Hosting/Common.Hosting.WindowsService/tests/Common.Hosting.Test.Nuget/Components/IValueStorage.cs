namespace Common.Hosting.Test.Nuget.Components
{
    public interface IValueStorage
    {
        int Get();

        void Set(int value);

        void Remove();
    }
}