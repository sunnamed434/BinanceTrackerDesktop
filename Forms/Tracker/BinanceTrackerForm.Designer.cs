
namespace BinanceTrackerDesktop.Tracker.Forms
{
    partial class BinanceTrackerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinanceTrackerForm));
            this.UserTotalBalanceText = new System.Windows.Forms.Label();
            this.TotalBalanceTooltipText = new System.Windows.Forms.Label();
            this.RefreshTotalBalanceButton = new System.Windows.Forms.Button();
            this.BinanceTrackerMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenBinanceTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisableNotificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuitBinanceTrackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BinanceTrackerNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.BinanceTrackerMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserTotalBalanceText
            // 
            this.UserTotalBalanceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserTotalBalanceText.Location = new System.Drawing.Point(0, 22);
            this.UserTotalBalanceText.Name = "UserTotalBalanceText";
            this.UserTotalBalanceText.Size = new System.Drawing.Size(352, 39);
            this.UserTotalBalanceText.TabIndex = 0;
            this.UserTotalBalanceText.Text = "€0.0";
            this.UserTotalBalanceText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TotalBalanceTooltipText
            // 
            this.TotalBalanceTooltipText.AutoSize = true;
            this.TotalBalanceTooltipText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalBalanceTooltipText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TotalBalanceTooltipText.Location = new System.Drawing.Point(4, 9);
            this.TotalBalanceTooltipText.Name = "TotalBalanceTooltipText";
            this.TotalBalanceTooltipText.Size = new System.Drawing.Size(86, 13);
            this.TotalBalanceTooltipText.TabIndex = 1;
            this.TotalBalanceTooltipText.Text = "Total Balance";
            // 
            // RefreshTotalBalanceButton
            // 
            this.RefreshTotalBalanceButton.Location = new System.Drawing.Point(140, 295);
            this.RefreshTotalBalanceButton.Name = "RefreshTotalBalanceButton";
            this.RefreshTotalBalanceButton.Size = new System.Drawing.Size(114, 23);
            this.RefreshTotalBalanceButton.TabIndex = 2;
            this.RefreshTotalBalanceButton.Text = "Refresh Balance";
            this.RefreshTotalBalanceButton.UseVisualStyleBackColor = true;
            this.RefreshTotalBalanceButton.Click += new System.EventHandler(this.onRefreshTotalBalanceButtonClick);
            // 
            // BinanceTrackerMenuStrip
            // 
            this.BinanceTrackerMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenBinanceTrackerToolStripMenuItem,
            this.DisableNotificationsToolStripMenuItem,
            this.QuitBinanceTrackerToolStripMenuItem});
            this.BinanceTrackerMenuStrip.Name = "BinanceTrackerMenuStrip";
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
            // BinanceTrackerNotifyIcon
            // 
            this.BinanceTrackerNotifyIcon.ContextMenuStrip = this.BinanceTrackerMenuStrip;
            this.BinanceTrackerNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("BinanceTrackerNotifyIcon.Icon")));
            this.BinanceTrackerNotifyIcon.Text = "Binance Tracker Desktop";
            this.BinanceTrackerNotifyIcon.Visible = true;
            // 
            // BinanceTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 330);
            this.Controls.Add(this.RefreshTotalBalanceButton);
            this.Controls.Add(this.TotalBalanceTooltipText);
            this.Controls.Add(this.UserTotalBalanceText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BinanceTrackerForm";
            this.Text = "Binance Tracker Desktop";
            this.BinanceTrackerMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label UserTotalBalanceText;
        private System.Windows.Forms.Label TotalBalanceTooltipText;
        private System.Windows.Forms.Button RefreshTotalBalanceButton;
        private System.Windows.Forms.ContextMenuStrip BinanceTrackerMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenBinanceTrackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DisableNotificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuitBinanceTrackerToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon BinanceTrackerNotifyIcon;
    }
}

