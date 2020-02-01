using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Jopalesha.Common.Infrastructure.Helpers;
using Xunit;

namespace Common.Infrastructure.Tests
{
    public class CheckHelperTests
    {
        [Fact]
        public void IsNullOrEmpty_ForEnumerable_ReturnsExpected()
        {
            Assert.True(Check.NotNullOrEmpty(new List<int> {5}).Any());
            Assert.True(Check.NotNullOrEmpty(new Dictionary<int, int> {{5, 5}}).Any());
            Assert.Throws<ArgumentException>(() => Check.NotNullOrEmpty(Enumerable.Empty<int>()).Any());
            Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty((IEnumerable) null));
        }

        [Fact]
        public void IsNullOrEmpty_ForString_ReturnsExpected()
        {
            Assert.True(!string.IsNullOrWhiteSpace(Check.NotNullOrEmpty("val")));
            Assert.Throws<ArgumentNullException>(() => Check.NotNullOrEmpty(null));
            Assert.Throws<ArgumentException>(() => Check.NotNullOrEmpty(""));
        }

        [Fact]
        public void IsTrue_ReturnsExpected()
        {
            Check.IsTrue(true);
            Assert.True(Check.IsTrue(5, It.IsPositive.Integer) == 5);
            Assert.Throws<ArgumentException>(()=>Check.IsTrue(false));
        }
    }
}
