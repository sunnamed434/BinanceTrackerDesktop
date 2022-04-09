using BinanceTrackerDesktop.Core.Notification.API;
using System;
using System.Drawing;

namespace BinanceTrackerDesktop.Core.Notification.Builder
{
    public interface IPopupBuilder
    {
        IPopupBuilder WithTitle(string name);

        IPopupBuilder WithMessage(string content);

        PopupBuilder WillCloseIn(int value);

        PopupBuilder WithIcon(Icon icon);

        PopupBuilder WithOnShowAction(Action callback);

        PopupBuilder WithOnCloseAction(Action callback);

        IPopupBuilder WithOnClickAction(Action callback);

        Popup Build();

        Popup Build(bool sendAnyway);
    }
}
