using System;

namespace BinanceTrackerDesktop.Core.Extension
{
    public static class WithGenericExtension
    {
        public static T With<T>(this T source, Action<T> set)
        {
            set?.Invoke(source);

            return source;
        }

        public static T With<T>(this T source, Action<T> apply, bool when)
        {
            if (when)
                apply?.Invoke(source);

            return source;
        }

        public static T With<T>(this T source, Action<T> apply, Func<bool> when)
        {
            if (when())
                apply?.Invoke(source);

            return source;
        }
    }
}
