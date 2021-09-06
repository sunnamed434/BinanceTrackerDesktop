using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray.API;
using BinanceTrackerDesktop.Forms.SystemTray.Tray;
using BinanceTrackerDesktop.Forms.SystemTray.Tray.Data;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray
{
    public partial class BinanceTrackerSystemTrayForm : Form, ISystemTrayFormControl
    {
        private readonly IFormEventListener[] formEventListeners;

        private readonly IFormSafelyCloseControl formSafelyCloseControl;



        NotifyIcon ISystemTrayFormControl.NotifyIcon => this.NotifyIcon;

        

        public BinanceTrackerSystemTrayForm(IFormSafelyCloseControl formSafelyCloseControl)
        {
            InitializeComponent();

            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            this.NotifyIcon.ContextMenuStrip = Tray;
            this.NotifyIcon.Text = TrayDataContainer.ApplicationName;
            this.NotifyIcon.DoubleClick += (s, e) => formEventListeners[0].TriggerEvent(s, e);

            new BinanceTrackerTray(this.formSafelyCloseControl = formSafelyCloseControl, this, this, new BinanceTrackerNotificationsControl(new StableNotificationsControl(this.NotifyIcon)), 
            formEventListeners = new IFormEventListener[]
            {
                new FormEventListener(),
                new FormEventListener(),
                new FormEventListener(),
                new FormEventListener(),
            });

            initializeContextMenuStripAndReadUserDataAsync();

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
        }

        

        void ISystemTrayFormControl.Close()
        {
            using (NotifyIcon)
                this.NotifyIcon.Visible = false;
        }

        void ISystemTrayFormControl.ChangeMenuItemTitle(int index, string to)
        {
            if (this.Tray.Items[index] == null)
                throw new IndexOutOfRangeException(nameof(index));

            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException(nameof(to));

            this.Tray.Items[index].Text = to;
        }



        private async void initializeContextMenuStripAndReadUserDataAsync()
        {
            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;

            this.Tray.Items.Add(new ToolStripMenuItem(TrayDataContainer.OpenApplication, default, (s, e) => formEventListeners[1].TriggerEvent(s, e)));
            this.Tray.Items.Add(new ToolStripMenuItem(binanceUserData.NotificationsEnabled == true ? TrayDataContainer.DisableNotifications : TrayDataContainer.EnableNotifications, default, (s, e) => formEventListeners[2].TriggerEvent(s, e)));
            this.Tray.Items.Add(new ToolStripMenuItem(TrayDataContainer.QuitApplication, default, (s, e) => formEventListeners[3].TriggerEvent(s, e)));
        }



        private async Task onCloseCallbackAsync()
        {
            ((ISystemTrayFormControl)this).Close();

            await Task.CompletedTask;
        }
    }
}
