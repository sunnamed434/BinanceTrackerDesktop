using BinanceTrackerDesktop.Core.ApplicationInfo.Environment;
using BinanceTrackerDesktop.Core.DirectoryFiles.Directories;
using BinanceTrackerDesktop.Core.Notification;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.Images.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.Forms.Tray
{
    public partial class BinanceTrackerTrayForm : Form
    {
        private readonly BinanceTrackerTray tray;



        public BinanceTrackerTrayForm()
        {
            InitializeComponent();

            Icon applicationIcon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFile(RegisteredImages.ApplicationIcon).GetIcon();

            this.NotifyIcon.ContextMenuStrip = ContextMenuStrip;
            this.NotifyIcon.ContextMenuStrip.RenderMode = ToolStripRenderMode.System;
            this.NotifyIcon.Text = ApplicationEnviroment.GlobalName;
            this.NotifyIcon.Icon = applicationIcon;
            base.Icon = applicationIcon;

            NotificationsSender.Initialize(this.NotifyIcon);
            tray = new BinanceTrackerTray(this.NotifyIcon);

            this.NotifyIcon.MouseClick += (s, e) => tray.EventsContainerControl.MouseClickListener.TriggerEvent(e);
            this.NotifyIcon.DoubleClick += (s, e) => tray.EventsContainerControl.DoubleClickListener.TriggerEvent(e);
        }
    }
}
