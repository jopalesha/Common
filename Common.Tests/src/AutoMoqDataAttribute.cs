using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using JetBrains.Annotations;

namespace Jopalesha.Common.Tests
{
    /// <summary>
    /// AutoMoq data attribute.
    /// </summary>
    [UsedImplicitly]
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMoqDataAttribute"/> class.
        /// </summary>
        public AutoMoqDataAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
