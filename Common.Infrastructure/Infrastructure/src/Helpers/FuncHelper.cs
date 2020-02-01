using System;

namespace Jopalesha.Common.Infrastructure.Helpers
{
    public static class It
    {
        public static class IsPositive
        {
            public static Func<int, bool> Integer => it => it >= 0;

            public static Func<double, bool> Double => it => it >= 0;

            public static Func<float, bool> Float => it => it >= 0;

            public static Func<long, bool> Long => it => it >= 0;
        }
    }
}
