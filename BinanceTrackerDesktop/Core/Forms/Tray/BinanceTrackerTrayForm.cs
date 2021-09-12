using BinanceTrackerDesktop.Core.ComponentControl.FormNotifications.API;
using BinanceTrackerDesktop.Core.Files.API;
using BinanceTrackerDesktop.Core.Forms.API;
using BinanceTrackerDesktop.Core.Forms.Tray.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Forms.Tray
{
    public partial class BinanceTrackerTrayForm : Form
    {
        private readonly IFormSafelyComponentControl formSafelyCloseControl;

        private readonly BinanceTrackerTray tray;



        public BinanceTrackerTrayForm(IFormSafelyComponentControl formSafelyCloseControl)
        {
            InitializeComponent();

            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            this.formSafelyCloseControl = formSafelyCloseControl;

            Icon applicationIcon = new ApplicationDirectoryControl().Directories.Images.GetDirectoryFileAt(DirectoryIcons.ApplicationIcon).Icon;

            this.NotifyIcon.ContextMenuStrip = ContextMenuStrip;
            this.NotifyIcon.ContextMenuStrip.RenderMode = ToolStripRenderMode.System;
            this.NotifyIcon.Text = TrayItemTextContainer.ApplicationName;
            this.NotifyIcon.Icon = applicationIcon;
            base.Icon = applicationIcon;

            tray = new BinanceTrackerTray(this.NotifyIcon, this.formSafelyCloseControl, new FormNotificationsControl(new FormStableNotificationsControl(this.NotifyIcon)));

            this.NotifyIcon.MouseClick += (s, e) => tray.EventsContainerControl.MouseClick.TriggerEvent(e);
            this.NotifyIcon.DoubleClick += (s, e) => tray.EventsContainerControl.DoubleClickListener.TriggerEvent(e);
        }
    }
}
