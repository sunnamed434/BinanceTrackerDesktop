﻿using BinanceTrackerDesktop.Notifications.Popup.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Data.Value;
using System.Text;

namespace BinanceTrackerDesktop.Notifications.Popup.Builder;

public sealed class PopupBuilder : IPopupBuilder
{
    private readonly Popup popup;

    private bool showMessageBoxIfShould;



    public PopupBuilder()
    {
        popup = Popup.Empty;
        showMessageBoxIfShould = false;
    }



    public IPopupBuilder WithTitle(string name)
    {
        popup.Title = name;
        return this;
    }

    public IPopupBuilder WithMessage(string content)
    {
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

    public IPopupBuilder WithIcon(Icon icon)
    {
        popup.Icon = icon;
        return this;
    }

    public IPopupBuilder WithOnShowAction(Action callback)
    {
        popup.OnShow = callback;
        return this;
    }

    public IPopupBuilder WithOnCloseAction(Action callback)
    {
        popup.OnClose = callback;
        return this;
    }

    public IPopupBuilder WithOnClickAction(Action callback)
    {
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

    public IPopup BuildAsMessageBox()
    {
        MessageBox.Show(popup.Message, popup.Title);

        return popup;
    }

    public IPopup Build()
    {
        if (showMessageBoxIfShould)
        {
            return BuildAsMessageBox();
        }

        return popup;
    }

    public IPopup Build(bool ignoreUserNotificationsState)
    {
        if (showMessageBoxIfShould)
        {
            return Build();
        }

        popup.Show(ignoreUserNotificationsState);

        return Build();
    }



    public static implicit operator Popup(PopupBuilder builder)
    {
        return (Popup)builder.Build();
    }
}
