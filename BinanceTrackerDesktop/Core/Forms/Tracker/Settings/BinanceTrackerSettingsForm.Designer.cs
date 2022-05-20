using BinanceTrackerDesktop.Core.Themes.Models;
using BinanceTrackerDesktop.Core.Themes.Themable;
using System.ComponentModel;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.Settings
{
    public sealed partial class BinanceTrackerSettingsForm : IThemable
    {
        private IContainer components;



        public void ApplyTheme()
        {
            ThemeColors loadedThemeData = themable.ThemesProvider.LoadThemeData();

            base.BackColor = loadedThemeData.Form;
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



        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.NewCurrenyTextBox = new System.Windows.Forms.TextBox();
            this.NewCurrenyLabel = new System.Windows.Forms.Label();
            this.ChangeCurrencyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NewCurrenyTextBox
            // 
            this.NewCurrenyTextBox.Location = new System.Drawing.Point(9, 25);
            this.NewCurrenyTextBox.Name = "NewCurrenyTextBox";
            this.NewCurrenyTextBox.Size = new System.Drawing.Size(122, 20);
            this.NewCurrenyTextBox.TabIndex = 0;
            // 
            // NewCurrenyLabel
            // 
            this.NewCurrenyLabel.AutoSize = true;
            this.NewCurrenyLabel.Location = new System.Drawing.Point(12, 9);
            this.NewCurrenyLabel.Name = "NewCurrenyLabel";
            this.NewCurrenyLabel.Size = new System.Drawing.Size(73, 13);
            this.NewCurrenyLabel.TabIndex = 1;
            this.NewCurrenyLabel.Text = "New currency";
            // 
            // ChangeCurrencyButton
            // 
            this.ChangeCurrencyButton.Location = new System.Drawing.Point(9, 51);
            this.ChangeCurrencyButton.Name = "ChangeCurrencyButton";
            this.ChangeCurrencyButton.Size = new System.Drawing.Size(122, 23);
            this.ChangeCurrencyButton.TabIndex = 2;
            this.ChangeCurrencyButton.Text = "Change";
            this.ChangeCurrencyButton.UseVisualStyleBackColor = true;
            // 
            // BinanceTrackerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 107);
            this.Controls.Add(this.ChangeCurrencyButton);
            this.Controls.Add(this.NewCurrenyLabel);
            this.Controls.Add(this.NewCurrenyTextBox);
            this.Name = "BinanceTrackerSettingsForm";
            this.Text = "BinanceTrackerSettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion



        private TextBox NewCurrenyTextBox;

        private Label NewCurrenyLabel;

        private Button ChangeCurrencyButton;
    }
}