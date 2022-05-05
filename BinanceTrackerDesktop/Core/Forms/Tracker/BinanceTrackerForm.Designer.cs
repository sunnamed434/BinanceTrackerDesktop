using BinanceTrackerDesktop.Core.Themes.Attributes;

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
            this.UserTotalBalanceText.Location = new System.Drawing.Point(3, 63);
            this.UserTotalBalanceText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserTotalBalanceText.Name = "UserTotalBalanceText";
            this.UserTotalBalanceText.Size = new System.Drawing.Size(469, 60);
            this.UserTotalBalanceText.TabIndex = 0;
            this.UserTotalBalanceText.Text = "€0.00";
            this.UserTotalBalanceText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TotalBalanceTooltipText
            // 
            this.TotalBalanceTooltipText.AutoSize = true;
            this.TotalBalanceTooltipText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TotalBalanceTooltipText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TotalBalanceTooltipText.Location = new System.Drawing.Point(9, 43);
            this.TotalBalanceTooltipText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TotalBalanceTooltipText.Name = "TotalBalanceTooltipText";
            this.TotalBalanceTooltipText.Size = new System.Drawing.Size(108, 17);
            this.TotalBalanceTooltipText.TabIndex = 1;
            this.TotalBalanceTooltipText.Text = "Total Balance";
            // 
            // RefreshTotalBalanceButton
            // 
            this.RefreshTotalBalanceButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RefreshTotalBalanceButton.Location = new System.Drawing.Point(187, 454);
            this.RefreshTotalBalanceButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RefreshTotalBalanceButton.Name = "RefreshTotalBalanceButton";
            this.RefreshTotalBalanceButton.Size = new System.Drawing.Size(152, 35);
            this.RefreshTotalBalanceButton.TabIndex = 2;
            this.RefreshTotalBalanceButton.Text = "Refresh Balance";
            this.RefreshTotalBalanceButton.UseVisualStyleBackColor = true;
            // 
            // UserTotalBalanceLosesText
            // 
            this.UserTotalBalanceLosesText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UserTotalBalanceLosesText.ForeColor = System.Drawing.Color.Gray;
            this.UserTotalBalanceLosesText.Location = new System.Drawing.Point(360, 34);
            this.UserTotalBalanceLosesText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserTotalBalanceLosesText.Name = "UserTotalBalanceLosesText";
            this.UserTotalBalanceLosesText.Size = new System.Drawing.Size(165, 29);
            this.UserTotalBalanceLosesText.TabIndex = 3;
            this.UserTotalBalanceLosesText.Text = "€0.00";
            this.UserTotalBalanceLosesText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MenuStrip
            // 
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.MenuStrip.Size = new System.Drawing.Size(525, 24);
            this.MenuStrip.TabIndex = 4;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // BinanceTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 508);
            this.Controls.Add(this.UserTotalBalanceLosesText);
            this.Controls.Add(this.RefreshTotalBalanceButton);
            this.Controls.Add(this.TotalBalanceTooltipText);
            this.Controls.Add(this.UserTotalBalanceText);
            this.Controls.Add(this.MenuStrip);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainMenuStrip = this.MenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BinanceTrackerForm";
            this.Text = "Binance Tracker Desktop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        [ThemeComponentAttribute(nameof(UserTotalBalanceText))]
        private Label UserTotalBalanceText;

        [ThemeComponentAttribute(nameof(TotalBalanceTooltipText))]
        private Label TotalBalanceTooltipText;

        [ThemeComponentAttribute(nameof(RefreshTotalBalanceButton))]
        private Button RefreshTotalBalanceButton;

        [ThemeComponentAttribute(nameof(UserTotalBalanceLosesText))]
        private Label UserTotalBalanceLosesText;

        private MenuStrip MenuStrip;
    }
}

