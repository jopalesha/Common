using System;

namespace Jopalesha.Common.Infrastructure
{
    public static class It
    {
        public static Func<double, bool> IsPositive => it => it >= 0;
    }
}
