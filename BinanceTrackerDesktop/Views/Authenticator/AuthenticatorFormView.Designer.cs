namespace BinanceTrackerDesktop.Views.Authenticator;

partial class AuthenticatorFormView
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
        this.CheckAuthenticationPINButton = new System.Windows.Forms.Button();
        this.UserPINTextBox = new System.Windows.Forms.TextBox();
        this.HeaderLabel = new System.Windows.Forms.Label();
        this.UserSecretTextBot = new System.Windows.Forms.TextBox();
        this.UserPINLabel = new System.Windows.Forms.Label();
        this.SecretLabel = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // CheckAuthenticationPINButton
        // 
        this.CheckAuthenticationPINButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.CheckAuthenticationPINButton.Location = new System.Drawing.Point(132, 195);
        this.CheckAuthenticationPINButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
        this.CheckAuthenticationPINButton.Name = "CheckAuthenticationPINButton";
        this.CheckAuthenticationPINButton.Size = new System.Drawing.Size(272, 42);
        this.CheckAuthenticationPINButton.TabIndex = 8;
        this.CheckAuthenticationPINButton.Text = "Check Authentication PIN";
        this.CheckAuthenticationPINButton.UseVisualStyleBackColor = true;
        // 
        // UserPINTextBox
        // 
        this.UserPINTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.UserPINTextBox.Location = new System.Drawing.Point(18, 157);
        this.UserPINTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
        this.UserPINTextBox.Name = "UserPINTextBox";
        this.UserPINTextBox.Size = new System.Drawing.Size(488, 26);
        this.UserPINTextBox.TabIndex = 6;
        // 
        // HeaderLabel
        // 
        this.HeaderLabel.AutoSize = true;
        this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.HeaderLabel.Location = new System.Drawing.Point(131, 23);
        this.HeaderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.HeaderLabel.Name = "HeaderLabel";
        this.HeaderLabel.Size = new System.Drawing.Size(262, 25);
        this.HeaderLabel.TabIndex = 10;
        this.HeaderLabel.Text = "Authenticate Yourself Please";
        // 
        // UserSecretTextBot
        // 
        this.UserSecretTextBot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.UserSecretTextBot.Location = new System.Drawing.Point(18, 87);
        this.UserSecretTextBot.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
        this.UserSecretTextBot.Name = "UserSecretTextBot";
        this.UserSecretTextBot.Size = new System.Drawing.Size(488, 26);
        this.UserSecretTextBot.TabIndex = 11;
        // 
        // UserPINLabel
        // 
        this.UserPINLabel.AutoSize = true;
        this.UserPINLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.UserPINLabel.Location = new System.Drawing.Point(249, 125);
        this.UserPINLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.UserPINLabel.Name = "UserPINLabel";
        this.UserPINLabel.Size = new System.Drawing.Size(35, 20);
        this.UserPINLabel.TabIndex = 12;
        this.UserPINLabel.Text = "PIN:";
        // 
        // SecretLabel
        // 
        this.SecretLabel.AutoSize = true;
        this.SecretLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.SecretLabel.Location = new System.Drawing.Point(240, 55);
        this.SecretLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.SecretLabel.Name = "SecretLabel";
        this.SecretLabel.Size = new System.Drawing.Size(53, 20);
        this.SecretLabel.TabIndex = 13;
        this.SecretLabel.Text = "Secret:";
        // 
        // AuthenticatorForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(524, 247);
        this.Controls.Add(this.SecretLabel);
        this.Controls.Add(this.UserPINLabel);
        this.Controls.Add(this.UserSecretTextBot);
        this.Controls.Add(this.HeaderLabel);
        this.Controls.Add(this.CheckAuthenticationPINButton);
        this.Controls.Add(this.UserPINTextBox);
        this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
        this.Name = "AuthenticatorForm";
        this.Text = "Authenticator";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button CheckAuthenticationPINButton;
    private System.Windows.Forms.TextBox UserPINTextBox;
    private System.Windows.Forms.Label HeaderLabel;
    private System.Windows.Forms.TextBox UserSecretTextBot;
    private System.Windows.Forms.Label UserPINLabel;
    private System.Windows.Forms.Label SecretLabel;
}