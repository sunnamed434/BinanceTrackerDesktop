using BinanceTrackerDesktop.Core.User.Data.API;
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

        Action OnShow { get; set; }

        Action OnClose { get; set; }

        Action OnClick { get; set; }
    }

    public class Popup : IPopup
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public int Timeout { get; set; }

        public ToolTipIcon Icon { get; set; }

        public Action OnShow { get; set; }

        public Action OnClose { get; set; }

        public Action OnClick { get; set; }



        public static readonly Popup Empty = new Popup
        {
            Title = string.Empty,
            Message = string.Empty,
            Icon = ToolTipIcon.None,
            OnShow = null,
            OnClose = null,
            OnClick = null,
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



        public static implicit operator Popup(PopupBuilder builder)
        {
            return builder.Build();
        }
    }

    public class NotificationsControl
    {
        private static NotifyIcon notifyIcon;

        private static Popup lastUsedPopup;



        public static void Initialize(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotificationsControl.notifyIcon = notifyIcon;

            notifyIcon.BalloonTipShown += onPopupShown;
            notifyIcon.BalloonTipClosed += onPopupClosed;
            notifyIcon.BalloonTipClicked += onPopupClicked;
        }
        
        ~NotificationsControl()
        {
            notifyIcon.BalloonTipClicked -= onPopupClicked;
            notifyIcon.BalloonTipClosed -= onPopupClosed;
            notifyIcon.BalloonTipShown -= onPopupShown;
        }



        public static void Show(Popup popup, bool sendAnyway = false)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));

            lastUsedPopup = popup;

            if (sendAnyway)
            {
                notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, popup.Icon);
            }
            else if (new BinaryUserDataSaveReadSystem().Read().NotificationsEnabled)
            {
                notifyIcon.ShowBalloonTip(popup.Timeout, popup.Title, popup.Message, popup.Icon);
            }
        }



        private static void onPopupClicked(object sender, EventArgs e)
        {
            lastUsedPopup?.OnClick?.Invoke();
        }

        private static void onPopupShown(object sender, EventArgs e)
        {
            lastUsedPopup?.OnShow?.Invoke();
        }

        private static void onPopupClosed(object sender, EventArgs e)
        {
            lastUsedPopup?.OnClose?.Invoke();
            lastUsedPopup = null;
        }
    }
}
