using System.Drawing;

namespace BinanceTrackerDesktop.Core.Controls.API
{
    public interface IFormTextColorControl
    {
        void SetTextSync(string content, Color color);

        void SetText(string content);

        void SetTextColor(Color color);
    }
}
