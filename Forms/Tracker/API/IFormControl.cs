using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.Tracker.API
{
    public interface IFormControl
    {
        FormWindowState WindowState { get; }

        void Show();

        void Hide();

        void Close();
    }
}
