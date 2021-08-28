namespace BinanceTrackerDesktop.Forms.SystemTray.API
{
    public interface ISystemTrayFormControl
    {
        void Close();

        void ChangeMenuItemTitle(int index, string to);
    }
}
