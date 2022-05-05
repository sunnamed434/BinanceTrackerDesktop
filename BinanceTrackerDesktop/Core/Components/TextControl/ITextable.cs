namespace BinanceTrackerDesktop.Core.Components.TextControl
{
    public interface ITextable
    {
        void SetText(string content, Color? color = null);

        void SetBackgroundColor(Color? color);

        void SetForegroundColor(Color? color);

        void SetDefaultTextColor(Color? color);

        void SetDefaultTextColorAndAsCurrentForegroundColor(Color? color);

        void SetDefaultTextColorAndAsCurrentBackgroundColor(Color? color);

        Color? GetDefaultTextColor();
    }
}
