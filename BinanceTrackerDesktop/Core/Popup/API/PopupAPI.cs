using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Popup.API
{
    public class Popup
    {
        public string Title;

        public string Message;

        public int Interval;

        public ToolTipIcon Icon;



        public static readonly Popup Empty = new Popup();
    }

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

        public PopupBuilder WithInterval(int value)
        {
            popup.Interval = value;
            return this;
        }

        public PopupBuilder WithImage(ToolTipIcon icon)
        {
            popup.Icon = icon;
            return this;
        }

        public Popup Build()
        {
            return popup;
        }



        public static implicit operator Popup(PopupBuilder builder)
        {
            return builder.Build();
        }
    }

    public class NotificationsControl
    {
        private readonly NotifyIcon notifyIcon;



        public NotificationsControl(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            this.notifyIcon = notifyIcon;
        }



        public void Show(Popup popup)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));

            this.notifyIcon.ShowBalloonTip(popup.Interval, popup.Title, popup.Message, popup.Icon);
        }
    }
}
