using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.SystemTray.API;
using BinanceTrackerDesktop.Forms.SystemTray.Tray;
using BinanceTrackerDesktop.Forms.SystemTray.Tray.Data;
using BinanceTrackerDesktop.Forms.Tracker.Notifications;
using BinanceTrackerDesktop.Forms.Tracker.Notifications.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray
{
    public partial class BinanceTrackerSystemTrayForm : Form
    {
        private readonly IFormSafelyComponentControl formSafelyCloseControl;

        private IFormSystemTrayControl systemTrayControl;

        private IFormTrayControl formTrayControl;



        public BinanceTrackerSystemTrayForm(IFormSafelyComponentControl formSafelyCloseControl)
        {
            InitializeComponent();

            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            initializeAsync(this.formSafelyCloseControl = formSafelyCloseControl);

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
        }

        

        private async void initializeAsync(IFormSafelyComponentControl formSafelyCloseControl)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            this.NotifyIcon.ContextMenuStrip = this.ContextMenuStrip;
            this.NotifyIcon.ContextMenuStrip.RenderMode = ToolStripRenderMode.System;
            this.NotifyIcon.Text = TrayItemsDataContainer.ApplicationName;
            systemTrayControl = new FormSystemTrayControl(this.NotifyIcon);

            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            formTrayControl = new FormTrayControl(systemTrayControl, new List<IFormTrayItemControl>
            {
                new FormTrayItemControl(TrayItemsDataContainer.OpenApplication, TrayItemsUniquesDataContainer.OpenApplicationUniqueIndex),
                new FormTrayItemControl(binanceUserData.NotificationsEnabled == true ? TrayItemsDataContainer.DisableNotifications : TrayItemsDataContainer.EnableNotifications, TrayItemsUniquesDataContainer.NotificationsUniqueIndex),
                new FormTrayItemControl(TrayItemsDataContainer.QuitApplication, TrayItemsUniquesDataContainer.QuitApplicationUniqueIndex),
            });;

            this.NotifyIcon.MouseClick += (s, e) => formTrayControl.EventsContainerControl.MouseClick.TriggerEvent(e);
            this.NotifyIcon.DoubleClick += (s, e) => formTrayControl.EventsContainerControl.DoubleClickListener.TriggerEvent(e);

            new BinanceTrackerTray(this.formSafelyCloseControl, systemTrayControl, this, new BinanceTrackerNotificationsControl(new StableNotificationsControl(this.NotifyIcon)), formTrayControl);
        }

        

        private async Task onCloseCallbackAsync()
        {
            formTrayControl.SystemTrayFormControl.Close();

            await Task.CompletedTask;
        }
    }
}
