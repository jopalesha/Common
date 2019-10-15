namespace Jopalesha.Common.Hosting.Test.Sample.Components
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
    }
}