
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
            this.SuspendLayout();
            // 
            // UserTotalBalanceText
            // 
            this.UserTotalBalanceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserTotalBalanceText.Location = new System.Drawing.Point(0, 22);
            this.UserTotalBalanceText.Name = "UserTotalBalanceText";
            this.UserTotalBalanceText.Size = new System.Drawing.Size(352, 39);
            this.UserTotalBalanceText.TabIndex = 0;
            this.UserTotalBalanceText.Text = "€0.00";
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
            this.RefreshTotalBalanceButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RefreshTotalBalanceButton.Location = new System.Drawing.Point(140, 295);
            this.RefreshTotalBalanceButton.Name = "RefreshTotalBalanceButton";
            this.RefreshTotalBalanceButton.Size = new System.Drawing.Size(114, 23);
            this.RefreshTotalBalanceButton.TabIndex = 2;
            this.RefreshTotalBalanceButton.Text = "Refresh Balance";
            this.RefreshTotalBalanceButton.UseVisualStyleBackColor = true;
            // 
            // UserTotalBalanceLosesText
            // 
            this.UserTotalBalanceLosesText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserTotalBalanceLosesText.ForeColor = System.Drawing.Color.Gray;
            this.UserTotalBalanceLosesText.Location = new System.Drawing.Point(270, 3);
            this.UserTotalBalanceLosesText.Name = "UserTotalBalanceLosesText";
            this.UserTotalBalanceLosesText.Size = new System.Drawing.Size(124, 19);
            this.UserTotalBalanceLosesText.TabIndex = 3;
            this.UserTotalBalanceLosesText.Text = "€0.00";
            this.UserTotalBalanceLosesText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BinanceTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 330);
            this.Controls.Add(this.UserTotalBalanceLosesText);
            this.Controls.Add(this.RefreshTotalBalanceButton);
            this.Controls.Add(this.TotalBalanceTooltipText);
            this.Controls.Add(this.UserTotalBalanceText);
            this.Name = "BinanceTrackerForm";
            this.Text = "Binance Tracker Desktop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label UserTotalBalanceText;
        private System.Windows.Forms.Label TotalBalanceTooltipText;
        private System.Windows.Forms.Button RefreshTotalBalanceButton;
        private System.Windows.Forms.Label UserTotalBalanceLosesText;
    }
}

