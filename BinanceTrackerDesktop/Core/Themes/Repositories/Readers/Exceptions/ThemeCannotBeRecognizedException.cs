namespace BinanceTrackerDesktop.Core.Themes.Repositories.Readers.Exceptions
{
    public sealed class ThemeCannotBeRecognizedException : Exception
    {
        public ThemeCannotBeRecognizedException(string message) : base(string.Format("Theme cannot be recognized: {0}", message))
        {
        }

        public ThemeCannotBeRecognizedException()
        {
        }
    }
}
