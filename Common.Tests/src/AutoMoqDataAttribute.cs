using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using JetBrains.Annotations;

namespace Jopalesha.Common.Tests
{
    [UsedImplicitly]
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
