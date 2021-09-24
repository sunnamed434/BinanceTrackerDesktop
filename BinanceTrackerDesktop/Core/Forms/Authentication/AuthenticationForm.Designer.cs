namespace BinanceTrackerDesktop.Core.Forms.Authentication
{
    partial class AuthenticationForm
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
            this.QRCodePictureBox = new System.Windows.Forms.PictureBox();
            this.SecretKeyTextBox = new System.Windows.Forms.TextBox();
            this.AccountTitleTextBox = new System.Windows.Forms.TextBox();
            this.AccountTitleLabel = new System.Windows.Forms.Label();
            this.SecretKeyLabel = new System.Windows.Forms.Label();
            this.GenerateQRCodeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.QRCodePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // QRCodePictureBox
            // 
            this.QRCodePictureBox.BackColor = System.Drawing.SystemColors.Window;
            this.QRCodePictureBox.Location = new System.Drawing.Point(27, 166);
            this.QRCodePictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.QRCodePictureBox.Name = "QRCodePictureBox";
            this.QRCodePictureBox.Size = new System.Drawing.Size(291, 272);
            this.QRCodePictureBox.TabIndex = 0;
            this.QRCodePictureBox.TabStop = false;
            // 
            // SecretKeyTextBox
            // 
            this.SecretKeyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecretKeyTextBox.Location = new System.Drawing.Point(115, 47);
            this.SecretKeyTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.SecretKeyTextBox.Name = "SecretKeyTextBox";
            this.SecretKeyTextBox.Size = new System.Drawing.Size(203, 22);
            this.SecretKeyTextBox.TabIndex = 1;
            // 
            // AccountTitleTextBox
            // 
            this.AccountTitleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountTitleTextBox.Location = new System.Drawing.Point(115, 13);
            this.AccountTitleTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.AccountTitleTextBox.Name = "AccountTitleTextBox";
            this.AccountTitleTextBox.Size = new System.Drawing.Size(203, 22);
            this.AccountTitleTextBox.TabIndex = 2;
            // 
            // AccountTitleLabel
            // 
            this.AccountTitleLabel.AutoSize = true;
            this.AccountTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountTitleLabel.Location = new System.Drawing.Point(17, 16);
            this.AccountTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AccountTitleLabel.Name = "AccountTitleLabel";
            this.AccountTitleLabel.Size = new System.Drawing.Size(90, 16);
            this.AccountTitleLabel.TabIndex = 3;
            this.AccountTitleLabel.Text = "Account Titile:";
            // 
            // SecretKeyLabel
            // 
            this.SecretKeyLabel.AutoSize = true;
            this.SecretKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecretKeyLabel.Location = new System.Drawing.Point(17, 53);
            this.SecretKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SecretKeyLabel.Name = "SecretKeyLabel";
            this.SecretKeyLabel.Size = new System.Drawing.Size(75, 16);
            this.SecretKeyLabel.TabIndex = 4;
            this.SecretKeyLabel.Text = "Secret Key:";
            // 
            // GenerateQRCodeButton
            // 
            this.GenerateQRCodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateQRCodeButton.Location = new System.Drawing.Point(27, 446);
            this.GenerateQRCodeButton.Margin = new System.Windows.Forms.Padding(4);
            this.GenerateQRCodeButton.Name = "GenerateQRCodeButton";
            this.GenerateQRCodeButton.Size = new System.Drawing.Size(291, 38);
            this.GenerateQRCodeButton.TabIndex = 5;
            this.GenerateQRCodeButton.Text = "Generate QR Code";
            this.GenerateQRCodeButton.UseVisualStyleBackColor = true;
            this.GenerateQRCodeButton.Click += new System.EventHandler(this.onGenerateQRCodeButtonClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 130);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "QR Code:";
            // 
            // AuthenticationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GenerateQRCodeButton);
            this.Controls.Add(this.SecretKeyLabel);
            this.Controls.Add(this.AccountTitleLabel);
            this.Controls.Add(this.AccountTitleTextBox);
            this.Controls.Add(this.SecretKeyTextBox);
            this.Controls.Add(this.QRCodePictureBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AuthenticationForm";
            this.Text = "AuthenticationForm";
            ((System.ComponentModel.ISupportInitialize)(this.QRCodePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox QRCodePictureBox;
        private System.Windows.Forms.TextBox SecretKeyTextBox;
        private System.Windows.Forms.TextBox AccountTitleTextBox;
        private System.Windows.Forms.Label AccountTitleLabel;
        private System.Windows.Forms.Label SecretKeyLabel;
        private System.Windows.Forms.Button GenerateQRCodeButton;
        private System.Windows.Forms.Label label1;
    }
}