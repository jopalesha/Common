using Jopalesha.Common.Infrastructure.Logging;
using Moq;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Xunit;

namespace Jopalesha.Common.Tests
{
    public abstract class ContainerTest
    {
        [Fact]
        public void Container_is_verified()
        {
            var mock = new Mock<ILoggerFactory>();
            mock.Setup(x => x.Create()).Returns(new Mock<ILogger>().Object);
            LoggerFactory.SetCurrent(mock.Object);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = Scope;

            Register(container);
            container.RegisterSingleton(LoggerFactory.Create);
            container.Verify();
        }

        protected virtual ScopedLifestyle Scope => new AsyncScopedLifestyle();

        protected abstract void Register(Container container);
    }
}
