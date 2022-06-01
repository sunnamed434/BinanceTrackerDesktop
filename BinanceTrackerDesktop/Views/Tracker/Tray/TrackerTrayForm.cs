using BinanceTrackerDesktop.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Awaitable.Awaitables;
using BinanceTrackerDesktop.Awaitable.Observer;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Entry;
using BinanceTrackerDesktop.Expandables;
using BinanceTrackerDesktop.Notifications;
using BinanceTrackerDesktop.Notifications.Popup.Builder;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.Window.Helper;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Forms.Tray;

public sealed partial class TrackerTrayForm : Form,
    IAwaitableSingletonObject,
    IAwaitableObserverInstance,
    IAwaitableExecute,
    IInitializableExpandable<ToolStripMenuItem, byte>
{
    private readonly ContextMenuStripExpandableDesignable expandable;

    private readonly IProcessWindowHelper processWindowHelper;

    private readonly ToolStripMenuItem applicationToolStripMenuItem;

    private readonly ToolStripMenuItem notificationsToolStripMenuItem;

    private readonly ToolStripMenuItem quitApplicationToolStripMenuItem;

    private static TrackerTrayForm instance;


    public TrackerTrayForm()
    {
        instance = this;

        InitializeComponent();

        Icon applicationIcon = ApplicationDirectories.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();

        this.NotifyIcon.ContextMenuStrip = ContextMenuStrip;
        this.NotifyIcon.ContextMenuStrip.RenderMode = ToolStripRenderMode.System;
        this.NotifyIcon.Text = ApplicationEnviroment.GlobalName;
        this.NotifyIcon.Icon = applicationIcon;
        base.Icon = applicationIcon;

        NotificationsSender.Initialize(this.NotifyIcon);

        expandable = new ContextMenuStripExpandableDesignable(this.ContextMenuStrip);
        expandable.AddComponents(this);
        processWindowHelper = new ProcessWindowHelper();
        applicationToolStripMenuItem = expandable.GetComponentOrDefault(TrayItemsIdContainer.OpenApplicationUniqueIndex);
        notificationsToolStripMenuItem = expandable.GetComponentOrDefault(TrayItemsIdContainer.NotificationsUniqueIndex);
        quitApplicationToolStripMenuItem = expandable.GetComponentOrDefault(TrayItemsIdContainer.QuitApplicationUniqueIndex);

        applicationToolStripMenuItem.MouseDown += onApplicationOpenItemClicked;
        notificationsToolStripMenuItem.MouseDown += onNotificationsItemControlClicked;
        quitApplicationToolStripMenuItem.MouseDown += onApplicationQuitItemClicked;
        this.NotifyIcon.DoubleClick += onTrayDoubleClick;
    }

    

    IAwaitablesObserver IAwaitableObserverInstance.Observer { get; set; }

    object IAwaitableSingletonObject.Instance => instance;



    void IAwaitableExecute.OnExecute()
    {
        applicationToolStripMenuItem.MouseDown -= onApplicationOpenItemClicked;
        notificationsToolStripMenuItem.MouseDown -= onNotificationsItemControlClicked;
        quitApplicationToolStripMenuItem.MouseDown -= onApplicationQuitItemClicked;

        this.closeTray();
    }

    private void closeTray()
    {
        using (this.NotifyIcon)
        {
            this.NotifyIcon.Visible = false;
            this.NotifyIcon.Icon.Dispose();
        }
    }

    private string getNotificationsText(bool isNotificationsEnabled)
    {
        return isNotificationsEnabled == true ? TrayItemsTextContainer.DisableNotifications : TrayItemsTextContainer.EnableNotifications;
    }

    private void onTrayDoubleClick(object sender, EventArgs e)
    {
        processWindowHelper.SetWindowToForeground();
    }

    private void onApplicationOpenItemClicked(object sender, MouseEventArgs e)
    {
        onTrayDoubleClick(sender, e);
    }

    private void onNotificationsItemControlClicked(object sender, MouseEventArgs e)
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

        notificationsToolStripMenuItem.Text = getNotificationsText(userData.IsNotificationsEnabled);
    }

    private async void onApplicationQuitItemClicked(object sender, MouseEventArgs e)
    {
        closeTray();

        await BinanceTrackerEntryPoint.AwaitablesProvider.Observer.CallListenersAsync();
    }



    IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> IInitializableExpandable<ToolStripMenuItem, byte>.InitializeItems()
    {
        yield return new KeyValuePair<byte, ToolStripMenuItem>(TrayItemsIdContainer.OpenApplicationUniqueIndex, new ToolStripMenuItem(TrayItemsTextContainer.OpenApplication));
        yield return new KeyValuePair<byte, ToolStripMenuItem>(TrayItemsIdContainer.NotificationsUniqueIndex, new ToolStripMenuItem(TrayItemsTextContainer.DisableNotifications));
        yield return new KeyValuePair<byte, ToolStripMenuItem>(TrayItemsIdContainer.QuitApplicationUniqueIndex, new ToolStripMenuItem(TrayItemsTextContainer.QuitApplication));
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
