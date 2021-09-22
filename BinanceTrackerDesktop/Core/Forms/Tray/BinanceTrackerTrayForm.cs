using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.DirectoryFiles.API;
using BinanceTrackerDesktop.Core.Forms.Tray.API;
using BinanceTrackerDesktop.Core.Popup.API;
using System;
using System.Drawing;
using System.Windows.Forms;
using static BinanceTrackerDesktop.Core.DirectoryFiles.API.DirectoryImagesControl;

namespace BinanceTrackerDesktop.Core.Forms.Tray
{
    public partial class BinanceTrackerTrayForm : Form
    {
        private readonly ISafelyComponentControl formSafelyCloseControl;

        private readonly BinanceTrackerTray tray;



        public BinanceTrackerTrayForm(ISafelyComponentControl formSafelyCloseControl)
        {
            InitializeComponent();

            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            this.formSafelyCloseControl = formSafelyCloseControl;

            Icon applicationIcon = new ApplicationDirectoriesControl().Folders.Resources.Images.GetDirectoryFileAt(RegisteredImages.ApplicationIcon).Icon;

            this.NotifyIcon.ContextMenuStrip = ContextMenuStrip;
            this.NotifyIcon.ContextMenuStrip.RenderMode = ToolStripRenderMode.System;
            this.NotifyIcon.Text = TrayItemsTextContainer.ApplicationName;
            this.NotifyIcon.Icon = applicationIcon;
            base.Icon = applicationIcon;

            NotificationsControl.Initialize(this.NotifyIcon);
            tray = new BinanceTrackerTray(this.NotifyIcon, this.formSafelyCloseControl);

            this.NotifyIcon.MouseClick += (s, e) => tray.EventsContainerControl.MouseClickListener.TriggerEvent(e);
            this.NotifyIcon.DoubleClick += (s, e) => tray.EventsContainerControl.DoubleClickListener.TriggerEvent(e);
        }
    }
}
