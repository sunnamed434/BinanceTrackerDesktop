using BinanceTrackerDesktop.Awaitable.Awaitables;
using BinanceTrackerDesktop.Awaitable.Observer;
using BinanceTrackerDesktop.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Expandables;
using BinanceTrackerDesktop.Localizations.Data;
using BinanceTrackerDesktop.Notifications;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;
using BinanceTrackerDesktop.Views.Tracker.Tray.Menu.Items;
using static BinanceTrackerDesktop.DirectoryFiles.Controls.Images.ImagesDirectoryFilesControl;

namespace BinanceTrackerDesktop.Forms.Tray;

public sealed partial class TrackerTrayForm : Form,
    IAwaitableSingletonObject,
    IAwaitableObserverInstance,
    IAwaitableExecute,
    IInitializableExpandable<TrackerMenuBase, byte>
{
    private static TrackerTrayForm instance;

    private readonly TrackerContextMenuStripExpandable expandable;

    private readonly TrayTrackerMenuOpen trayTrackerMenuOpen;



    public TrackerTrayForm()
    {
        instance = this;

        InitializeComponent();

        Icon applicationIcon = ApplicationDirectories.Resources.ImagesFolder.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();

        this.NotifyIcon.ContextMenuStrip = ContextMenuStrip;
        this.NotifyIcon.ContextMenuStrip.RenderMode = ToolStripRenderMode.System;
        this.NotifyIcon.Text = LocalizationData.Read().ApplicationName;
        this.NotifyIcon.Icon = applicationIcon;
        base.Icon = applicationIcon;

        NotificationsSender.Initialize(this.NotifyIcon);

        expandable = new TrackerContextMenuStripExpandable(this.ContextMenuStrip);
        expandable.AddComponents(this);
        trayTrackerMenuOpen = (TrayTrackerMenuOpen)expandable.GetComponentOrDefault(TrayItemsIdContainer.OpenApplicationUniqueIndex);

        this.NotifyIcon.DoubleClick += onTrayDoubleClick;
    }

    

    IAwaitablesObserver IAwaitableObserverInstance.Observer { get; set; }

    object IAwaitableSingletonObject.Instance => instance;



    void IAwaitableExecute.OnExecute()
    {
        using (this.NotifyIcon)
        {
            this.NotifyIcon.Visible = false;
            this.NotifyIcon.Icon?.Dispose();
        }
    }

    

    private void onTrayDoubleClick(object sender, EventArgs e)
    {
        trayTrackerMenuOpen.OnClick();
    }



    IEnumerable<KeyValuePair<byte, TrackerMenuBase>> IInitializableExpandable<TrackerMenuBase, byte>.InitializeItems()
    {
        yield return new KeyValuePair<byte, TrackerMenuBase>(TrayItemsIdContainer.OpenApplicationUniqueIndex, new TrayTrackerMenuOpen());
        yield return new KeyValuePair<byte, TrackerMenuBase>(TrayItemsIdContainer.NotificationsUniqueIndex, new TrayTrackerMenuNotifications());
        yield return new KeyValuePair<byte, TrackerMenuBase>(TrayItemsIdContainer.QuitApplicationUniqueIndex, new TrayTrackerMenuQuit(this.NotifyIcon));
    }
}

public sealed class TrayItemsIdContainer
{
    public const byte OpenApplicationUniqueIndex = 1;

    public const byte NotificationsUniqueIndex = 2;

    public const byte QuitApplicationUniqueIndex = 3;
}
