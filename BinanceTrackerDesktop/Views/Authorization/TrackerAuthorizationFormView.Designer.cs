namespace BinanceTrackerDesktop.Forms.Authorization;

partial class TrackerAuthorizationFormView
{
    private System.ComponentModel.IContainer components = null;

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
            this.CurrencyLabel = new System.Windows.Forms.Label();
            this.AddAuthenticatorButton = new System.Windows.Forms.Button();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.UserLanguageComboBox = new System.Windows.Forms.ComboBox();
            this.LanguageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserKeyTextBox
            // 
            this.UserKeyTextBox.Location = new System.Drawing.Point(19, 59);
            this.UserKeyTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.UserKeyTextBox.Name = "UserKeyTextBox";
            this.UserKeyTextBox.PasswordChar = '●';
            this.UserKeyTextBox.Size = new System.Drawing.Size(378, 23);
            this.UserKeyTextBox.TabIndex = 0;
            // 
            // UserSecretTextBox
            // 
            this.UserSecretTextBox.Location = new System.Drawing.Point(19, 110);
            this.UserSecretTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.UserSecretTextBox.Name = "UserSecretTextBox";
            this.UserSecretTextBox.PasswordChar = '●';
            this.UserSecretTextBox.Size = new System.Drawing.Size(378, 23);
            this.UserSecretTextBox.TabIndex = 1;
            // 
            // AuthorizeButton
            // 
            this.AuthorizeButton.Location = new System.Drawing.Point(116, 269);
            this.AuthorizeButton.Margin = new System.Windows.Forms.Padding(4);
            this.AuthorizeButton.Name = "AuthorizeButton";
            this.AuthorizeButton.Size = new System.Drawing.Size(185, 26);
            this.AuthorizeButton.TabIndex = 2;
            this.AuthorizeButton.Text = "Authorize";
            this.AuthorizeButton.UseVisualStyleBackColor = true;
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyLabel.Location = new System.Drawing.Point(189, 37);
            this.KeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(31, 19);
            this.KeyLabel.TabIndex = 3;
            this.KeyLabel.Text = "Key";
            // 
            // SecretLabel
            // 
            this.SecretLabel.AutoSize = true;
            this.SecretLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SecretLabel.Location = new System.Drawing.Point(183, 87);
            this.SecretLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SecretLabel.Name = "SecretLabel";
            this.SecretLabel.Size = new System.Drawing.Size(46, 19);
            this.SecretLabel.TabIndex = 4;
            this.SecretLabel.Text = "Secret";
            // 
            // UserCurrenyTextBox
            // 
            this.UserCurrenyTextBox.Location = new System.Drawing.Point(19, 160);
            this.UserCurrenyTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.UserCurrenyTextBox.Name = "UserCurrenyTextBox";
            this.UserCurrenyTextBox.Size = new System.Drawing.Size(378, 23);
            this.UserCurrenyTextBox.TabIndex = 5;
            // 
            // CurrencyLabel
            // 
            this.CurrencyLabel.AutoSize = true;
            this.CurrencyLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CurrencyLabel.Location = new System.Drawing.Point(163, 137);
            this.CurrencyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CurrencyLabel.Name = "CurrencyLabel";
            this.CurrencyLabel.Size = new System.Drawing.Size(91, 19);
            this.CurrencyLabel.TabIndex = 6;
            this.CurrencyLabel.Text = "Crypto Name";
            // 
            // AddAuthenticatorButton
            // 
            this.AddAuthenticatorButton.Location = new System.Drawing.Point(116, 302);
            this.AddAuthenticatorButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddAuthenticatorButton.Name = "AddAuthenticatorButton";
            this.AddAuthenticatorButton.Size = new System.Drawing.Size(185, 26);
            this.AddAuthenticatorButton.TabIndex = 7;
            this.AddAuthenticatorButton.Text = "Add authenticator";
            this.AddAuthenticatorButton.UseVisualStyleBackColor = true;
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.HeaderLabel.Location = new System.Drawing.Point(157, 10);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(103, 20);
            this.HeaderLabel.TabIndex = 8;
            this.HeaderLabel.Text = "Authorization";
            // 
            // UserLanguageComboBox
            // 
            this.UserLanguageComboBox.FormattingEnabled = true;
            this.UserLanguageComboBox.Location = new System.Drawing.Point(19, 210);
            this.UserLanguageComboBox.Name = "UserLanguageComboBox";
            this.UserLanguageComboBox.Size = new System.Drawing.Size(157, 23);
            this.UserLanguageComboBox.TabIndex = 9;
            // 
            // LanguageLabel
            // 
            this.LanguageLabel.AutoSize = true;
            this.LanguageLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LanguageLabel.Location = new System.Drawing.Point(63, 188);
            this.LanguageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LanguageLabel.Name = "LanguageLabel";
            this.LanguageLabel.Size = new System.Drawing.Size(69, 19);
            this.LanguageLabel.TabIndex = 10;
            this.LanguageLabel.Text = "Language";
            // 
            // TrackerAuthorizationFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 341);
            this.Controls.Add(this.LanguageLabel);
            this.Controls.Add(this.UserLanguageComboBox);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.AddAuthenticatorButton);
            this.Controls.Add(this.CurrencyLabel);
            this.Controls.Add(this.UserCurrenyTextBox);
            this.Controls.Add(this.SecretLabel);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.AuthorizeButton);
            this.Controls.Add(this.UserSecretTextBox);
            this.Controls.Add(this.UserKeyTextBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TrackerAuthorizationFormView";
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
    private System.Windows.Forms.Label CurrencyLabel;
    private Button AddAuthenticatorButton;
    private Label HeaderLabel;
    private ComboBox UserLanguageComboBox;
    private Label LanguageLabel;
}