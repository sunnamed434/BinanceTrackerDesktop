using BinanceTrackerDesktop.Notifications.Popup.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Data.Value;
using System.Text;

namespace BinanceTrackerDesktop.Notifications.Popup.Builder;

public sealed class PopupBuilder : IPopupBuilder
{
    private readonly IPopup popup;

    private bool showMessageBoxIfShould;



    public PopupBuilder()
    {
        popup = Popup.Empty;
        showMessageBoxIfShould = false;
    }



    public IPopupBuilder WithTitle(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        popup.Title = name;
        return this;
    }

    public IPopupBuilder WithMessage(string content)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        popup.Message = content;
        return this;
    }

    public IPopupBuilder WithMessage(StringBuilder stringBuilder)
    {
        if (stringBuilder == null)
        {
            throw new ArgumentNullException(nameof(stringBuilder));
        }

        popup.Message = stringBuilder.ToString();
        return this;
    }

    public IPopupBuilder WillCloseIn(int value)
    {
        popup.Timeout = value;
        return this;
    }

    public IPopupBuilder WithOnShowAction(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        popup.OnShow = callback;
        return this;
    }

    public IPopupBuilder WithOnCloseAction(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        popup.OnClose = callback;
        return this;
    }

    public IPopupBuilder WithOnClickAction(Action callback)
    {
        if (callback == null)
        {
            throw new ArgumentNullException(nameof(callback));
        }

        popup.OnClick = callback;
        return this;
    }

    public IPopupBuilder ShowMessageBoxIfShouldOnBuild()
    {
        if (UserDataValues.NotificationsDisabled.GetValue())
        {
            showMessageBoxIfShould = true;
        }

        return this;
    }

    public IPopup BuildToMessageBox()
    {
        MessageBox.Show(popup.Message, popup.Title);

        return popup;
    }

    public IPopup Build()
    {
        if (showMessageBoxIfShould)
        {
            return BuildToMessageBox();
        }

        return Build(false);
    }

    public IPopup Build(bool ignoreUserNotificationsState)
    {
        popup.Show(ignoreUserNotificationsState);

        return popup;
    }



    public static implicit operator Popup(PopupBuilder builder)
    {
        return (Popup)builder.Build();
    }
}
