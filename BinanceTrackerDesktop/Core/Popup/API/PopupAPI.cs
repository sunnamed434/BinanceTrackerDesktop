using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Popup.API
{
    public interface IPopup
    {
        string Title { get; set; }

        string Message { get; set; }

        int Timeout { get; set; }

        ToolTipIcon Icon { get; set; }
    }

    public class Popup : IPopup
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int Timeout { get; set; }

        public ToolTipIcon Icon { get; set; }



        public static readonly Popup Empty = new Popup
        {
            Title = string.Empty,
            Message = string.Empty,
            Icon = ToolTipIcon.None,
        };
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
        private static NotifyIcon notifyIcon;



        public static void Initialize(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotificationsControl.notifyIcon = notifyIcon;
        }

      

        public static void Show(Popup popup)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));

            notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, popup.Icon);
        }
    }
}
