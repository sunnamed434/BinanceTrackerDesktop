using BinanceTrackerDesktop.Core.Notification.Popup.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;

namespace BinanceTrackerDesktop.Core.Notification.Popup.Builder
{
    public sealed class PopupBuilder : IPopupBuilder
    {
        private readonly Popup popup = Popup.Empty;

        private bool isCarefully = false;



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

        public IPopupBuilder TryWithCarefully()
        {
            if (new BinaryUserDataSaveSystem().Read().IsNotificationsDisabled)
            {
                isCarefully = true;
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
            if (isCarefully)
            {
                return BuildAsMessageBox();
            }

            return popup;
        }

        public IPopup Build(bool sendAnyway)
        {
            if (isCarefully)
            {
                return Build();
            }

            popup.Show(sendAnyway);

            return Build();
        }



        public static implicit operator Popup(PopupBuilder builder)
        {
            return (Popup)builder.Build();
        }
    }
}
