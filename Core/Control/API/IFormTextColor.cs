using System.Drawing;

namespace BinanceTrackerDesktop.Core.Control.API
{
    public interface IFormTextColor : IFormText
    {
        void SetTextSync(string content, Color color);

        void SetTextColor(Color color);

        void SetDefaultTextColor(Color color);

        Color GetDefaultTextColor();
    }

    public interface IFormText
    {
        void SetText(string content);
    }
}
