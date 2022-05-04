
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
            this.AddAuthenticatorButton = new System.Windows.Forms.Button();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserKeyTextBox
            // 
            this.UserKeyTextBox.Location = new System.Drawing.Point(21, 79);
            this.UserKeyTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UserKeyTextBox.Name = "UserKeyTextBox";
            this.UserKeyTextBox.PasswordChar = '●';
            this.UserKeyTextBox.Size = new System.Drawing.Size(432, 27);
            this.UserKeyTextBox.TabIndex = 0;
            // 
            // UserSecretTextBox
            // 
            this.UserSecretTextBox.Location = new System.Drawing.Point(21, 146);
            this.UserSecretTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UserSecretTextBox.Name = "UserSecretTextBox";
            this.UserSecretTextBox.PasswordChar = '●';
            this.UserSecretTextBox.Size = new System.Drawing.Size(432, 27);
            this.UserSecretTextBox.TabIndex = 1;
            // 
            // AuthorizeButton
            // 
            this.AuthorizeButton.Location = new System.Drawing.Point(143, 250);
            this.AuthorizeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AuthorizeButton.Name = "AuthorizeButton";
            this.AuthorizeButton.Size = new System.Drawing.Size(211, 35);
            this.AuthorizeButton.TabIndex = 2;
            this.AuthorizeButton.Text = "Authorize";
            this.AuthorizeButton.UseVisualStyleBackColor = true;
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Location = new System.Drawing.Point(221, 49);
            this.KeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(33, 20);
            this.KeyLabel.TabIndex = 3;
            this.KeyLabel.Text = "Key";
            // 
            // SecretLabel
            // 
            this.SecretLabel.AutoSize = true;
            this.SecretLabel.Location = new System.Drawing.Point(212, 116);
            this.SecretLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SecretLabel.Name = "SecretLabel";
            this.SecretLabel.Size = new System.Drawing.Size(50, 20);
            this.SecretLabel.TabIndex = 4;
            this.SecretLabel.Text = "Secret";
            // 
            // UserCurrenyTextBox
            // 
            this.UserCurrenyTextBox.Location = new System.Drawing.Point(21, 213);
            this.UserCurrenyTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UserCurrenyTextBox.Name = "UserCurrenyTextBox";
            this.UserCurrenyTextBox.Size = new System.Drawing.Size(432, 27);
            this.UserCurrenyTextBox.TabIndex = 5;
            // 
            // CurrencyLable
            // 
            this.CurrencyLable.AutoSize = true;
            this.CurrencyLable.Location = new System.Drawing.Point(189, 183);
            this.CurrencyLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CurrencyLable.Name = "CurrencyLable";
            this.CurrencyLable.Size = new System.Drawing.Size(97, 20);
            this.CurrencyLable.TabIndex = 6;
            this.CurrencyLable.Text = "Crypto Name";
            // 
            // AddAuthenticatorButton
            // 
            this.AddAuthenticatorButton.Location = new System.Drawing.Point(143, 295);
            this.AddAuthenticatorButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddAuthenticatorButton.Name = "AddAuthenticatorButton";
            this.AddAuthenticatorButton.Size = new System.Drawing.Size(211, 35);
            this.AddAuthenticatorButton.TabIndex = 7;
            this.AddAuthenticatorButton.Text = "Add authenticator";
            this.AddAuthenticatorButton.UseVisualStyleBackColor = true;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HeaderLabel.Location = new System.Drawing.Point(174, 14);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(126, 25);
            this.HeaderLabel.TabIndex = 8;
            this.HeaderLabel.Text = "Authorization";
            // 
            // BinanceTrackerAuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 344);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.AddAuthenticatorButton);
            this.Controls.Add(this.CurrencyLable);
            this.Controls.Add(this.UserCurrenyTextBox);
            this.Controls.Add(this.SecretLabel);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.AuthorizeButton);
            this.Controls.Add(this.UserSecretTextBox);
            this.Controls.Add(this.UserKeyTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private Button AddAuthenticatorButton;
        private Label HeaderLabel;
    }
}