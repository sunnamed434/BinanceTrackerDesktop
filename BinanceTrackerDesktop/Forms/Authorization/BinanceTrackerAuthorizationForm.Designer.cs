
namespace BinanceTrackerDesktop.Forms.Authorization
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
            this.SuspendLayout();
            // 
            // UserKeyTextBox
            // 
            this.UserKeyTextBox.Location = new System.Drawing.Point(12, 25);
            this.UserKeyTextBox.Name = "UserKeyTextBox";
            this.UserKeyTextBox.PasswordChar = '●';
            this.UserKeyTextBox.Size = new System.Drawing.Size(325, 20);
            this.UserKeyTextBox.TabIndex = 0;
            // 
            // UserSecretTextBox
            // 
            this.UserSecretTextBox.Location = new System.Drawing.Point(12, 51);
            this.UserSecretTextBox.Name = "UserSecretTextBox";
            this.UserSecretTextBox.PasswordChar = '●';
            this.UserSecretTextBox.Size = new System.Drawing.Size(325, 20);
            this.UserSecretTextBox.TabIndex = 1;
            // 
            // AuthorizeButton
            // 
            this.AuthorizeButton.Location = new System.Drawing.Point(95, 77);
            this.AuthorizeButton.Name = "AuthorizeButton";
            this.AuthorizeButton.Size = new System.Drawing.Size(158, 23);
            this.AuthorizeButton.TabIndex = 2;
            this.AuthorizeButton.Text = "Authorize";
            this.AuthorizeButton.UseVisualStyleBackColor = true;
            // 
            // BinanceTrackerAuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 124);
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
    }
}