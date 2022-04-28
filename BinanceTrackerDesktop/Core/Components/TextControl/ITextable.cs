using System.Drawing;

namespace BinanceTrackerDesktop.Core.Components.TextControl
{
    public interface ITextable
    {
        void SetText(string content, Color? color = null);

        void SetColor(Color? color);

        void SetDefaultTextColor(Color? color);

        Color? GetDefaultTextColor();
    }
}
