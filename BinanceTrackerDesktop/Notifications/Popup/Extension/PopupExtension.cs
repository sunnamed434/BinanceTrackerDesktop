namespace BinanceTrackerDesktop.Notifications.Popup.Extension;

public static class PopupExtension
{
    public static void Show(this IPopup source, bool ignoreUserNotificationsState = false)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        NotificationsSender.Show(source, ignoreUserNotificationsState);
    }
}
