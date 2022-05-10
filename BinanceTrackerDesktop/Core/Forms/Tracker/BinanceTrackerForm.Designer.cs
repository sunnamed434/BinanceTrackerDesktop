using BinanceTrackerDesktop.Core.Themes.Models.Data;
using BinanceTrackerDesktop.Core.Themes.Provider;
using BinanceTrackerDesktop.Core.Themes.Themable;
using System.ComponentModel;

namespace BinanceTrackerDesktop.Tracker.Forms
{
    public sealed partial class BinanceTrackerForm : IThemable
    {
        public IThemesProvider ThemesProvider { get; }

        private IThemable themable;

        private IContainer components;



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }



        #region Windows Form Designer
        private void InitializeComponent()
        {
            this.UserTotalBalanceText = new System.Windows.Forms.Label();
            this.TotalBalanceTooltipText = new System.Windows.Forms.Label();
            this.RefreshTotalBalanceButton = new System.Windows.Forms.Button();
            this.UserTotalBalanceLosesText = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // UserTotalBalanceText
            // 
            this.UserTotalBalanceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserTotalBalanceText.Location = new System.Drawing.Point(8, 54);
            this.UserTotalBalanceText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserTotalBalanceText.Name = "UserTotalBalanceText";
            this.UserTotalBalanceText.Size = new System.Drawing.Size(410, 45);
            this.UserTotalBalanceText.TabIndex = 0;
            this.UserTotalBalanceText.Text = "€0.00";
            this.UserTotalBalanceText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TotalBalanceTooltipText
            // 
            this.TotalBalanceTooltipText.AutoSize = true;
            this.TotalBalanceTooltipText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TotalBalanceTooltipText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TotalBalanceTooltipText.Location = new System.Drawing.Point(8, 32);
            this.TotalBalanceTooltipText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TotalBalanceTooltipText.Name = "TotalBalanceTooltipText";
            this.TotalBalanceTooltipText.Size = new System.Drawing.Size(106, 20);
            this.TotalBalanceTooltipText.TabIndex = 1;
            this.TotalBalanceTooltipText.Text = "Total Balance";
            // 
            // RefreshTotalBalanceButton
            // 
            this.RefreshTotalBalanceButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RefreshTotalBalanceButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.RefreshTotalBalanceButton.FlatAppearance.BorderSize = 0;
            this.RefreshTotalBalanceButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RefreshTotalBalanceButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RefreshTotalBalanceButton.ForeColor = System.Drawing.SystemColors.Control;
            this.RefreshTotalBalanceButton.Location = new System.Drawing.Point(164, 340);
            this.RefreshTotalBalanceButton.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshTotalBalanceButton.Name = "RefreshTotalBalanceButton";
            this.RefreshTotalBalanceButton.Size = new System.Drawing.Size(133, 26);
            this.RefreshTotalBalanceButton.TabIndex = 2;
            this.RefreshTotalBalanceButton.Text = "Refresh Balance";
            this.RefreshTotalBalanceButton.UseVisualStyleBackColor = false;
            // 
            // UserTotalBalanceLosesText
            // 
            this.UserTotalBalanceLosesText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserTotalBalanceLosesText.ForeColor = System.Drawing.Color.Gray;
            this.UserTotalBalanceLosesText.Location = new System.Drawing.Point(315, 26);
            this.UserTotalBalanceLosesText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserTotalBalanceLosesText.Name = "UserTotalBalanceLosesText";
            this.UserTotalBalanceLosesText.Size = new System.Drawing.Size(144, 22);
            this.UserTotalBalanceLosesText.TabIndex = 3;
            this.UserTotalBalanceLosesText.Text = "€0.00";
            this.UserTotalBalanceLosesText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MenuStrip.Size = new System.Drawing.Size(459, 24);
            this.MenuStrip.TabIndex = 4;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // BinanceTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(459, 381);
            this.Controls.Add(this.UserTotalBalanceLosesText);
            this.Controls.Add(this.RefreshTotalBalanceButton);
            this.Controls.Add(this.TotalBalanceTooltipText);
            this.Controls.Add(this.UserTotalBalanceText);
            this.Controls.Add(this.MenuStrip);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BinanceTrackerForm";
            this.Text = "Binance Tracker Desktop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion



        void IThemable.ApplyTheme()
        {
            LoadedThemeData loadedThemeData = themable.ThemesProvider.LoadThemeData();
            BackColor = loadedThemeData.Form;
            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = loadedThemeData.Button;
                    button.ForeColor = loadedThemeData.ButtonText;
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                }

                if (control is Label label)
                {
                    label.ForeColor = loadedThemeData.Text;
                }
            }
        }



        private Label UserTotalBalanceText;

        private Label TotalBalanceTooltipText;

        private Button RefreshTotalBalanceButton;

        private Label UserTotalBalanceLosesText;

        private MenuStrip MenuStrip;
    }
}
