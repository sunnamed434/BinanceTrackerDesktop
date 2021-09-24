namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    partial class AuthenticatorForm
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
            this.CheckAuthenticationPINButton = new System.Windows.Forms.Button();
            this.UserPINTextBox = new System.Windows.Forms.TextBox();
            this.InfoLabel = new System.Windows.Forms.Label();
            this.UserSecretTextBot = new System.Windows.Forms.TextBox();
            this.UserPINInfoLabel = new System.Windows.Forms.Label();
            this.SecretInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CheckAuthenticationPINButton
            // 
            this.CheckAuthenticationPINButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckAuthenticationPINButton.Location = new System.Drawing.Point(94, 171);
            this.CheckAuthenticationPINButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheckAuthenticationPINButton.Name = "CheckAuthenticationPINButton";
            this.CheckAuthenticationPINButton.Size = new System.Drawing.Size(204, 27);
            this.CheckAuthenticationPINButton.TabIndex = 8;
            this.CheckAuthenticationPINButton.Text = "Check Authentication PIN";
            this.CheckAuthenticationPINButton.UseVisualStyleBackColor = true;
            // 
            // UserPINTextBox
            // 
            this.UserPINTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserPINTextBox.Location = new System.Drawing.Point(13, 121);
            this.UserPINTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UserPINTextBox.Name = "UserPINTextBox";
            this.UserPINTextBox.Size = new System.Drawing.Size(367, 22);
            this.UserPINTextBox.TabIndex = 6;
            // 
            // InfoLabel
            // 
            this.InfoLabel.AutoSize = true;
            this.InfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.InfoLabel.Location = new System.Drawing.Point(115, 9);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(163, 20);
            this.InfoLabel.TabIndex = 10;
            this.InfoLabel.Text = "Authenticate Yourself";
            // 
            // UserSecretTextBot
            // 
            this.UserSecretTextBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserSecretTextBot.Location = new System.Drawing.Point(13, 67);
            this.UserSecretTextBot.Margin = new System.Windows.Forms.Padding(4);
            this.UserSecretTextBot.Name = "UserSecretTextBot";
            this.UserSecretTextBot.Size = new System.Drawing.Size(367, 22);
            this.UserSecretTextBot.TabIndex = 11;
            // 
            // UserPINInfoLabel
            // 
            this.UserPINInfoLabel.AutoSize = true;
            this.UserPINInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.UserPINInfoLabel.Location = new System.Drawing.Point(180, 101);
            this.UserPINInfoLabel.Name = "UserPINInfoLabel";
            this.UserPINInfoLabel.Size = new System.Drawing.Size(32, 16);
            this.UserPINInfoLabel.TabIndex = 12;
            this.UserPINInfoLabel.Text = "PIN:";
            // 
            // SecretInfoLabel
            // 
            this.SecretInfoLabel.AutoSize = true;
            this.SecretInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.SecretInfoLabel.Location = new System.Drawing.Point(172, 47);
            this.SecretInfoLabel.Name = "SecretInfoLabel";
            this.SecretInfoLabel.Size = new System.Drawing.Size(49, 16);
            this.SecretInfoLabel.TabIndex = 13;
            this.SecretInfoLabel.Text = "Secret:";
            // 
            // AuthenticatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 204);
            this.Controls.Add(this.SecretInfoLabel);
            this.Controls.Add(this.UserPINInfoLabel);
            this.Controls.Add(this.UserSecretTextBot);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.CheckAuthenticationPINButton);
            this.Controls.Add(this.UserPINTextBox);
            this.Name = "AuthenticatorForm";
            this.Text = "AuthenticatorForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CheckAuthenticationPINButton;
        private System.Windows.Forms.TextBox UserPINTextBox;
        private System.Windows.Forms.Label InfoLabel;
        private System.Windows.Forms.TextBox UserSecretTextBot;
        private System.Windows.Forms.Label UserPINInfoLabel;
        private System.Windows.Forms.Label SecretInfoLabel;
    }
}