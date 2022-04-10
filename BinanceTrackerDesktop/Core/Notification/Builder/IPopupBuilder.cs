using BinanceTrackerDesktop.Core.Notification.API;
using System;
using System.Drawing;

namespace BinanceTrackerDesktop.Core.Notification.Builder
{
    public interface IPopupBuilder
    {
        IPopupBuilder WithTitle(string name);

        IPopupBuilder WithMessage(string content);

        IPopupBuilder WillCloseIn(int value);

        IPopupBuilder WithIcon(Icon icon);

        IPopupBuilder WithOnShowAction(Action callback);

        IPopupBuilder WithOnCloseAction(Action callback);

        IPopupBuilder WithOnClickAction(Action callback);

        IPopupBuilder WithCarefully();

        IPopup Build();

        IPopup Build(bool sendAnyway);
    }
}
