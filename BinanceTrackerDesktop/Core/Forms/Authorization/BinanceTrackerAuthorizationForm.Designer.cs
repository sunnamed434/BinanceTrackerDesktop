
namespace BinanceTrackerDesktop.Core.Forms.Authorization
{
    partial class BinanceTrackerAuthorizationForm
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
            this.UserKeyTextBox = new System.Windows.Forms.TextBox();
            this.UserSecretTextBox = new System.Windows.Forms.TextBox();
            this.AuthorizeButton = new System.Windows.Forms.Button();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.SecretLabel = new System.Windows.Forms.Label();
            this.UserCurrenyTextBox = new System.Windows.Forms.TextBox();
            this.CurrencyLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserKeyTextBox
            // 
            this.UserKeyTextBox.Location = new System.Drawing.Point(12, 29);
            this.UserKeyTextBox.Name = "UserKeyTextBox";
            this.UserKeyTextBox.PasswordChar = '●';
            this.UserKeyTextBox.Size = new System.Drawing.Size(325, 20);
            this.UserKeyTextBox.TabIndex = 0;
            // 
            // UserSecretTextBox
            // 
            this.UserSecretTextBox.Location = new System.Drawing.Point(12, 70);
            this.UserSecretTextBox.Name = "UserSecretTextBox";
            this.UserSecretTextBox.PasswordChar = '●';
            this.UserSecretTextBox.Size = new System.Drawing.Size(325, 20);
            this.UserSecretTextBox.TabIndex = 1;
            // 
            // AuthorizeButton
            // 
            this.AuthorizeButton.Location = new System.Drawing.Point(95, 140);
            this.AuthorizeButton.Name = "AuthorizeButton";
            this.AuthorizeButton.Size = new System.Drawing.Size(158, 23);
            this.AuthorizeButton.TabIndex = 2;
            this.AuthorizeButton.Text = "Authorize";
            this.AuthorizeButton.UseVisualStyleBackColor = true;
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Location = new System.Drawing.Point(13, 13);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(25, 13);
            this.KeyLabel.TabIndex = 3;
            this.KeyLabel.Text = "Key";
            // 
            // SecretLabel
            // 
            this.SecretLabel.AutoSize = true;
            this.SecretLabel.Location = new System.Drawing.Point(13, 54);
            this.SecretLabel.Name = "SecretLabel";
            this.SecretLabel.Size = new System.Drawing.Size(38, 13);
            this.SecretLabel.TabIndex = 4;
            this.SecretLabel.Text = "Secret";
            // 
            // UserCurrenyTextBox
            // 
            this.UserCurrenyTextBox.Location = new System.Drawing.Point(12, 114);
            this.UserCurrenyTextBox.Name = "UserCurrenyTextBox";
            this.UserCurrenyTextBox.Size = new System.Drawing.Size(325, 20);
            this.UserCurrenyTextBox.TabIndex = 5;
            // 
            // CurrencyLable
            // 
            this.CurrencyLable.AutoSize = true;
            this.CurrencyLable.Location = new System.Drawing.Point(15, 98);
            this.CurrencyLable.Name = "CurrencyLable";
            this.CurrencyLable.Size = new System.Drawing.Size(68, 13);
            this.CurrencyLable.TabIndex = 6;
            this.CurrencyLable.Text = "Crypto Name";
            // 
            // BinanceTrackerAuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 170);
            this.Controls.Add(this.CurrencyLable);
            this.Controls.Add(this.UserCurrenyTextBox);
            this.Controls.Add(this.SecretLabel);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.AuthorizeButton);
            this.Controls.Add(this.UserSecretTextBox);
            this.Controls.Add(this.UserKeyTextBox);
            this.Name = "BinanceTrackerAuthorizationForm";
            this.Text = "Binance Tracker Authorization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserKeyTextBox;
        private System.Windows.Forms.TextBox UserSecretTextBox;
        private System.Windows.Forms.Button AuthorizeButton;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.Label SecretLabel;
        private System.Windows.Forms.TextBox UserCurrenyTextBox;
        private System.Windows.Forms.Label CurrencyLable;
    }
}