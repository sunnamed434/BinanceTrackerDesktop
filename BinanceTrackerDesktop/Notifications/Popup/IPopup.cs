namespace BinanceTrackerDesktop.Notifications.Popup;

public interface IPopup
{
    string Title { get; set; }

    string Message { get; set; }

    int Timeout { get; set; }

    Icon Icon { get; set; }

    Action OnShow { get; set; }

    Action OnClose { get; set; }

    Action OnClick { get; set; }
}
