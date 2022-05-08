using BinanceTrackerDesktop.Core.Themes.Provider;

namespace BinanceTrackerDesktop.Core.Themes.Themable
{
    /// <summary>
    /// For types what want to be "themable".
    /// </summary>
    public interface IThemable
    {
        IThemesProvider ThemesProvider { get; }



        /// <summary>
        /// Applying theme.
        /// </summary>
        void ApplyTheme();
    }
}
