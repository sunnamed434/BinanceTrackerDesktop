namespace BinanceTrackerDesktop.Forms.Tray;

partial class TrackerTrayForm
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
        this.components = new System.ComponentModel.Container();
        this.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
        this.SuspendLayout();
        // 
        // ContextMenuStrip
        // 
        this.ContextMenuStrip.Name = "Tray";
        this.ContextMenuStrip.Size = new System.Drawing.Size(61, 4);
        // 
        // NotifyIcon
        // 
        this.NotifyIcon.Text = "notifyIcon1";
        this.NotifyIcon.Visible = true;
        // 
        // BinanceTrackerTrayForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(296, 0);
        this.Name = "BinanceTrackerTrayForm";
        this.Text = "BinanceTrackerTrayForm";
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ContextMenuStrip ContextMenuStrip;
    private System.Windows.Forms.NotifyIcon NotifyIcon;
}