using Jopalesha.Common.Infrastructure.Logging;
using Moq;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Xunit;
using Xunit.Sdk;

namespace Jopalesha.Common.Tests
{
    /// <summary>
    /// DI container test helper.
    /// </summary>
    public abstract class ContainerTest
    {
        /// <summary>
        /// Gets default scoped lifestyle.
        /// </summary>
        protected virtual ScopedLifestyle Scope => new AsyncScopedLifestyle();

        /// <summary>
        /// Verify DI container setup.
        /// </summary>
        [Fact]
        public void Container_is_verified()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = Scope;

            Register(container);
            container.RegisterSingleton<ILogger>(() => new OutputLogger(new TestOutputHelper()));
            container.Verify();
        }

        /// <summary>
        /// DI setup.
        /// </summary>
        /// <param name="container">Di container.</param>
        protected abstract void Register(Container container);
    }
}
