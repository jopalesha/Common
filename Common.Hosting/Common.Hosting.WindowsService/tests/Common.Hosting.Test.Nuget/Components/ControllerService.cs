namespace Common.Hosting.Test.Nuget.Components
{
    public class ControllerService : IControllerService
    {
        private readonly IValueStorage _valueStorage;

        public ControllerService(IValueStorage valueStorage)
        {
            _valueStorage = valueStorage;
        }

        public int Get()
        {
            return _valueStorage.Get();
        }

        public void Set(int value)
        {
            _valueStorage.Set(value);
        }
    }
}