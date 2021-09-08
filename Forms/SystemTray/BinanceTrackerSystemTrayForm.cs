﻿using BinanceTrackerDesktop.Core.UserData.API;
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
        private IFormTrayControl formTrayControl;

        private IFormSafelyComponentControl formSafelyCloseControl;

        private IFormSystemTrayControl systemTrayControl;



        public BinanceTrackerSystemTrayForm(IFormSafelyComponentControl formSafelyCloseControl)
        {
            InitializeComponent();

            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));
            
            this.NotifyIcon.ContextMenuStrip = this.Tray;
            this.NotifyIcon.Text = TrayDataContainer.ApplicationName;
            this.NotifyIcon.DoubleClick += (s, e) => formTrayControl.DoubleClickListener.ClickEvent.TriggerEvent(s, e);

            initializeAsync(formSafelyCloseControl);

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
        }

        

        private async void initializeAsync(IFormSafelyComponentControl formSafelyCloseControl)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            systemTrayControl = new FormSystemTrayControl(this.NotifyIcon);

            BinanceUserData binanceUserData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            formTrayControl = new FormTrayControl(systemTrayControl, new List<IFormTrayItemControl>
            {
                new FormTrayItemControl(TrayDataContainer.OpenApplication),
                new FormTrayItemControl(binanceUserData.NotificationsEnabled == true ? TrayDataContainer.DisableNotifications : TrayDataContainer.EnableNotifications),
                new FormTrayItemControl(TrayDataContainer.QuitApplication),
            });

            new BinanceTrackerTray(this.formSafelyCloseControl = formSafelyCloseControl, systemTrayControl, this, new BinanceTrackerNotificationsControl(new StableNotificationsControl(this.NotifyIcon)), formTrayControl);
        }



        private async Task onCloseCallbackAsync()
        {
            await formSafelyCloseControl.CallListenersAsync();

            await Task.CompletedTask;
        }
    }
}
