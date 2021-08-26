using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray
{
    public partial class BinanceTrackerSystemTrayForm : Form
    {
        public BinanceTrackerSystemTrayForm()
        {
            InitializeComponent();

            this.OpenBinanceTrackerToolStripMenuItem.Click += onOpenBinanceTrackerToolStripMenuItemClick;
            this.DisableNotificationsToolStripMenuItem.Click += onDisableNotificationsToolStripMenuItemClick;
            this.QuitBinanceTrackerToolStripMenuItem.Click += onQuitBinanceTrackerToolStripMenuItemClick;

            base.FormClosing += onFormClosing;
        }



        private void onOpenBinanceTrackerToolStripMenuItemClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void onDisableNotificationsToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        private void onQuitBinanceTrackerToolStripMenuItemClick(object sender, EventArgs e)
        {
            base.Close();
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            this.OpenBinanceTrackerToolStripMenuItem.Click -= onOpenBinanceTrackerToolStripMenuItemClick;
            this.DisableNotificationsToolStripMenuItem.Click -= onDisableNotificationsToolStripMenuItemClick;
            this.QuitBinanceTrackerToolStripMenuItem.Click -= onQuitBinanceTrackerToolStripMenuItemClick;
        }

        private void BinanceTrackerSystemTrayForm_Load(object sender, EventArgs e)
        {

        }
    }
}
