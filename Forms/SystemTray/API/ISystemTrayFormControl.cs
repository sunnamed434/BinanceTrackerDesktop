using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray.API
{
    public interface ISystemTrayFormControl
    {
        NotifyIcon NotifyIcon { get; }

        void Close();

        void ChangeMenuItemTitle(int index, string to);
    }
}
