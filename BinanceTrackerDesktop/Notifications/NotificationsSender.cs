using BinanceTrackerDesktop.User.Data.Save.Binary;

namespace BinanceTrackerDesktop.Notifications;

public sealed class NotificationsSender
{
    private static NotifyIcon notifyIcon;

    private static Popup.Popup lastUsedPopup;



    public static void Initialize(NotifyIcon notifyIcon)
    {
        NotificationsSender.notifyIcon = notifyIcon ?? throw new ArgumentNullException(nameof(notifyIcon));

        notifyIcon.BalloonTipShown += onPopupShown;
        notifyIcon.BalloonTipClicked += onPopupClicked;
        notifyIcon.BalloonTipClosed += onPopupClosed;
    }



    public static void Show(Popup.Popup popup, bool ignoreUserNotificationsState = false)
    {
        if (popup == null)
        {
            throw new ArgumentNullException(nameof(popup));
        }

        notifyIcon.Icon = popup.Icon;
        lastUsedPopup = popup;

        if (ignoreUserNotificationsState)
        {
            notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, ToolTipIcon.None);
        }
        else if (new BinaryUserDataSaveSystem().Read().IsNotificationsEnabled)
        {
            notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, ToolTipIcon.None);
        }
    }



    private static void onPopupShown(object sender, EventArgs e)
    {
        if (lastUsedPopup != null)
        {
            lastUsedPopup?.OnShow?.Invoke();
            lastUsedPopup.OnShow = null;
        }
    }

    private static void onPopupClicked(object sender, EventArgs e)
    {
        if (lastUsedPopup != null)
        {
            lastUsedPopup.OnClick?.Invoke();
            lastUsedPopup.OnClick = null;
        }

        onPopupClosed(sender, e);
    }

    private static void onPopupClosed(object sender, EventArgs e)
    {
        if (lastUsedPopup != null)
        {
            lastUsedPopup.OnClose?.Invoke();
            lastUsedPopup.OnClose = null;
        }

        lastUsedPopup = null;
    }
}
