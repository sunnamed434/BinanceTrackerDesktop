namespace BinanceTrackerDesktop.Core.Notifications.Popup.Builder
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

        IPopupBuilder ShowMessageBoxIfShouldOnBuild();

        IPopup BuildAsMessageBox();

        IPopup Build();

        IPopup Build(bool sendAnyway);
    }
}
