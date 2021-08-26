
namespace BinanceTrackerDesktop.Forms.SystemTray
{
    partial class BinanceTrackerSystemTrayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinanceTrackerSystemTrayForm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.BinanceTrackerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenBinanceTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisableNotificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitBinanceTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BinanceTrackerMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // BinanceTrackerMenuStrip
            // 
            this.BinanceTrackerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenBinanceTrackerToolStripMenuItem,
            this.DisableNotificationsToolStripMenuItem,
            this.QuitBinanceTrackerToolStripMenuItem});
            this.BinanceTrackerMenuStrip.Name = "contextMenuStrip1";
            this.BinanceTrackerMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.BinanceTrackerMenuStrip.Size = new System.Drawing.Size(189, 70);
            this.BinanceTrackerMenuStrip.Text = "Binance Tracker Desktop";
            // 
            // OpenBinanceTrackerToolStripMenuItem
            // 
            this.OpenBinanceTrackerToolStripMenuItem.Name = "OpenBinanceTrackerToolStripMenuItem";
            this.OpenBinanceTrackerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.OpenBinanceTrackerToolStripMenuItem.Text = "Open Binance Tracker";
            // 
            // DisableNotificationsToolStripMenuItem
            // 
            this.DisableNotificationsToolStripMenuItem.Name = "DisableNotificationsToolStripMenuItem";
            this.DisableNotificationsToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.DisableNotificationsToolStripMenuItem.Text = "Disable notifications";
            // 
            // QuitBinanceTrackerToolStripMenuItem
            // 
            this.QuitBinanceTrackerToolStripMenuItem.Name = "QuitBinanceTrackerToolStripMenuItem";
            this.QuitBinanceTrackerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.QuitBinanceTrackerToolStripMenuItem.Text = "Quit Binance Tracker";
            // 
            // BinanceTrackerSystemTrayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 44);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BinanceTrackerSystemTrayForm";
            this.Text = "BinanceTrackerDesktop";
            this.Load += new System.EventHandler(this.BinanceTrackerSystemTrayForm_Load);
            this.BinanceTrackerMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip BinanceTrackerMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenBinanceTrackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisableNotificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitBinanceTrackerToolStripMenuItem;
    }
}