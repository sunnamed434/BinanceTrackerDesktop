using BinanceTrackerDesktop.Core.Notification.API;
using BinanceTrackerDesktop.Core.Notification.Extension;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Notification.Builder
{
    public class PopupBuilder
    {
        private readonly Popup popup = Popup.Empty;



        public PopupBuilder WithTitle(string name)
        {
            popup.Title = name;
            return this;
        }

        public PopupBuilder WithMessage(string content)
        {
            popup.Message = content;
            return this;
        }

        public PopupBuilder WillCloseIn(int value)
        {
            popup.Timeout = value;
            return this;
        }

        public PopupBuilder WithImage(ToolTipIcon icon)
        {
            popup.Icon = icon;
            return this;
        }

        public PopupBuilder WithOnShowAction(Action callback)
        {
            popup.OnShow = callback;
            return this;
        }

        public PopupBuilder WithOnCloseAction(Action callback)
        {
            popup.OnClose = callback;
            return this;
        }

        public PopupBuilder WithOnClickAction(Action callback)
        {
            popup.OnClick = callback;
            return this;
        }

        public Popup Build()
        {
            return popup;
        }

        public Popup Build(bool sendAnyway)
        {
            popup.Show(sendAnyway);

            return Build();
        }



        public static implicit operator Popup(PopupBuilder builder)
        {
            return builder.Build();
        }
    }
}
