using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;
using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control;
using BinanceTrackerDesktop.Core.Components.TrayControl.Base;
using BinanceTrackerDesktop.Core.Components.TrayControl.Extension;
using BinanceTrackerDesktop.Core.Entry;
using BinanceTrackerDesktop.Core.Notifications.Popup.Builder;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using BinanceTrackerDesktop.Core.Window;

namespace BinanceTrackerDesktop.Core.Forms.Tray
{
    public sealed class BinanceTrackerTray : TrayComponentControlBase, IAwaitableSingletonObject, IAwaitableComponentObserverInstance, IAwaitableComponentExecute
    {
        private readonly ProcessWindowHelper processWindowHelper;

        private readonly MenuStripComponentItemControl applicationOpenItemControl;

        private readonly MenuStripComponentItemControl notificationsItemControl;

        private readonly MenuStripComponentItemControl applicationQuitItemControl;

        private static BinanceTrackerTray instance;



        public BinanceTrackerTray(NotifyIcon notifyIcon) : base(notifyIcon)
        {
            instance = this;

            processWindowHelper = new ProcessWindowHelper();

            applicationOpenItemControl = base.GetComponentAt(TrayItemsIdContainer.OpenApplicationUniqueIndex);
            notificationsItemControl = base.GetComponentAt(TrayItemsIdContainer.NotificationsUniqueIndex);
            applicationQuitItemControl = base.GetComponentAt(TrayItemsIdContainer.QuitApplicationUniqueIndex);

            initializeAsync();

            applicationOpenItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.OnClick.OnTriggerEventHandler += onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler += onTrayDoubleClick;
        }



        IAwaitableComponentsObserver IAwaitableComponentObserverInstance.Observer { get; set; }

        object IAwaitableSingletonObject.Instance => instance;



        async Task IAwaitableComponentExecute.OnExecute()
        {
            applicationOpenItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onApplicationOpenItemClicked;
            notificationsItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onNotificationsItemControlClicked;
            applicationQuitItemControl.EventsContainer.OnClick.OnTriggerEventHandler -= onApplicationQuitItemClicked;
            EventsContainerControl.DoubleClickListener.OnTriggerEventHandler -= onTrayDoubleClick;

            this.HideTray();

            await Task.CompletedTask;
        }

        private async void initializeAsync()
        {
            UserData userData = new BinaryUserDataSaveSystem().Read();
            notificationsItemControl.SetText(getNotificationsText(userData.IsNotificationsEnabled));

            UserWalletCoinResult coinResult = await new UserClient().Wallet.GetBestCoinAsync();
            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage("Tracker Running")
                .WillCloseIn(90)
                .WithOnClickAction(() => processWindowHelper.SetWindowToForeground())
                .WithOnCloseAction(() => new PopupBuilder()
                                              .WithTitle(ApplicationEnviroment.GlobalName)
                                              .WithMessage("Your best for today: " + coinResult.Asset)
                                              .WillCloseIn(90)
                                              .ShowMessageBoxIfShouldOnBuild()
                                              .Build())
                .Build();
        }

        private string getNotificationsText(bool isNotificationsEnabled)
        {
            return isNotificationsEnabled == true ? TrayItemsTextContainer.DisableNotifications : TrayItemsTextContainer.EnableNotifications;
        }



        private void onTrayDoubleClick(EventArgs e)
        {
            processWindowHelper.SetWindowToForeground();
        }

        private void onApplicationOpenItemClicked(EventArgs e)
        {
            onTrayDoubleClick(e);
        }

        private void onNotificationsItemControlClicked(EventArgs e)
        {
            BinaryUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
            IUserDataBuilder userDataBuilder = new UserDataBuilder(saveSystem.Read());

            UserData userData = userDataBuilder.Build();

            userDataBuilder.AddNotificationsStateBasedOnData(!userData.IsNotificationsEnabled);

            userData = userDataBuilder.Build()
                .WriteUserDataThenRead(saveSystem);

            new PopupBuilder()
                .WithTitle(ApplicationEnviroment.GlobalName)
                .WithMessage(userData.IsNotificationsEnabled ? TrayItemsTextContainer.NotificationsEnabled : TrayItemsTextContainer.NotificationsDisabled)
                .WillCloseIn(90)
                .ShowMessageBoxIfShouldOnBuild()
                .Build();

            notificationsItemControl.SetText(getNotificationsText(userData.IsNotificationsEnabled));
        }

        private async void onApplicationQuitItemClicked(EventArgs e)
        {
            this.HideTray();

            await BinanceTrackerEntryPoint.AwaitableComponentsProvider.Observer.CallListenersAsync();
        }



        protected override IEnumerable<MenuStripComponentItemControl> InitializeItems()
        {
            yield return new MenuStripComponentItemControl(TrayItemsTextContainer.OpenApplication, TrayItemsIdContainer.OpenApplicationUniqueIndex);
            yield return new MenuStripComponentItemControl(TrayItemsTextContainer.DisableNotifications, TrayItemsIdContainer.NotificationsUniqueIndex);
            yield return new MenuStripComponentItemControl(TrayItemsTextContainer.QuitApplication, TrayItemsIdContainer.QuitApplicationUniqueIndex);
        }
    }

    public sealed class TrayItemsTextContainer
    {
        public const string OpenApplication = "Open Binance Tracker";

        public const string QuitApplication = "Quit Binance Tracker";

        public const string NotificationsEnabled = "Notifications Enabled";

        public const string NotificationsDisabled = "Notifications Disabled";

        public const string EnableNotifications = "Enable Notifications";

        public const string DisableNotifications = "Disable Notifications";
    }

    public sealed class TrayItemsIdContainer
    {
        public const byte OpenApplicationUniqueIndex = 1;

        public const byte NotificationsUniqueIndex = 2;

        public const byte QuitApplicationUniqueIndex = 3;
    }
}
